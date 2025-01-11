using System.Text.Json;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process.Collector;

public class DumpCollector : ICollector
{
    private static readonly string DumpLocation =
        $"{LootDumpProcessorContext.GetConfig().CollectorConfig.DumpLocation}/collector/";

    private readonly List<PartialData> processedDumps =
        new(LootDumpProcessorContext.GetConfig().CollectorConfig.MaxEntitiesBeforeDumping + 50);

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
            processedDumps.Add(parsedDump);
            if (processedDumps.Count > LootDumpProcessorContext.GetConfig().CollectorConfig.MaxEntitiesBeforeDumping)
            {
                var fileName = $"collector-{DateTime.Now.ToString("yyyyMMddHHmmssfffff")}.json";
                File.WriteAllText($"{DumpLocation}{fileName}",
                    JsonSerializer.Serialize(processedDumps, JsonSerializerSettings.Default));
                processedDumps.Clear();
            }
        }
    }

    public List<PartialData> Retrieve()
    {
        foreach (var file in Directory.GetFiles(DumpLocation))
            processedDumps.AddRange(JsonSerializer
                .Deserialize<List<PartialData>>(File.ReadAllText(file), JsonSerializerSettings.Default));

        return processedDumps;
    }

    public void Clear()
    {
        lock (lockObject)
        {
            foreach (var file in Directory.GetFiles(DumpLocation)) File.Delete(file);

            processedDumps.Clear();
        }
    }
}