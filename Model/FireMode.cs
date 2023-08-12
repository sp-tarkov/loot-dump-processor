using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class FireMode : ICloneable
    {
        [JsonProperty("FireMode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("FireMode")]
        public string? FireModeType { get; set; }

        public object Clone()
        {
            return new FireMode
            {
                FireModeType = this.FireModeType
            };
        }
    }
}