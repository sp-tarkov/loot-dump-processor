using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output.LooseLoot
{
    public class SpawnPointsForced
    {
        [JsonProperty("locationId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("locationId")]
        public string? LocationId { get; set; }

        [JsonProperty("probability", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("probability")]
        public double? Probability { get; set; }

        [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("template")]
        public Template? Template { get; set; }
    }
}