namespace LootDumpProcessor.Model.Input;

public readonly record struct BTRServerSettings(
    float ChanceSpawn,
    Vector3 SpawnPeriod,
    float MoveSpeed,
    float ReadyToDepartureTime,
    float CheckTurnDistanceTime,
    float TurnCheckSensitivity,
    float DecreaseSpeedOnTurnLimit,
    float EndSplineDecelerationDistance,
    float AccelerationSpeed,
    float DecelerationSpeed,
    Vector3 PauseDurationRange,
    float BodySwingReturnSpeed,
    float BodySwingDamping,
    float BodySwingIntensity,
    ServerMapBTRSettings ServerMapBTRSettings
);