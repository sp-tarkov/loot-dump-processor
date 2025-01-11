using YamlDotNet.Serialization;

namespace LootDumpProcessor.Model.Output.StaticContainer;

public class StaticForced
{
    [YamlMember(Alias = "containerId")] public string? ContainerId { get; set; }


    [YamlMember(Alias = "itemTpl")] public string? ItemTpl { get; set; }
}