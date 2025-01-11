namespace LootDumpProcessor.Model.Input;

public class AirdropParameter
{
    public int? PlaneAirdropStartMin { get; set; }
    public int? PlaneAirdropStartMax { get; set; }
    public int? PlaneAirdropEnd { get; set; }
    public float? PlaneAirdropChance { get; set; }
    public int? PlaneAirdropMax { get; set; }
    public int? PlaneAirdropCooldownMin { get; set; }
    public int? PlaneAirdropCooldownMax { get; set; }
    public int? AirdropPointDeactivateDistance { get; set; }
    public int? MinPlayersCountToSpawnAirdrop { get; set; }
    public int? UnsuccessfulTryPenalty { get; set; }
}