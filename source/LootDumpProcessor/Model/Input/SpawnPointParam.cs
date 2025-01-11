namespace LootDumpProcessor.Model.Input;

public class SpawnPointParam
{
    public string? Id { get; set; }


    public Vector3? Position { get; set; }


    public float? Rotation { get; set; }


    public List<string>? Sides { get; set; }


    public List<string>? Categories { get; set; }


    public string? Infiltration { get; set; }


    public float? DelayToCanSpawnSec { get; set; }


    public ColliderParams? ColliderParams { get; set; }


    public string? BotZoneName { get; set; }


    public int? CorePointId { get; set; }
}