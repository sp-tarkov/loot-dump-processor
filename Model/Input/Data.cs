using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Data
    {
        [JsonProperty("serverId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("serverId")]
        public string? ServerID { get; set; }

        [JsonProperty("serverSettings", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("serverSettings")]
        public ServerSettings? ServerSettings { get; set; }

        [JsonProperty("profile", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("profile")]
        public object? Profile { get; set; }

        [JsonProperty("locationLoot", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("locationLoot")]
        public required LocationLoot LocationLoot { get; set; }
    }
}