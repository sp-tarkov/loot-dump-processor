namespace LootDumpProcessor.Model.Input;

public class BTRServerSettings
{
    public float? ChanceSpawn { get; set; }
    public Vector3? SpawnPeriod { get; set; }
    public float? MoveSpeed { get; set; }
    public float? ReadyToDepartureTime { get; set; }
    public float? CheckTurnDistanceTime { get; set; }
    public float? TurnCheckSensitivity { get; set; }
    public float? DecreaseSpeedOnTurnLimit { get; set; }
    public float? EndSplineDecelerationDistance { get; set; }
    public float? AccelerationSpeed { get; set; }
    public float? DecelerationSpeed { get; set; }
    public Vector3? PauseDurationRange { get; set; }
    public float? BodySwingReturnSpeed { get; set; }
    public float? BodySwingDamping { get; set; }
    public float? BodySwingIntensity { get; set; }
    public ServerMapBTRSettings? ServerMapBTRSettings { get; set; }
}