using System.Collections.Frozen;
using JetBrains.Annotations;
using LootDumpProcessor.Model.Output.StaticContainer;
using YamlDotNet.Serialization;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record ForcedStatic
{
    [YamlMember(Alias = "static_weapon_ids")]
    public readonly IReadOnlyList<string> StaticWeaponIds = Array.Empty<string>();

    [YamlMember(Alias = "forced_items")]
    public readonly FrozenDictionary<string, IReadOnlyList<StaticForced>> ForcedItems =
        FrozenDictionary<string, IReadOnlyList<StaticForced>>.Empty;
}