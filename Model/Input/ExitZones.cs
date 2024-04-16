using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ExitZones
    {
        [JsonProperty("$oid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("$oid")]
        public string? Oid { get; set; }
    }
}