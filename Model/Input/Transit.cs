using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Transit
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonProperty("activateAfterSec", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("activateAfterSec")]
        public int? ActivateAfterSec { get; set; }

        [JsonProperty("target", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("target")]
        public string? Target { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("time")]
        public int? Time { get; set; }

        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("conditions")]
        public string? Conditions { get; set; }
    }
}