using JetBrains.Annotations;
using LootDumpProcessor.Model.Output.StaticContainer;
using YamlDotNet.Serialization;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public class ForcedStaticDto
{
    [YamlMember(Alias = "static_weapon_ids")] public List<string> StaticWeaponIds { get; set; }

    [YamlMember(Alias = "forced_items")] public Dictionary<string, List<StaticForced>> ForcedItems { get; set; }
}