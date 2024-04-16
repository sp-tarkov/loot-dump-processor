using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output.LooseLoot
{
    public class SpawnPointCount
    {
        [JsonProperty("mean", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("mean")]
        public double? Mean { get; set; }

        [JsonProperty("std", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("std")]
        public double? Std { get; set; }
    }
}