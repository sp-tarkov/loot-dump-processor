namespace LootDumpProcessor.Model.Output.LooseLoot;

public class SpawnPoint
{
    public string? LocationId { get; set; }


    public double? Probability { get; set; }


    public Template? Template { get; set; }


    public List<ItemDistribution>? ItemDistribution { get; set; }
}