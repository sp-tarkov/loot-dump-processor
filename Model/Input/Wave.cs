using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Wave
    {
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("number")]
        public int? Number { get; set; }

        [JsonProperty("time_min", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("time_min")]
        public int? TimeMin { get; set; }

        [JsonProperty("time_max", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("time_max")]
        public int? TimeMax { get; set; }

        [JsonProperty("slots_min", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("slots_min")]
        public int? SlotsMin { get; set; }

        [JsonProperty("slots_max", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("slots_max")]
        public int? SlotsMax { get; set; }

        [JsonProperty("SpawnPoints", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("SpawnPoints")]
        public string? SpawnPoints { get; set; }

        [JsonProperty("BotSide", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotSide")]
        public string? BotSide { get; set; }

        [JsonProperty("BotPreset", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotPreset")]
        public string? BotPreset { get; set; }

        [JsonProperty("isPlayers", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("isPlayers")]
        public bool? IsPlayers { get; set; }

        [JsonProperty("WildSpawnType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("WildSpawnType")]
        public string? WildSpawnType { get; set; }
    }
}