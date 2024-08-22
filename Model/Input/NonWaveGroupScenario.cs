using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class NonWaveGroupScenario
    {
        [JsonProperty("MinToBeGroup", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinToBeGroup")]
        public int? MinToBeGroup { get; set; }

        [JsonProperty("MaxToBeGroup", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxToBeGroup")]
        public int? MaxToBeGroup { get; set; }

        [JsonProperty("Chance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Chance")]
        public float? Chance { get; set; }

        [JsonProperty("Enabled", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Enabled")]
        public bool? Enabled { get; set; }
    }
}