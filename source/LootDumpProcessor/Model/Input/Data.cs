namespace LootDumpProcessor.Model.Input;

public class Data
{
    public string? ServerID { get; set; }
    public ServerSettings? ServerSettings { get; set; }
    public object? Profile { get; set; }
    public required LocationLoot LocationLoot { get; set; }
}