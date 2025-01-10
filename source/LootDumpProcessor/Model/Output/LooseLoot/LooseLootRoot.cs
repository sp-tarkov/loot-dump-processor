using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output.LooseLoot
{
    public class LooseLootRoot
    {
        [JsonProperty("spawnpointCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("spawnpointCount")]
        public SpawnPointCount? SpawnPointCount { get; set; }

        [JsonProperty("spawnpointsForced", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("spawnpointsForced")]
        public List<SpawnPointsForced>? SpawnPointsForced { get; set; }

        [JsonProperty("spawnpoints", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("spawnpoints")]
        public List<SpawnPoint>? SpawnPoints { get; set; }
    }
}