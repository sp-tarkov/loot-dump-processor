using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class RootData
    {
        [JsonProperty("err", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("err")]
        public int? Err { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("data")]
        public required Data Data { get; set; }

        [JsonProperty("errmsg", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("errmsg")]
        public object? Errmsg { get; set; }
    }
}