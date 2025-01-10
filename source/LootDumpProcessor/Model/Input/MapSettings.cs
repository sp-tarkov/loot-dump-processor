using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class MapSettings
    {
        [JsonProperty("MapID", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MapID")]
        public string? MapID { get; set; }

        [JsonProperty("ChanceSpawn", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ChanceSpawn")]
        public float? ChanceSpawn { get; set; }

        [JsonProperty("SpawnPeriod", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("SpawnPeriod")]
        public Vector3? SpawnPeriod { get; set; }

        [JsonProperty("MoveSpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MoveSpeed")]
        public float? MoveSpeed { get; set; }

        [JsonProperty("ReadyToDepartureTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ReadyToDepartureTime")]
        public float? ReadyToDepartureTime { get; set; }

        [JsonProperty("CheckTurnDistanceTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("CheckTurnDistanceTime")]
        public float? CheckTurnDistanceTime { get; set; }

        [JsonProperty("TurnCheckSensitivity", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TurnCheckSensitivity")]
        public float? TurnCheckSensitivity { get; set; }

        [JsonProperty("DecreaseSpeedOnTurnLimit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DecreaseSpeedOnTurnLimit")]
        public float? DecreaseSpeedOnTurnLimit { get; set; }

        [JsonProperty("EndSplineDecelerationDistance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EndSplineDecelerationDistance")]
        public float? EndSplineDecelerationDistance { get; set; }

        [JsonProperty("AccelerationSpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AccelerationSpeed")]
        public float? AccelerationSpeed { get; set; }

        [JsonProperty("DecelerationSpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DecelerationSpeed")]
        public float? DecelerationSpeed { get; set; }

        [JsonProperty("PauseDurationRange", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PauseDurationRange")]
        public Vector3? PauseDurationRange { get; set; }

        [JsonProperty("BodySwingReturnSpeed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BodySwingReturnSpeed")]
        public float? BodySwingReturnSpeed { get; set; }

        [JsonProperty("BodySwingDamping", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BodySwingDamping")]
        public float? BodySwingDamping { get; set; }

        [JsonProperty("BodySwingIntensity", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BodySwingIntensity")]
        public float? BodySwingIntensity { get; set; }
    }
}