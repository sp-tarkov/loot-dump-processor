using System.Collections.Frozen;
using JetBrains.Annotations;
using LootDumpProcessor.Model.Output.StaticContainer;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public class ForcedStatic(
    IReadOnlyList<string> staticWeaponIds, FrozenDictionary<string, IReadOnlyList<StaticForced>> forcedItems
)
{
    public readonly IReadOnlyList<string> StaticWeaponIds = staticWeaponIds;
    public readonly FrozenDictionary<string, IReadOnlyList<StaticForced>> ForcedItems = forcedItems;
}