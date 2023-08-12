using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Vector3 : ICloneable
    {
        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("x")]
        public double? X { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("y")]
        public double? Y { get; set; }

        [JsonProperty("z", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("z")]
        public double? Z { get; set; }

        public object Clone()
        {
            return new Vector3
            {
                X = this.X,
                Y = this.Y,
                Z = this.Z
            };
        }
    }
}