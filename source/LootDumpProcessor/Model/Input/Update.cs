using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Update
    {
        [JsonProperty("StackObjectsCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("StackObjectsCount")]
        public int? StackObjectsCount { get; set; }
    }
}