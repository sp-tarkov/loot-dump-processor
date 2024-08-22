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
        public float? Chance { get; set; }

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
        public float? ExfiltrationTime { get; set; }

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

        [JsonProperty("EventAvailable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EventAvailable")]
        public bool? EventAvailable { get; set; }

        [JsonProperty("MinTimePVE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinTimePVE")]
        public int? MinTimePVE { get; set; }

        [JsonProperty("MaxTimePVE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxTimePVE")]
        public int? MaxTimePVE { get; set; }

        [JsonProperty("ChancePVE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ChancePVE")]
        public float? ChancePVE { get; set; }

        [JsonProperty("CountPVE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("CountPVE")]
        public int? CountPVE { get; set; }

        [JsonProperty("ExfiltrationTimePVE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ExfiltrationTimePVE")]
        public float? ExfiltrationTimePVE { get; set; }

        [JsonProperty("PlayersCountPVE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlayersCountPVE")]
        public int? PlayersCountPVE { get; set; }
    }
}