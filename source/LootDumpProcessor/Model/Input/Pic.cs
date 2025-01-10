using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Pic
    {
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonProperty("rcid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("rcid")]
        public string? Rcid { get; set; }
    }
}