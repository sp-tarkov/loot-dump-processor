using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Vector3 : ICloneable
    {
        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("x")]
        public float? X { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("y")]
        public float? Y { get; set; }

        [JsonProperty("z", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("z")]
        public float? Z { get; set; }

        public object Clone()
        {
            return new Vector3
            {
                X = X,
                Y = Y,
                Z = Z
            };
        }
    }
}