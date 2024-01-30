using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output
{
    public class StaticItemDistribution
    {
        [JsonProperty("itemcountDistribution", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("itemcountDistribution")]
        public List<ItemCountDistribution>? ItemCountDistribution { get; set; }

        [JsonProperty("itemDistribution", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("itemDistribution")]
        public List<StaticDistribution>? ItemDistribution { get; set; }
    }
}