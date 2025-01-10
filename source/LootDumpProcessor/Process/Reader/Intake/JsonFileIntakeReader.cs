using System.Collections.Concurrent;
using LootDumpProcessor.Logger;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Utils;

namespace LootDumpProcessor.Process.Reader.Intake;

public class JsonFileIntakeReader : IIntakeReader
{
    private static readonly IJsonSerializer _jsonSerializer = JsonSerializerFactory.GetInstance();

    private static readonly HashSet<string>? _ignoredLocations =
        LootDumpProcessorContext.GetConfig().ReaderConfig.IntakeReaderConfig?.IgnoredDumpLocations.ToHashSet();

    private static readonly ConcurrentDictionary<string, int> _totalMapDumpsCounter = new();
    
    public bool Read(string file, out BasicInfo basicInfo)
    {
        var fileData = File.ReadAllText(file);

        if (fileData == null)
        {
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Error))
                LoggerFactory.GetInstance().Log($"Couldnt parse date from file: {file}", LogLevel.Error);

            basicInfo = null;
            return false;
        }

        // If the file format changes it may screw up this date parser
        if (!FileDateParser.TryParseFileDate(file, out var date))
        {
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Error))
                LoggerFactory.GetInstance().Log($"Couldnt parse date from file: {file}", LogLevel.Error);
        }

        var fi = _jsonSerializer.Deserialize<RootData>(fileData);
        if (fi?.Data?.LocationLoot?.Name != null && (!_ignoredLocations?.Contains(fi.Data.LocationLoot.Name) ?? true))
        {
            if (!_totalMapDumpsCounter.TryGetValue(fi.Data.LocationLoot.Name, out var counter))
            {
                counter = 0;
                _totalMapDumpsCounter[fi.Data.LocationLoot.Name] = counter;
            }

            if (counter < (LootDumpProcessorContext.GetConfig().ReaderConfig.IntakeReaderConfig?.MaxDumpsPerMap ?? 1500))
            {
                basicInfo = new BasicInfo
                {
                    Map = fi.Data.LocationLoot.Id.ToLower(),
                    FileHash = ProcessorUtil.HashFile(fileData),
                    Data = fi,
                    Date = date ?? DateTime.MinValue,
                    FileName = file
                };

                _totalMapDumpsCounter[fi.Data.LocationLoot.Name] += 1;

                if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Debug))
                    LoggerFactory.GetInstance().Log($"File {file} fully read, returning data", LogLevel.Debug);

                return true;
            }

            // Map dump limit reached, exit
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Debug))
                LoggerFactory.GetInstance().Log($"Ignoring file {file} as the file cap for map {fi.Data.LocationLoot.Id} has been reached", LogLevel.Debug);

            basicInfo = null;
            return false;
        }

        if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Warning))
            LoggerFactory.GetInstance().Log($"File {file} was not eligible for dump data, it did not contain a location name or it was on ignored locations config", LogLevel.Warning);
        
        basicInfo = null;
        return false;
    }
}