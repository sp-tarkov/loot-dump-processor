using System.Text.Json.Serialization;
using LootDumpProcessor.Model.Tarkov;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Props
    {
        [JsonProperty("Center", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Center")]
        public Vector3? Center { get; set; }

        [JsonProperty("Radius", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Radius")]
        public float? Radius { get; set; }
    }
}