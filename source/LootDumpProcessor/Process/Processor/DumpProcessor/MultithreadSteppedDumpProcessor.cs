using System.Collections.Concurrent;
using System.Text.Json;
using LootDumpProcessor;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Output.LooseLoot;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.v2.AmmoProcessor;
using LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Storage.Collections;
using LootDumpProcessor.Utils;
using Microsoft.Extensions.Logging;

public class MultithreadSteppedDumpProcessor(
    IStaticLootProcessor staticLootProcessor,
    IStaticContainersProcessor staticContainersProcessor,
    IAmmoProcessor ammoProcessor,
    ILooseLootProcessor looseLootProcessor,
    ILogger<MultithreadSteppedDumpProcessor> logger, IKeyGenerator keyGenerator
)
    : IDumpProcessor
{
    private readonly IStaticLootProcessor _staticLootProcessor =
        staticLootProcessor ?? throw new ArgumentNullException(nameof(staticLootProcessor));

    private readonly IStaticContainersProcessor _staticContainersProcessor =
        staticContainersProcessor ?? throw new ArgumentNullException(nameof(staticContainersProcessor));

    private readonly IAmmoProcessor _ammoProcessor =
        ammoProcessor ?? throw new ArgumentNullException(nameof(ammoProcessor));

    private readonly ILooseLootProcessor _looseLootProcessor =
        looseLootProcessor ?? throw new ArgumentNullException(nameof(looseLootProcessor));

    private readonly ILogger<MultithreadSteppedDumpProcessor> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IKeyGenerator
        _keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));

    private static readonly IDataStorage _dataStorage = DataStorageFactory.GetInstance();

    public Dictionary<OutputFileType, object> ProcessDumps(List<PartialData> dumps)
    {
        _logger.LogInformation("Starting final dump processing");
        var output = new Dictionary<OutputFileType, object>();

        var dumpProcessData = GetDumpProcessData(dumps);
        _logger.LogInformation("Heavy processing done!");

        var staticContainers = new ConcurrentDictionary<string, MapStaticLoot>();
        var mapDumpCounter = new ConcurrentDictionary<string, int>();
        var mapStaticContainersAggregated = new ConcurrentDictionary<string, ConcurrentDictionary<Template, int>>();

        _logger.LogInformation("Queuing dumps for static data processing");

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        Parallel.ForEachAsync(dumps, parallelOptions,
            async (partialData, cancellationToken) =>
                await Process(partialData, staticContainers, mapStaticContainersAggregated, mapDumpCounter));

        _logger.LogInformation("All static data processing threads finished");

        mapStaticContainersAggregated.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Select(
                    td => new StaticDataPoint
                    {
                        Template = td.Key,
                        Probability = GetStaticContainerProbability(kv.Key, td, mapDumpCounter)
                    }
                ).ToList()
            ).ToList()
            .ForEach(kv =>
                staticContainers[kv.Key].StaticContainers = kv.Value);

        output.Add(OutputFileType.StaticContainer, staticContainers);
        _logger.LogInformation("Processing ammo distribution");

        var staticAmmo = new ConcurrentDictionary<string, IReadOnlyDictionary<string, List<AmmoDistribution>>>();
        Parallel.ForEach(dumpProcessData.ContainerCounts.Keys, parallelOptions, mapId =>
        {
            var preProcessedStaticLoots = dumpProcessData.ContainerCounts[mapId];
            var ammoDistribution = _ammoProcessor.CreateAmmoDistribution(mapId, preProcessedStaticLoots);
            staticAmmo[mapId] = ammoDistribution;
        });
        output.Add(OutputFileType.StaticAmmo, staticAmmo);

        _logger.LogInformation("Processing static loot distribution");

        var staticLoot = new ConcurrentDictionary<string, IReadOnlyDictionary<string, StaticItemDistribution>>();
        Parallel.ForEach(dumpProcessData.ContainerCounts.Keys, parallelOptions, mapId =>
        {
            var preProcessedStaticLoots = dumpProcessData.ContainerCounts[mapId];
            var staticLootDistribution =
                _staticLootProcessor.CreateStaticLootDistribution(mapId, preProcessedStaticLoots);
            staticLoot[mapId] = staticLootDistribution;
        });
        output.Add(OutputFileType.StaticLoot, staticLoot);

        _logger.LogInformation("Processing loose loot distribution");

        var looseLoot = new ConcurrentDictionary<string, LooseLootRoot>();
        Parallel.ForEach(dumpProcessData.MapCounts.Keys, parallelOptions, mapId =>
        {
            var mapCount = dumpProcessData.MapCounts[mapId];
            var looseLootCount = dumpProcessData.LooseLootCounts[mapId];
            var looseLootDistribution =
                _looseLootProcessor.CreateLooseLootDistribution(mapId, mapCount, looseLootCount);
            looseLoot[mapId] = looseLootDistribution;
        });
        _logger.LogInformation("Collecting loose loot distribution information");
        var loot = dumpProcessData.MapCounts
            .Select(mapCount => mapCount.Key)
            .ToDictionary(mi => mi, mi => looseLoot[mi]);

        output.Add(OutputFileType.LooseLoot, loot);
        _logger.LogInformation("Dump processing fully completed!");
        return output;
    }

    private async Task Process(PartialData partialData,
        ConcurrentDictionary<string, MapStaticLoot> staticContainers,
        ConcurrentDictionary<string, ConcurrentDictionary<Template, int>> mapStaticContainersAggregated,
        ConcurrentDictionary<string, int> mapDumpCounter)
    {
        _logger.LogDebug("Processing static data for file {FileName}", partialData.BasicInfo.FileName);

        var fileContent = await File.ReadAllTextAsync(partialData.BasicInfo.FileName);
        var dataDump = JsonSerializer.Deserialize<RootData>(fileContent, JsonSerializerSettings.Default);

        if (dataDump == null)
        {
            _logger.LogError("Failed to deserialize data from file {FileName}", partialData.BasicInfo.FileName);
            return;
        }

        var mapId = dataDump.Data.LocationLoot.Id.ToLower();

        if (!staticContainers.TryGetValue(mapId, out var mapStaticLoot))
        {
            _logger.LogInformation("Doing first time process for map {MapId} of real static data", mapId);

            staticContainers[mapId] = new MapStaticLoot
            {
                StaticWeapons = new List<Template>(),
                StaticForced = new List<StaticForced>()
            };
        }
        else
        {
            var mapStaticContainers =
                _staticContainersProcessor.CreateStaticWeaponsAndForcedContainers(dataDump);

            var newStaticWeapons = mapStaticContainers.StaticWeapons.Where(x =>
                !mapStaticLoot.StaticWeapons.Exists(y => y.Id == x.Id));
            var newStaticForced = mapStaticContainers.StaticForced.Where(x =>
                !mapStaticLoot.StaticForced.Exists(y => y.ContainerId == x.ContainerId));

            mapStaticLoot.StaticWeapons.AddRange(newStaticWeapons);
            mapStaticLoot.StaticForced.AddRange(newStaticForced);
        }

        if (!mapStaticContainersAggregated.TryGetValue(mapId,
                out ConcurrentDictionary<Template, int> mapAggregatedDataDict))
        {
            mapAggregatedDataDict = new ConcurrentDictionary<Template, int>();
            mapStaticContainersAggregated.TryAdd(mapId, mapAggregatedDataDict);
        }

        if (!DumpWasMadeAfterConfigThresholdDate(partialData)) return;

        IncrementMapCounterDictionaryValue(mapDumpCounter, mapId);

        var containerIgnoreListExists = LootDumpProcessorContext.GetConfig().ContainerIgnoreList
            .TryGetValue(mapId, out var ignoreListForMap);
        foreach (var dynamicStaticContainer in _staticContainersProcessor.CreateDynamicStaticContainers(
                     dataDump))
        {
            if (containerIgnoreListExists && ignoreListForMap.Contains(dynamicStaticContainer.Id)) continue;

            if (!mapAggregatedDataDict.TryAdd(dynamicStaticContainer, 1))
                mapAggregatedDataDict[dynamicStaticContainer] += 1;
        }

        GCHandler.Collect();
    }

    private static bool DumpWasMadeAfterConfigThresholdDate(PartialData dataDump) =>
        FileDateParser.TryParseFileDate(dataDump.BasicInfo.FileName, out var fileDate) &&
        fileDate.HasValue &&
        fileDate.Value > LootDumpProcessorContext.GetConfig().DumpProcessorConfig
            .SpawnContainerChanceIncludeAfterDate;

    private static void IncrementMapCounterDictionaryValue(IDictionary<string, int> mapDumpCounter, string mapName)
    {
        if (!mapDumpCounter.TryAdd(mapName, 1)) mapDumpCounter[mapName] += 1;
    }

    private static double GetStaticContainerProbability(string mapName, KeyValuePair<Template, int> td,
        IReadOnlyDictionary<string, int> mapDumpCounter) =>
        Math.Round((double)((decimal)td.Value / (decimal)mapDumpCounter[mapName]), 2);

    private DumpProcessData GetDumpProcessData(List<PartialData> dumps)
    {
        var dumpProcessData = new DumpProcessData();

        dumps.GroupBy(dump => dump.BasicInfo.Map)
            .ToList()
            .ForEach(tuple =>
            {
                var mapName = tuple.Key;
                var partialFileMetaData = tuple.ToList();
                _logger.LogInformation("Processing map {MapName}, total dump data to process: {Count}", mapName,
                    partialFileMetaData.Count);
                dumpProcessData.MapCounts[mapName] = partialFileMetaData.Count;

                var lockObjectContainerCounts = new object();
                var lockObjectCounts = new object();

                var lockObjectDictionaryCounts = new object();
                var dictionaryCounts = new FlatKeyableDictionary<string, int>(_keyGenerator.Generate());

                var lockObjectDictionaryItemProperties = new object();
                var dictionaryItemProperties =
                    new FlatKeyableDictionary<string, FlatKeyableList<Template>>(_keyGenerator.Generate());

                var actualDictionaryItemProperties = new FlatKeyableDictionary<string, IKey>(_keyGenerator.Generate());
                var looseLootCounts = new LooseLootCounts(_keyGenerator.Generate(), dictionaryCounts.GetKey(),
                    actualDictionaryItemProperties.GetKey());

                dumpProcessData.LooseLootCounts.Add(mapName, looseLootCounts.GetKey());

                BlockingCollection<PartialData> _partialDataToProcess = new();

                foreach (var partialData in partialFileMetaData) _partialDataToProcess.Add(partialData);

                partialFileMetaData = null;
                tuple = null;
                GCHandler.Collect();

                var parallelOptions = new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                };
                Parallel.ForEach(_partialDataToProcess, parallelOptions,
                    partialData =>
                    {
                        ProcessPartialData(partialData, lockObjectContainerCounts, dumpProcessData, mapName,
                            lockObjectDictionaryCounts, dictionaryCounts, lockObjectDictionaryItemProperties,
                            dictionaryItemProperties, actualDictionaryItemProperties, lockObjectCounts,
                            looseLootCounts);
                    });

                foreach (var (_, value) in dictionaryItemProperties) _dataStorage.Store(value);

                _dataStorage.Store(dictionaryCounts);
                dictionaryCounts = null;
                GCHandler.Collect();

                _dataStorage.Store(actualDictionaryItemProperties);
                actualDictionaryItemProperties = null;
                GCHandler.Collect();
                _dataStorage.Store(looseLootCounts);
                looseLootCounts = null;
                GCHandler.Collect();
            });
        return dumpProcessData;
    }

    private void ProcessPartialData(PartialData partialDataToProcess,
        object lockObjectContainerCounts,
        DumpProcessData dumpProcessData, string mapName, object lockObjectDictionaryCounts,
        FlatKeyableDictionary<string, int>? dictionaryCounts, object lockObjectDictionaryItemProperties,
        FlatKeyableDictionary<string, FlatKeyableList<Template>> dictionaryItemProperties,
        FlatKeyableDictionary<string, IKey>? actualDictionaryItemProperties,
        object lockObjectCounts, LooseLootCounts? looseLootCounts)
    {
        try
        {
            var dumpData = _dataStorage.GetItem<ParsedDump>(partialDataToProcess.ParsedDumpKey);

            lock (lockObjectContainerCounts)
            {
                if (!dumpProcessData.ContainerCounts.ContainsKey(mapName))
                    dumpProcessData.ContainerCounts.Add(mapName,
                        dumpData.Containers.ToList());
                else dumpProcessData.ContainerCounts[mapName].AddRange(dumpData.Containers);
            }

            var loadedDictionary =
                _dataStorage
                    .GetItem<SubdivisionedKeyableDictionary<string, List<Template>>>(
                        dumpData.LooseLoot.ItemProperties
                    );
            foreach (var (uniqueKey, containerTemplate) in loadedDictionary)
            {
                var count = dumpData.LooseLoot.Counts[uniqueKey];
                lock (lockObjectDictionaryCounts)
                {
                    if (dictionaryCounts.ContainsKey(uniqueKey))
                        dictionaryCounts[uniqueKey] += count;
                    else
                        dictionaryCounts[uniqueKey] = count;
                }

                lock (lockObjectDictionaryItemProperties)
                {
                    if (!dictionaryItemProperties.TryGetValue(uniqueKey, out var values))
                    {
                        values = new FlatKeyableList<Template>(_keyGenerator.Generate());
                        dictionaryItemProperties.Add(uniqueKey, values);
                        actualDictionaryItemProperties.Add(uniqueKey, values.GetKey());
                    }

                    values.AddRange(containerTemplate);
                }
            }

            lock (lockObjectCounts)
            {
                looseLootCounts.MapSpawnpointCount.Add(
                    dumpData.LooseLoot.MapSpawnpointCount);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("ERROR OCCURRED: {Message}\n{StackTrace}", e.Message, e.StackTrace);
        }
    }
}