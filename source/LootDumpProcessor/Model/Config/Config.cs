using System.Text.Json.Serialization;
using LootDumpProcessor.Serializers.Json;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class Config
{
    [JsonProperty("serverLocation")]
    [JsonPropertyName("serverLocation")]
    public string ServerLocation { get; set; } = string.Empty;

    [JsonProperty("threads")]
    [JsonPropertyName("threads")]
    public int Threads { get; set; } = 6;

    [JsonProperty("threadPoolingTimeoutMs")]
    [JsonPropertyName("threadPoolingTimeoutMs")]
    public int ThreadPoolingTimeoutMs { get; set; } = 1000;

    [JsonProperty("jsonSerializer")]
    [JsonPropertyName("jsonSerializer")]
    public JsonSerializerTypes JsonSerializer { get; set; } = JsonSerializerTypes.DotNet;

    [JsonProperty("manualGarbageCollectionCalls")]
    [JsonPropertyName("manualGarbageCollectionCalls")]
    public bool ManualGarbageCollectionCalls { get; set; }

    [JsonProperty("dataStorageConfig")]
    [JsonPropertyName("dataStorageConfig")]
    public DataStorageConfig DataStorageConfig { get; set; }

    [JsonProperty("loggerConfig")]
    [JsonPropertyName("loggerConfig")]
    public LoggerConfig LoggerConfig { get; set; }

    [JsonProperty("readerConfig")]
    [JsonPropertyName("readerConfig")]
    public ReaderConfig ReaderConfig { get; set; }

    [JsonProperty("processorConfig")]
    [JsonPropertyName("processorConfig")]
    public ProcessorConfig ProcessorConfig { get; set; }
    
    [JsonProperty("dumpProcessorConfig")]
    [JsonPropertyName("dumpProcessorConfig")]
    public DumpProcessorConfig DumpProcessorConfig { get; set; }

    [JsonProperty("writerConfig")]
    [JsonPropertyName("writerConfig")]
    public WriterConfig WriterConfig { get; set; }
    
    [JsonProperty("collectorConfig")]
    [JsonPropertyName("collectorConfig")]
    public CollectorConfig CollectorConfig { get; set; }

    [JsonProperty("containerIgnoreList")]
    [JsonPropertyName("containerIgnoreList")]
    public Dictionary<string, string[]> ContainerIgnoreList { get; set; }
    
    [JsonProperty("mapsToProcess")]
    [JsonPropertyName("mapsToProcess")]
    public List<string> MapsToProcess { get; set; }
}