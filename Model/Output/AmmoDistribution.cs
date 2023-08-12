using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output;

public class AmmoDistribution : AbstractDistribution
{
    [JsonProperty("tpl", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("tpl")]
    public string? Tpl { get; set; }
}