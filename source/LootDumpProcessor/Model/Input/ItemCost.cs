using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ItemCost
    {
        [JsonProperty("Count", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Count")]
        public int? Count { get; set; }
    }
}