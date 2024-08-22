using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class MatchMakerMinPlayersByWaitTime
    {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("time")]
        public int? Time { get; set; }

        [JsonProperty("minPlayers", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("minPlayers")]
        public int? MinPlayers { get; set; }
    }
}