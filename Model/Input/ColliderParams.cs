using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ColliderParams
    {
        [JsonProperty("_parent", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("_parent")]
        public string? Parent { get; set; }

        [JsonProperty("_props", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("_props")]
        public Props? Props { get; set; }
    }
}