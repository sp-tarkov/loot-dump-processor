using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Exit
    {
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonProperty("EntryPoints", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EntryPoints")]
        public string? EntryPoints { get; set; }

        [JsonProperty("Chance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Chance")]
        public int? Chance { get; set; }

        [JsonProperty("MinTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinTime")]
        public int? MinTime { get; set; }

        [JsonProperty("MaxTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxTime")]
        public int? MaxTime { get; set; }

        [JsonProperty("PlayersCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlayersCount")]
        public int? PlayersCount { get; set; }

        [JsonProperty("ExfiltrationTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ExfiltrationTime")]
        public int? ExfiltrationTime { get; set; }

        [JsonProperty("PassageRequirement", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PassageRequirement")]
        public string? PassageRequirement { get; set; }

        [JsonProperty("ExfiltrationType", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ExfiltrationType")]
        public string? ExfiltrationType { get; set; }

        [JsonProperty("RequiredSlot", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("RequiredSlot")]
        public string? RequiredSlot { get; set; }

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonProperty("RequirementTip", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("RequirementTip")]
        public string? RequirementTip { get; set; }

        [JsonProperty("Count", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Count")]
        public int? Count { get; set; }
    }
}