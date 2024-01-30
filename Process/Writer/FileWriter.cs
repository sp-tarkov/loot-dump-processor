using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Output.LooseLoot;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process.Writer;

public class FileWriter : IWriter
{
    private static readonly IJsonSerializer _jsonSerializer = JsonSerializerFactory.GetInstance();
    private static readonly string _outputPath;

    static FileWriter()
    {
        var path = LootDumpProcessorContext.GetConfig().WriterConfig.OutputLocation;
        if (string.IsNullOrEmpty(path))
            throw new Exception("Output directory must be set in WriterConfigs");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        _outputPath = path;
    }

    public void WriteAll(Dictionary<OutputFileType, object> dumpData)
    {
        foreach (var (key, value) in dumpData)
        {
            Write(key, value);
        }
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
                var looseLootData = (Dictionary<string, LooseLootRoot>)data;
                foreach (var (key, value) in looseLootData)
                {
                    foreach (var s in LootDumpProcessorContext.GetDirectoryMappings()[key].Name)
                    {
                        if (!Directory.Exists($@"{_outputPath}\locations\{s}"))
                            Directory.CreateDirectory($@"{_outputPath}\locations\{s}");
                        File.WriteAllText($@"{_outputPath}\locations\{s}\looseLoot.json",
                            _jsonSerializer.Serialize(value));
                    }
                }

                break;
            case OutputFileType.StaticContainer:
                var staticContainer = (Dictionary<string, MapStaticLoot>)data;
                File.WriteAllText($@"{_outputPath}\loot\staticContainers.json",
                    _jsonSerializer.Serialize(staticContainer));
                break;
            case OutputFileType.StaticLoot:
                var staticLoot = (Dictionary<string, StaticItemDistribution>)data;
                File.WriteAllText($@"{_outputPath}\loot\staticLoot.json",
                    _jsonSerializer.Serialize(staticLoot));
                break;
            case OutputFileType.StaticAmmo:
                var staticAmmo = (Dictionary<string, List<AmmoDistribution>>)data;
                File.WriteAllText($@"{_outputPath}\loot\staticAmmo.json",
                    _jsonSerializer.Serialize(staticAmmo));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}