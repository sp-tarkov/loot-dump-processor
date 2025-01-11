namespace LootDumpProcessor.Model.Input;

public readonly record struct SpawnPointParam(
    string Id, Vector3 Position, float Rotation, IReadOnlyList<string> Sides, IReadOnlyList<string> Categories,
    string Infiltration,
    float DelayToCanSpawnSec, ColliderParams ColliderParams, string BotZoneName, int CorePointId
);