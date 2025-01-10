using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Banner
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonProperty("pic", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("pic")]
        public Pic? Pic { get; set; }
    }
}