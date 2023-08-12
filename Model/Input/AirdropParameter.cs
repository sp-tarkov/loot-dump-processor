using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class AirdropParameter
    {
        [JsonProperty("PlaneAirdropStartMin", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropStartMin")]
        public int? PlaneAirdropStartMin { get; set; }

        [JsonProperty("PlaneAirdropStartMax", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropStartMax")]
        public int? PlaneAirdropStartMax { get; set; }

        [JsonProperty("PlaneAirdropEnd", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropEnd")]
        public int? PlaneAirdropEnd { get; set; }

        [JsonProperty("PlaneAirdropChance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropChance")]
        public double? PlaneAirdropChance { get; set; }

        [JsonProperty("PlaneAirdropMax", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropMax")]
        public int? PlaneAirdropMax { get; set; }

        [JsonProperty("PlaneAirdropCooldownMin", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropCooldownMin")]
        public int? PlaneAirdropCooldownMin { get; set; }

        [JsonProperty("PlaneAirdropCooldownMax", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlaneAirdropCooldownMax")]
        public int? PlaneAirdropCooldownMax { get; set; }

        [JsonProperty("AirdropPointDeactivateDistance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AirdropPointDeactivateDistance")]
        public int? AirdropPointDeactivateDistance { get; set; }

        [JsonProperty("MinPlayersCountToSpawnAirdrop", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinPlayersCountToSpawnAirdrop")]
        public int? MinPlayersCountToSpawnAirdrop { get; set; }

        [JsonProperty("UnsuccessfulTryPenalty", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("UnsuccessfulTryPenalty")]
        public int? UnsuccessfulTryPenalty { get; set; }
    }
}