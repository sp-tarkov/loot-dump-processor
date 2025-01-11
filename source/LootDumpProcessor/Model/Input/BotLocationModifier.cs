namespace LootDumpProcessor.Model.Input;

public readonly record struct BotLocationModifier(
    float AccuracySpeed,
    float Scattering,
    float GainSight,
    float MarksmanAccuratyCoef,
    float VisibleDistance,
    float DistToPersueAxemanCoef,
    int KhorovodChance,
    float MinExfiltrationTime,
    float MaxExfiltrationTime,
    float DistToActivatePvE,
    float DistToSleepPvE,
    float DistToActivate,
    float DistToSleep,
    IReadOnlyList<AdditionalHostilitySetting> AdditionalHostilitySettings
);