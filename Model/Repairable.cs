using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Repairable : ICloneable
    {
        [JsonProperty("Durability", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Durability")]
        public int? Durability { get; set; }

        [JsonProperty("MaxDurability", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxDurability")]
        public int? MaxDurability { get; set; }

        public object Clone()
        {
            return new Repairable
            {
                Durability = Durability,
                MaxDurability = MaxDurability
            };
        }
    }
}