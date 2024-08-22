using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ServerMapBTRSettings
    {
        [JsonProperty("Develop", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Develop")]
        public MapSettings? Develop { get; set; }

        [JsonProperty("TarkovStreets", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TarkovStreets")]
        public MapSettings? TarkovStreets { get; set; }

        [JsonProperty("Woods", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Woods")]
        public MapSettings? Woods { get; set; }
    }
}