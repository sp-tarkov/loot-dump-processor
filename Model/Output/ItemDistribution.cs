using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output
{
    public class ItemDistribution : AbstractDistribution
    {
        [JsonProperty("composedKey", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("composedKey")]
        public ComposedKey? ComposedKey { get; set; }
    }
}