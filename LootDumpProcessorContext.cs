using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Process;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Serializers.Yaml;

namespace LootDumpProcessor;

public static class LootDumpProcessorContext
{
    private static Config? _config;
    private static readonly object _configLock = new();
    private static ForcedStatic? _forcedStatic;
    private static readonly object _forcedStaticLock = new();
    private static Dictionary<string, MapDirectoryMapping>? _mapDirectoryMappings;
    private static readonly object _mapDirectoryMappingsLock = new();
    private static HashSet<string>? _staticWeaponIds;
    private static readonly object _staticWeaponIdsLock = new();
    private static Dictionary<string, List<StaticForced>>? _forcedItems;
    private static readonly object _forcedItemsLock = new();
    private static Dictionary<string, HashSet<string>>? _forcedLoose;
    private static readonly object _forcedLooseLock = new();
    private static TarkovItems? _tarkovItems;
    private static readonly object _tarkovItemsLock = new();

    public static Config GetConfig()
    {
        lock (_configLock)
        {
            if (_config == null)
            {
                // This is the only instance where manual selection of the serializer is required
                // after this, GetInstance() for the JsonSerializerFactory should used without
                // parameters
                _config = JsonSerializerFactory.GetInstance(JsonSerializerTypes.DotNet)
                    .Deserialize<Config>(File.ReadAllText("./Config/config.json"));
            }
        }

        return _config;
    }

    public static ForcedStatic GetForcedStatic()
    {
        lock (_forcedStaticLock)
        {
            if (_forcedStatic == null)
            {
                _forcedStatic = YamlSerializerFactory.GetInstance()
                    .Deserialize<ForcedStatic>(File.ReadAllText("./Config/forced_static.yaml"));
            }
        }

        return _forcedStatic;
    }

    /// <summary>
    /// Not Used
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, MapDirectoryMapping> GetDirectoryMappings()
    {
        lock (_mapDirectoryMappingsLock)
        {
            if (_mapDirectoryMappings == null)
            {
                _mapDirectoryMappings = YamlSerializerFactory.GetInstance()
                    .Deserialize<Dictionary<string, MapDirectoryMapping>>(
                        File.ReadAllText("./Config/map_directory_mapping.yaml"));
            }
        }

        return _mapDirectoryMappings;
    }

    public static HashSet<string> GetStaticWeaponIds()
    {
        lock (_staticWeaponIdsLock)
        {
            if (_staticWeaponIds == null)
            {
                _staticWeaponIds = GetForcedStatic().StaticWeaponIds.ToHashSet();
            }
        }

        return _staticWeaponIds;
    }

    public static Dictionary<string, List<StaticForced>> GetForcedItems()
    {
        lock (_forcedItemsLock)
        {
            if (_forcedItems == null)
            {
                _forcedItems = GetForcedStatic().ForcedItems;
            }
        }

        return _forcedItems;
    }

    public static Dictionary<string, HashSet<string>> GetForcedLooseItems()
    {
        lock (_forcedLooseLock)
        {
            if (_forcedLoose == null)
            {
                _forcedLoose = YamlSerializerFactory.GetInstance().Deserialize<Dictionary<string, HashSet<string>>>(
                    File.ReadAllText("./Config/forced_loose.yaml"));
            }
        }

        return _forcedLoose;
    }

    public static TarkovItems GetTarkovItems()
    {
        lock (_tarkovItemsLock)
        {
            if (_tarkovItems == null)
            {
                _tarkovItems = new TarkovItems(
                    $"{GetConfig().ServerItemsJsonLocation}"
                );
            }
        }

        return _tarkovItems;
    }
}