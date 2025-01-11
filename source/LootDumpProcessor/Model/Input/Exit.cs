namespace LootDumpProcessor.Model.Input;

public class Exit
{
    public string? Name { get; set; }
    public string? EntryPoints { get; set; }
    public float? Chance { get; set; }
    public int? MinTime { get; set; }
    public int? MaxTime { get; set; }
    public int? PlayersCount { get; set; }
    public float? ExfiltrationTime { get; set; }
    public string? PassageRequirement { get; set; }
    public string? ExfiltrationType { get; set; }
    public string? RequiredSlot { get; set; }
    public string? Id { get; set; }
    public string? RequirementTip { get; set; }
    public int? Count { get; set; }
    public bool? EventAvailable { get; set; }
    public int? MinTimePVE { get; set; }
    public int? MaxTimePVE { get; set; }
    public float? ChancePVE { get; set; }
    public int? CountPVE { get; set; }
    public float? ExfiltrationTimePVE { get; set; }
    public int? PlayersCountPVE { get; set; }
}