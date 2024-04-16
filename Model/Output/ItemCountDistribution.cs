using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output
{
    public class ItemCountDistribution
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonProperty("relativeProbability", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("relativeProbability")]
        public int? RelativeProbability { get; set; }
    }
}