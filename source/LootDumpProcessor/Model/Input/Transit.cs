namespace LootDumpProcessor.Model.Input;

public readonly record struct Transit(
    int Id, bool Active, string Name, string Location, string Description, int ActivateAfterSec, string Target,
    int Time, string Conditions
);