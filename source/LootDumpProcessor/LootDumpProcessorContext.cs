using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Output.StaticContainer;
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