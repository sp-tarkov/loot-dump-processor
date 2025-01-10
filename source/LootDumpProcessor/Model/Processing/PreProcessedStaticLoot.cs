namespace LootDumpProcessor.Model.Processing;

public class PreProcessedStaticLoot
{
    public string Type { get; set; }
    public string ContainerId { get; set; }
    public List<Item> Items { get; set; }
}