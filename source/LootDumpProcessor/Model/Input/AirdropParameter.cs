namespace LootDumpProcessor.Model.Input;

public readonly record struct AirdropParameter(
    int PlaneAirdropStartMin,
    int PlaneAirdropStartMax,
    int PlaneAirdropEnd,
    float PlaneAirdropChance,
    int PlaneAirdropMax,
    int PlaneAirdropCooldownMin,
    int PlaneAirdropCooldownMax,
    int AirdropPointDeactivateDistance,
    int MinPlayersCountToSpawnAirdrop,
    int UnsuccessfulTryPenalty
);