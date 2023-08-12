using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output;

public abstract class AbstractDistribution
{
    [JsonProperty("relativeProbability", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("relativeProbability")]
    public int? RelativeProbability { get; set; }
}