using System.Text.Json;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Output.LooseLoot;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process.Writer;

public class FileWriter : IWriter
{
    private static readonly string _outputPath;

    static FileWriter()
    {
        var path = LootDumpProcessorContext.GetConfig().WriterConfig.OutputLocation;
        if (string.IsNullOrEmpty(path))
            throw new Exception("Output directory must be set in WriterConfigs");

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        _outputPath = path;
    }

    public void WriteAll(Dictionary<OutputFileType, object> dumpData)
    {
        foreach (var (key, value) in dumpData) Write(key, value);
    }

    public void Write(
        OutputFileType type,
        object data
    )
    {
        if (!Directory.Exists($"{_outputPath}\\loot"))
            Directory.CreateDirectory($"{_outputPath}\\loot");
        switch (type)
        {
            case OutputFileType.LooseLoot:
                var looseLootData = (IReadOnlyDictionary<string, LooseLootRoot>)data;
                foreach (var (key, value) in looseLootData)
                {
                    if (!Directory.Exists($@"{_outputPath}\locations\{key}"))
                        Directory.CreateDirectory($@"{_outputPath}\locations\{key}");
                    File.WriteAllText($@"{_outputPath}\locations\{key}\looseLoot.json",
                        JsonSerializer.Serialize(value, JsonSerializerSettings.Default));
                }

                break;
            case OutputFileType.StaticContainer:
                var staticContainer = (IReadOnlyDictionary<string, MapStaticLoot>)data;
                foreach (var (key, value) in staticContainer)
                {
                    if (!Directory.Exists($@"{_outputPath}\locations\{key}"))
                        Directory.CreateDirectory($@"{_outputPath}\locations\{key}");
                    File.WriteAllText($@"{_outputPath}\locations\{key}\staticContainers.json",
                        JsonSerializer.Serialize(value, JsonSerializerSettings.Default));
                }

                break;
            case OutputFileType.StaticLoot:
                var staticLootData =
                    (IReadOnlyDictionary<string, IReadOnlyDictionary<string, StaticItemDistribution>>)data;
                foreach (var (key, value) in staticLootData)
                {
                    if (!Directory.Exists($@"{_outputPath}\locations\{key}"))
                        Directory.CreateDirectory($@"{_outputPath}\locations\{key}");
                    File.WriteAllText($@"{_outputPath}\locations\{key}\staticLoot.json",
                        JsonSerializer.Serialize(value, JsonSerializerSettings.Default));
                }

                break;
            case OutputFileType.StaticAmmo:
                var staticAmmo = (IReadOnlyDictionary<string, IReadOnlyDictionary<string, List<AmmoDistribution>>>)data;
                foreach (var (key, value) in staticAmmo)
                {
                    if (!Directory.Exists($@"{_outputPath}\locations\{key}"))
                        Directory.CreateDirectory($@"{_outputPath}\locations\{key}");
                    File.WriteAllText($@"{_outputPath}\locations\{key}\staticAmmo.json",
                        JsonSerializer.Serialize(value, JsonSerializerSettings.Default));
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}