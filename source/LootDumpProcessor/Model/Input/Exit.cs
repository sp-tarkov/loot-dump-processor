namespace LootDumpProcessor.Model.Input;

public readonly record struct Exit(
    string Name,
    string EntryPoints,
    float Chance,
    int MinTime,
    int MaxTime,
    int PlayersCount,
    float ExfiltrationTime,
    string PassageRequirement,
    string ExfiltrationType,
    string RequiredSlot,
    string Id,
    string RequirementTip,
    int Count,
    bool EventAvailable,
    int MinTimePve,
    int MaxTimePve,
    float ChancePve,
    int CountPve,
    float ExfiltrationTimePve,
    int PlayersCountPve
);