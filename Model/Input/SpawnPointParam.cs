using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class SpawnPointParam
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonProperty("Position", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Position")]
        public Vector3? Position { get; set; }

        [JsonProperty("Rotation", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Rotation")]
        public double? Rotation { get; set; }

        [JsonProperty("Sides", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Sides")]
        public List<string>? Sides { get; set; }

        [JsonProperty("Categories", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Categories")]
        public List<string>? Categories { get; set; }

        [JsonProperty("Infiltration", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Infiltration")]
        public string? Infiltration { get; set; }

        [JsonProperty("DelayToCanSpawnSec", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DelayToCanSpawnSec")]
        public int? DelayToCanSpawnSec { get; set; }

        [JsonProperty("ColliderParams", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ColliderParams")]
        public ColliderParams? ColliderParams { get; set; }
    }
}