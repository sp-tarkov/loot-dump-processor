﻿using System.Collections.Concurrent;
using LootDumpProcessor.Logger;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Storage.Collections;

namespace LootDumpProcessor.Process.Processor.DumpProcessor;

public class MultithreadSteppedDumpProcessor : IDumpProcessor
{
    private static IJsonSerializer _jsonSerializer = JsonSerializerFactory.GetInstance();

    private static readonly List<Task> Runners = new();

    private static readonly BlockingCollection<PartialData> _partialDataToProcess = new();

    // if we need to, this variable can be moved to use the factory, but since the factory
    // needs a locking mechanism to prevent dictionary access exceptions, its better to keep
    // a reference to use here
    private static readonly IDataStorage _dataStorage = DataStorageFactory.GetInstance();

    public Dictionary<OutputFileType, object> ProcessDumps(List<PartialData> dumps)
    {
        LoggerFactory.GetInstance().Log("Starting final dump processing", LogLevel.Info);
        var output = new Dictionary<OutputFileType, object>();

        var dumpProcessData = GetDumpProcessData(dumps);
        LoggerFactory.GetInstance().Log("Heavy processing done!", LogLevel.Info);

        var staticContainers = new Dictionary<string, MapStaticLoot>();
        var staticContainersLock = new object();
        // We need to count how many dumps we have for each map
        var mapDumpCounter = new Dictionary<string, int>();
        var mapDumpCounterLock = new object();
        // dictionary of maps, that has a dictionary of template and hit count
        var mapStaticContainersAggregated = new Dictionary<string, Dictionary<Template, int>>();
        var mapStaticContainersAggregatedLock = new object();

        Runners.Clear();
        // BSG changed the map data so static containers are now dynamic, so we need to scan all dumps for the static containers.
        foreach (var dumped in dumps)
        {
            Runners.Add(
                Task.Factory.StartNew(() =>
                {
                    var data = _jsonSerializer.Deserialize<RootData>(File.ReadAllText(dumped.BasicInfo.FileName));
                    // the if statement below will keep track of how many dumps we have for each map
                    lock (mapDumpCounterLock)
                    {
                        if (mapDumpCounter.ContainsKey(data.Data.Name))
                            mapDumpCounter[data.Data.Name] += 1;
                        else
                            mapDumpCounter.Add(data.Data.Name, 1);
                    }
                    // the if statement below takes care of processing "forced" or real static data for each map, we only need
                    // to do this once per map, so we dont care about doing it again
                    lock (staticContainersLock)
                    {
                        if (!staticContainers.ContainsKey(data.Data.Name))
                        {
                            var mapStaticLoot = StaticLootProcessor.CreateRealStaticContainers(data);
                            staticContainers[mapStaticLoot.Item1] = mapStaticLoot.Item2;
                        }
                    }

                    // the section below takes care of finding how many "dynamic static containers" we have on the map
                    Dictionary<Template, int> mapAggregatedData;
                    lock (mapStaticContainersAggregatedLock)
                    {
                        if (!mapStaticContainersAggregated.TryGetValue(data.Data.Name, out mapAggregatedData))
                        {
                            mapAggregatedData = new Dictionary<Template, int>();
                            mapStaticContainersAggregated.Add(data.Data.Name, mapAggregatedData);
                        }
                    }

                    // Only process the dump file if the date is higher (after) the configuration date
                    if (FileDateParser.TryParseFileDate(dumped.BasicInfo.FileName, out var fileDate) &&
                        fileDate.HasValue &&
                        fileDate.Value > LootDumpProcessorContext.GetConfig().DumpProcessorConfig
                            .SpawnContainerChanceIncludeAfterDate)
                    {
                        foreach (var dynamicStaticContainer in StaticLootProcessor.CreateDynamicStaticContainers(data))
                        {
                            lock (mapStaticContainersAggregatedLock)
                            {
                                if (mapAggregatedData.ContainsKey(dynamicStaticContainer))
                                    mapAggregatedData[dynamicStaticContainer] += 1;
                                else
                                    mapAggregatedData.Add(dynamicStaticContainer, 1);
                            }
                        }
                    }

                    GCHandler.Collect();
                })
            );
        }

        Task.WaitAll(Runners.ToArray());
        // Aggregate and calculate the probability of a static container
        mapStaticContainersAggregated.ToDictionary(
            kv => kv.Key,
            kv => kv.Value.Select(
                td => new StaticDataPoint
                {
                    Template = td.Key,
                    Probability = GetStaticProbability(kv.Key, td, mapDumpCounter)
                }
            ).ToList()
        ).ToList().ForEach(kv => staticContainers[kv.Key].StaticContainers = kv.Value);

        // Static containers
        output.Add(OutputFileType.StaticContainer, staticContainers);

        // Ammo distribution
        output.Add(
            OutputFileType.StaticAmmo,
            StaticLootProcessor.CreateAmmoDistribution(dumpProcessData.ContainerCounts)
        );

        // Static loot distribution
        output.Add(
            OutputFileType.StaticLoot,
            StaticLootProcessor.CreateStaticLootDistribution(dumpProcessData.ContainerCounts)
        );

        // Loose loot distribution
        var looseLootDistribution = LooseLootProcessor.CreateLooseLootDistribution(
            dumpProcessData.MapCounts,
            dumpProcessData.LooseLootCounts
        );

        var loot = dumpProcessData.MapCounts
            .Select(mapCount => mapCount.Key)
            .ToDictionary(mi => mi, mi => looseLootDistribution[mi]);

        output.Add(OutputFileType.LooseLoot, loot);
        return output;
    }

    private static double GetStaticProbability(string mapName, KeyValuePair<Template, int> td, Dictionary<string, int> mapDumpCounter)
    {
        if (mapName == "Streets of Tarkov")
        {
            var result = LootDumpProcessorContext.GetTarkovItems().GetProbabilityByContainerId(td.Key.Id);
            if (result != null)
            {
                return Math.Round((double)(result.probability), 2);
            }
        }
        
        return Math.Round((double)((decimal)td.Value / (decimal)mapDumpCounter[mapName]), 2);
    }

    private DumpProcessData GetDumpProcessData(List<PartialData> dumps)
    {
        var dumpProcessData = new DumpProcessData();

        dumps.GroupBy(x => x.BasicInfo.Map)
            .ToList()
            .ForEach(tuple =>
            {
                var mapi = tuple.Key;
                var g = tuple.ToList();
                LoggerFactory.GetInstance().Log(
                    $"Processing map {mapi}, total dump data to process: {g.Count}",
                    LogLevel.Info
                );
                dumpProcessData.MapCounts[mapi] = g.Count;

                var lockObjectContainerCounts = new object();

                var lockObjectCounts = new object();
                var counts = new LooseLootCounts();

                var lockObjectDictionaryCounts = new object();
                var dictionaryCounts = new FlatKeyableDictionary<string, int>();
                counts.Counts = dictionaryCounts.GetKey();

                /*
                var dictionaryItemCounts = new FlatKeyableDictionary<string, List<string>>();
                counts.Items = dictionaryItemCounts.GetKey();
                */

                var lockObjectDictionaryItemProperties = new object();
                var dictionaryItemProperties = new FlatKeyableDictionary<string, FlatKeyableList<Template>>();

                var actualDictionaryItemProperties = new FlatKeyableDictionary<string, IKey>();
                counts.ItemProperties = actualDictionaryItemProperties.GetKey();

                dumpProcessData.LooseLootCounts.Add(mapi, counts.GetKey());
                // add the items to the queue
                foreach (var gi in g)
                {
                    _partialDataToProcess.Add(gi);
                }

                // Call GC before running threads
                g = null;
                tuple = null;
                GCHandler.Collect();

                // The data storage factory has a lock, we dont want the locks to occur when multithreading

                for (int i = 0; i < LootDumpProcessorContext.GetConfig().Threads; i++)
                {
                    Runners.Add(
                        Task.Factory.StartNew(
                            () =>
                            {
                                while (_partialDataToProcess.TryTake(out var partialData,
                                           TimeSpan.FromMilliseconds(5000)))
                                {
                                    try
                                    {
                                        var dumpData = _dataStorage.GetItem<ParsedDump>(partialData.ParsedDumpKey);
                                        lock (lockObjectContainerCounts)
                                        {
                                            dumpProcessData.ContainerCounts.AddRange(dumpData.Containers);
                                        }

                                        // loose loot into ids on files
                                        var loadedDictionary =
                                            _dataStorage
                                                .GetItem<SubdivisionedKeyableDictionary<string, List<Template>>>(
                                                    dumpData.LooseLoot.ItemProperties
                                                );
                                        foreach (var (k, v) in loadedDictionary)
                                        {
                                            var count = dumpData.LooseLoot.Counts[k];
                                            lock (lockObjectDictionaryCounts)
                                            {
                                                if (dictionaryCounts.ContainsKey(k))
                                                    dictionaryCounts[k] += count;
                                                else
                                                    dictionaryCounts[k] = count;
                                            }

                                            /*
                                            var itemList = dumpData.LooseLoot.Items[k];
                                            if (!dictionaryItemCounts.TryGetValue(k, out var itemCounts))
                                            {
                                                itemCounts = new List<string>();
                                                dictionaryItemCounts.Add(k, itemCounts);
                                            }
                                            itemCounts.AddRange(itemList);
                                            */

                                            lock (lockObjectDictionaryItemProperties)
                                            {
                                                if (!dictionaryItemProperties.TryGetValue(k, out var values))
                                                {
                                                    values = new FlatKeyableList<Template>();
                                                    dictionaryItemProperties.Add(k, values);
                                                    actualDictionaryItemProperties.Add(k, values.GetKey());
                                                }

                                                values.AddRange(v);
                                            }
                                        }

                                        lock (lockObjectCounts)
                                        {
                                            counts.MapSpawnpointCount.AddRange(new List<int>
                                            {
                                                dumpData.LooseLoot.MapSpawnpointCount
                                            });
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        LoggerFactory.GetInstance().Log(
                                            $"ERROR OCCURRED:{e.Message}\n{e.StackTrace}",
                                            LogLevel.Error
                                        );
                                    }
                                }
                            },
                            TaskCreationOptions.LongRunning)
                    );
                }

                // Wait until all runners are done processing
                while (!Runners.All(r => r.IsCompleted))
                {
                    LoggerFactory.GetInstance().Log(
                        $"One or more file processors are still processing files. Waiting {LootDumpProcessorContext.GetConfig().ThreadPoolingTimeoutMs}ms before checking again",
                        LogLevel.Info
                    );
                    Thread.Sleep(
                        TimeSpan.FromMilliseconds(LootDumpProcessorContext.GetConfig().ThreadPoolingTimeoutMs));
                }

                foreach (var (_, value) in dictionaryItemProperties)
                {
                    _dataStorage.Store(value);
                }

                _dataStorage.Store(dictionaryCounts);
                dictionaryCounts = null;
                GCHandler.Collect();
                /*
                DataStorageFactory.GetInstance().Store(dictionaryItemCounts);
                dictionaryItemCounts = null;
                GC.Collect();
                */
                _dataStorage.Store(actualDictionaryItemProperties);
                actualDictionaryItemProperties = null;
                GCHandler.Collect();
                _dataStorage.Store(counts);
                counts = null;
                GCHandler.Collect();
            });
        return dumpProcessData;
    }
}