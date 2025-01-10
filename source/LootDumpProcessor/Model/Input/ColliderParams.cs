using System.Text.Json.Serialization;
using LootDumpProcessor.Model.Tarkov;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ColliderParams
    {
        [JsonProperty("Parent", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Parent")]
        public string? Parent { get; set; }

        [JsonProperty("_props", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("_props")]
        public Props? Props { get; set; }
    }
}