using System.Text.Json;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Serializers.Json;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor.Process.Collector;

public class DumpCollector(IOptions<Config> config) : ICollector
{
    private readonly Config _config = (config ?? throw new ArgumentNullException(nameof(config))).Value;

    private string DumpLocation => Path.Combine(_config.CollectorConfig.DumpLocation, "collector");

    private List<PartialData> ProcessedDumps => new(_config.CollectorConfig.MaxEntitiesBeforeDumping + 50);

    private readonly object lockObject = new();

    public void Setup()
    {
        if (Directory.Exists(DumpLocation)) Directory.Delete(DumpLocation, true);

        Directory.CreateDirectory(DumpLocation);
    }

    public void Hold(PartialData parsedDump)
    {
        lock (lockObject)
        {
            ProcessedDumps.Add(parsedDump);
            if (ProcessedDumps.Count > _config.CollectorConfig.MaxEntitiesBeforeDumping)
            {
                var fileName = $"collector-{DateTime.Now.ToString("yyyyMMddHHmmssfffff")}.json";
                File.WriteAllText($"{DumpLocation}{fileName}",
                    JsonSerializer.Serialize(ProcessedDumps, JsonSerializerSettings.Default));
                ProcessedDumps.Clear();
            }
        }
    }

    public List<PartialData> Retrieve()
    {
        foreach (var file in Directory.GetFiles(DumpLocation))
            ProcessedDumps.AddRange(JsonSerializer
                .Deserialize<List<PartialData>>(File.ReadAllText(file), JsonSerializerSettings.Default));

        return ProcessedDumps;
    }

    public void Clear()
    {
        lock (lockObject)
        {
            foreach (var file in Directory.GetFiles(DumpLocation)) File.Delete(file);

            ProcessedDumps.Clear();
        }
    }
}