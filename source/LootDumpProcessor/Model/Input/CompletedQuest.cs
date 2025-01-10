using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class CompletedQuest
    {
        [JsonProperty("QuestId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("QuestId")]
        public string? QuestID { get; set; }
    }
}