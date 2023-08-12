using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output;

public class StaticDistribution : AbstractDistribution
{
    [JsonProperty("tpl", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("tpl")]
    public string? Tpl { get; set; }
}