using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class TraderService
    {
        [JsonProperty("TraderId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TraderId")]
        public string? TraderID { get; set; }

        [JsonProperty("TraderServiceType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TraderServiceType")]
        public string? TraderServiceType { get; set; }

        [JsonProperty("Requirements", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Requirements")]
        public Requirements? Requirements { get; set; }

        [JsonProperty("ServiceItemCost", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ServiceItemCost")]
        public object? ServiceItemCost { get; set; }

        [JsonProperty("UniqueItems", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("UniqueItems")]
        public List<object>? UniqueItems { get; set; }
    }
}