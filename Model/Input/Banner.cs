using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Banner
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)] [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonProperty("pic")] [JsonPropertyName("pic")]
        public Preview? Pic { get; set; }
    }
}
