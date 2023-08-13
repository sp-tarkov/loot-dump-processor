using System.Collections.Concurrent;
using LootDumpProcessor.Logger;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process.Impl;

public class JsonFileIntakeReader : IIntakeReader
{
    private static readonly IJsonSerializer _jsonSerializer = JsonSerializerFactory.GetInstance();

    private static readonly HashSet<string>? _ignoredLocations =
        LootDumpProcessorContext.GetConfig().ReaderConfig.IntakeReaderConfig?.IgnoredDumpLocations.ToHashSet();

    private readonly ConcurrentDictionary<string, int> _totalMapDumpsCounter = new();
    
    public bool Read(string file, out BasicInfo basicInfo)
    {
        var fileData = File.ReadAllText(file);
        // If the file format changes it may screw up this date parser
        if (!FileDateParser.TryParseFileDate(file, out var date))
            LoggerFactory.GetInstance().Log($"Couldnt parse date from file: {file}", LogLevel.Error);

        var fi = _jsonSerializer.Deserialize<RootData>(fileData);
        if (fi.Data?.Name != null && (!_ignoredLocations?.Contains(fi.Data.Name) ?? true))
        {
            int counter;
            if (!_totalMapDumpsCounter.TryGetValue(fi.Data.Name, out counter))
            {
                counter = 0;
                _totalMapDumpsCounter[fi.Data.Name] = counter;
            }

            if (counter < LootDumpProcessorContext.GetConfig().ReaderConfig.IntakeReaderConfig.MaxDumpsPerMap)
            {
                basicInfo = new BasicInfo
                {
                    Map = fi.Data.Name,
                    FileHash = ProcessorUtil.HashFile(fileData),
                    Data = fi,
                    Date = date.Value,
                    FileName = file
                };
                _totalMapDumpsCounter[fi.Data.Name] += 1;
                LoggerFactory.GetInstance().Log($"File {file} fully read, returning data", LogLevel.Info);
                return true;
            }
            LoggerFactory.GetInstance().Log($"Ignoring file {file} as the file cap for map {fi.Data.Name} has been reached", LogLevel.Info);
        }

        LoggerFactory.GetInstance().Log(
            $"File {file} was not eligible for dump data, it did not contain a location name or it was on ignored locations config",
            LogLevel.Info
        );
        basicInfo = null;
        return false;
    }
}