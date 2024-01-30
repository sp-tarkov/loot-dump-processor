using System.Text.Json.Serialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace LootDumpProcessor.Model.Output.StaticContainer
{
    public class StaticForced
    {
        [JsonProperty("containerId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("containerId")]
        [YamlMember(Alias = "containerId")]
        public string? ContainerId { get; set; }

        [JsonProperty("itemTpl", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("itemTpl")]
        [YamlMember(Alias = "itemTpl")]
        public string? ItemTpl { get; set; }
    }
}