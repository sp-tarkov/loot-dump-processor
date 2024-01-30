using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class BotLocationModifier
    {
        [JsonProperty("AccuracySpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AccuracySpeed")]
        public double? AccuracySpeed { get; set; }

        [JsonProperty("Scattering", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Scattering")]
        public double? Scattering { get; set; }

        [JsonProperty("GainSight", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("GainSight")]
        public double? GainSight { get; set; }

        [JsonProperty("MarksmanAccuratyCoef", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MarksmanAccuratyCoef")]
        public double? MarksmanAccuratyCoef { get; set; }

        [JsonProperty("VisibleDistance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("VisibleDistance")]
        public double? VisibleDistance { get; set; }
    }
}