using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output
{
    public class StaticItemDistribution
    {
        [JsonProperty("itemcountDistribution", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("itemcountDistribution")]
        public IReadOnlyList<ItemCountDistribution>? ItemCountDistribution { get; set; }

        [JsonProperty("itemDistribution", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("itemDistribution")]
        public IReadOnlyList<StaticDistribution>? ItemDistribution { get; set; }
    }
}