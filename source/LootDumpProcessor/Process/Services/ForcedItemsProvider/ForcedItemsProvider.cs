using System.Collections.Frozen;
using System.Collections.Immutable;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Serializers.Yaml;

namespace LootDumpProcessor.Process.Services.ForcedItemsProvider;

public class ForcedItemsProvider : IForcedItemsProvider
{
    private static readonly string ForcedStaticPath = Path.Combine("Config", "forced_static.yaml");
    private static readonly string ForcedLooseLootPath = Path.Combine("Config", "forced_loose.yaml");

    private ForcedStatic? _forcedStatic;
    private FrozenDictionary<string, ImmutableHashSet<string>>? _forcedLoose;

    public async Task<ForcedStatic> GetForcedStatic()
    {
        if (_forcedStatic is not null) return _forcedStatic;
        _forcedStatic = await ReadForcedStatic();
        return _forcedStatic;
    }

    private async Task<ForcedStatic> ReadForcedStatic()
    {
        var forcedStaticContent = await File.ReadAllTextAsync(ForcedStaticPath);

        // Workaround needed because YamlDotNet cannot deserialize properly
        var forcedStaticDto = Yaml.Deserializer.Deserialize<ForcedStaticDto>(forcedStaticContent);
        var forcedStatic = new ForcedStatic(
            forcedStaticDto.StaticWeaponIds.AsReadOnly(),
            forcedStaticDto.ForcedItems.ToFrozenDictionary(
                kvp => kvp.Key,
                IReadOnlyList<StaticForced> (kvp) => kvp.Value.AsReadOnly()
            ));

        return forcedStatic;
    }

    public async Task<FrozenDictionary<string, ImmutableHashSet<string>>> GetForcedLooseItems()
    {
        if (_forcedLoose is not null) return _forcedLoose;
        _forcedLoose = await ReadForcedLooseItems();
        return _forcedLoose;
    }

    private async Task<FrozenDictionary<string, ImmutableHashSet<string>>> ReadForcedLooseItems()
    {
        var forcedLooseContent = await File.ReadAllTextAsync(ForcedLooseLootPath);
        var forcedLooseLoot = Yaml.Deserializer.Deserialize<Dictionary<string, HashSet<string>>>(forcedLooseContent);
        return forcedLooseLoot.ToFrozenDictionary(
            pair => pair.Key,
            pair => ImmutableHashSet.CreateRange(pair.Value)
        );
    }
}