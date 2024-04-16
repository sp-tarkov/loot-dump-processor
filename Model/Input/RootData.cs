using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class RootData
    {
        [JsonProperty("err", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("err")]
        public int? Err { get; set; }

        [JsonProperty("errmsg")]
        [JsonPropertyName("errmsg")]
        public object? errmsg { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
    }
}