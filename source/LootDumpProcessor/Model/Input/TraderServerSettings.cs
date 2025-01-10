using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class TraderServerSettings
    {
        [JsonProperty("TraderServices", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TraderServices")]
        public TraderServices? TraderServices { get; set; }
    }
}