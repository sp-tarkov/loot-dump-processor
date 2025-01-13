using System.Collections.Concurrent;
using System.Text.Json;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor.Process.Reader.Intake;

public class JsonFileIntakeReader(ILogger<JsonFileIntakeReader> logger, IOptions<Config> config) : IIntakeReader
{
    private readonly ILogger<JsonFileIntakeReader> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly Config _config = (config ?? throw new ArgumentNullException(nameof(config))).Value;

    private HashSet<string> IgnoredLocations => _config
        .ReaderConfig.IntakeReaderConfig.IgnoredDumpLocations.ToHashSet();

    private readonly ConcurrentDictionary<string, int> _totalMapDumpsCounter = new();

    public bool Read(string file, out BasicInfo basicInfo)
    {
        basicInfo = null;
        string? fileData;

        try
        {
            fileData = File.ReadAllText(file);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to read file: {File}", file);
            return false;
        }

        if (string.IsNullOrWhiteSpace(fileData))
        {
            _logger.LogError("Could not parse data from file: {File}", file);
            return false;
        }

        // If the file format changes, it may affect the date parser
        if (!FileDateParser.TryParseFileDate(file, out var date))
            _logger.LogError("Could not parse date from file: {File}", file);

        var fi = JsonSerializer.Deserialize<RootData>(fileData, JsonSerializerSettings.Default);
        if (fi?.Data.LocationLoot.Name != null && (!IgnoredLocations?.Contains(fi.Data.LocationLoot.Name) ?? true))
        {
            var mapName = fi.Data.LocationLoot.Name;
            var mapId = fi.Data.LocationLoot.Id.ToLower();

            var counter = _totalMapDumpsCounter.AddOrUpdate(mapName, 0, (_, current) => current);

            var maxDumpsPerMap = _config
                .ReaderConfig.IntakeReaderConfig?.MaxDumpsPerMap ?? 1500;

            if (counter < maxDumpsPerMap)
            {
                basicInfo = new BasicInfo
                {
                    Map = mapId,
                    FileHash = ProcessorUtil.HashFile(fileData),
                    Data = fi,
                    Date = date ?? DateTime.MinValue,
                    FileName = file
                };

                _totalMapDumpsCounter[mapName] += 1;

                _logger.LogDebug("File {File} fully read, returning data", file);
                return true;
            }

            // Map dump limit reached, exit
            _logger.LogDebug("Ignoring file {File} as the file cap for map {MapId} has been reached", file, mapId);
            return false;
        }

        _logger.LogWarning(
            "File {File} was not eligible for dump data; it did not contain a location name or it was on the ignored locations config",
            file);
        return false;
    }
}