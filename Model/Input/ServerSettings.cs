using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ServerSettings
    {
        [JsonProperty("TraderServerSettings", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TraderServerSettings")]
        public TraderServerSettings? TraderServerSettings { get; set; }

        [JsonProperty("BTRServerSettings", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BTRServerSettings")]
        public BTRServerSettings? BTRServerSettings { get; set; }
    }
}