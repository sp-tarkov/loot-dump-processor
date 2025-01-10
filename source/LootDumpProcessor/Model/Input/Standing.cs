using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Standing
    {
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Value")]
        public float? Value { get; set; }
    }
}