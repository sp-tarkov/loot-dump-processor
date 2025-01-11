namespace LootDumpProcessor.Model.Config;

public class Config
{
    public string ServerLocation { get; set; } = string.Empty;
    public bool ManualGarbageCollectionCalls { get; set; }
    public DataStorageConfig DataStorageConfig { get; set; }
    public ReaderConfig ReaderConfig { get; set; }
    public ProcessorConfig ProcessorConfig { get; set; }
    public DumpProcessorConfig DumpProcessorConfig { get; set; }
    public WriterConfig WriterConfig { get; set; }
    public CollectorConfig CollectorConfig { get; set; }
    public Dictionary<string, string[]> ContainerIgnoreList { get; set; }
    public List<string> MapsToProcess { get; set; }
}