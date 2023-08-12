using System.Globalization;
using System.Text.RegularExpressions;
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

    private static Regex FileNameDateRegex = new("([0-9]{4}(-[0-9]{2}){2}_((-){0,1}[0-9]{2}){3})");

    public bool Read(string file, out BasicInfo basicInfo)
    {
        var fileData = File.ReadAllText(file);
        var unparsedDate = FileNameDateRegex.Match(file).Groups[1].Value;
        var date = DateTime.ParseExact(unparsedDate, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);

        var fi = _jsonSerializer.Deserialize<RootData>(fileData);
        if (fi.Data?.Name != null && (!_ignoredLocations?.Contains(fi.Data.Name) ?? true))
        {
            basicInfo = new BasicInfo
            {
                Map = fi.Data.Name,
                FileHash = ProcessorUtil.HashFile(fileData),
                Data = fi,
                Date = date,
                FileName = file
            };
            LoggerFactory.GetInstance().Log($"File {file} fully read, returning data", LogLevel.Info);
            return true;
        }

        LoggerFactory.GetInstance().Log(
            $"File {file} was not eligible for dump data, it did not contain a location name or it was on ignored locations config",
            LogLevel.Info
        );
        basicInfo = null;
        return false;
    }
}