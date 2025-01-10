using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Requirements
    {
        [JsonProperty("CompletedQuests", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("CompletedQuests")]
        public List<CompletedQuest>? CompletedQuests { get; set; }

        [JsonProperty("Standings", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Standings")]
        public object? Standings { get; set; }
    }
}