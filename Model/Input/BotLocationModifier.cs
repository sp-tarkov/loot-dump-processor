using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class BotLocationModifier
    {
        [JsonProperty("AccuracySpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AccuracySpeed")]
        public float? AccuracySpeed { get; set; }

        [JsonProperty("Scattering", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Scattering")]
        public float? Scattering { get; set; }

        [JsonProperty("GainSight", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("GainSight")]
        public float? GainSight { get; set; }

        [JsonProperty("MarksmanAccuratyCoef", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MarksmanAccuratyCoef")]
        public float? MarksmanAccuratyCoef { get; set; }

        [JsonProperty("VisibleDistance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("VisibleDistance")]
        public float? VisibleDistance { get; set; }

        [JsonProperty("DistToPersueAxemanCoef", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DistToPersueAxemanCoef")]
        public float? DistToPersueAxemanCoef { get; set; }

        [JsonProperty("KhorovodChance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("KhorovodChance")]
        public int? KhorovodChance { get; set; }

        [JsonProperty("MinExfiltrationTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinExfiltrationTime")]
        public float? MinExfiltrationTime { get; set; }

        [JsonProperty("MaxExfiltrationTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxExfiltrationTime")]
        public float? MaxExfiltrationTime { get; set; }

        [JsonProperty("DistToActivatePvE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DistToActivatePvE")]
        public float? DistToActivatePvE { get; set; }

        [JsonProperty("DistToSleepPvE", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DistToSleepPvE")]
        public float? DistToSleepPvE { get; set; }

        [JsonProperty("DistToActivate", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DistToActivate")]
        public float? DistToActivate { get; set; }

        [JsonProperty("DistToSleep", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DistToSleep")]
        public float? DistToSleep { get; set; }

        [JsonProperty("AdditionalHostilitySettings", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AdditionalHostilitySettings")]
        public List<AdditionalHostilitySetting>? AdditionalHostilitySettings { get; set; }
    }
}