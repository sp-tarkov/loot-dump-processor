using LootDumpProcessor.Process.Collector;

namespace LootDumpProcessor.Model.Config;

public class CollectorConfig
{
    public CollectorType CollectorType { get; set; }
    public int MaxEntitiesBeforeDumping { get; set; }
    public string DumpLocation { get; set; }
}