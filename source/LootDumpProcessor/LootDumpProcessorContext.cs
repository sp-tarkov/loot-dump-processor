using System.Text.Json;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Serializers.Yaml;

namespace LootDumpProcessor;

public static class LootDumpProcessorContext
{
    private static Config? _config;
    private static readonly object _configLock = new();
    private static ForcedStatic? _forcedStatic;
    private static readonly object _forcedStaticLock = new();
    private static HashSet<string>? _staticWeaponIds;
    private static readonly object _staticWeaponIdsLock = new();
    private static Dictionary<string, List<StaticForced>>? _forcedItems;
    private static readonly object _forcedItemsLock = new();
    private static Dictionary<string, HashSet<string>>? _forcedLoose;
    private static readonly object _forcedLooseLock = new();

    public static Config GetConfig()
    {
        lock (_configLock)
        {
            if (_config == null)
                // This is the only instance where manual selection of the serializer is required
                // after this, GetInstance() for the JsonSerializerFactory should used without
                // parameters
                _config = JsonSerializer.Deserialize<Config>(File.ReadAllText("./Config/config.json"),
                    JsonSerializerSettings.Default);
        }

        return _config;
    }

    public static Dictionary<string, HashSet<string>> GetForcedLooseItems()
    {
        lock (_forcedLooseLock)
        {
            if (_forcedLoose == null)
                _forcedLoose = Yaml.Deserializer.Deserialize<Dictionary<string, HashSet<string>>>(
                    File.ReadAllText("./Config/forced_loose.yaml"));
        }

        return _forcedLoose;
    }
}