using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Foldable : ICloneable
    {
        [JsonProperty("Folded", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Folded")]
        public bool? Folded { get; set; }

        public object Clone()
        {
            return new Foldable
            {
                Folded = this.Folded
            };
        }
    }
}