namespace LootDumpProcessor.Model.Config;

public class IntakeReaderConfig
{
    public int MaxDumpsPerMap { get; set; } = 1500;
    public List<string> IgnoredDumpLocations { get; set; } = new();
}