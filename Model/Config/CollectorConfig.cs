using System.Text.Json.Serialization;
using LootDumpProcessor.Process.Collector;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class CollectorConfig
{
    [JsonProperty("collectorType")]
    [JsonPropertyName("collectorType")]
    public CollectorType CollectorType { get; set; }
    
    [JsonProperty("maxEntitiesBeforeDumping")]
    [JsonPropertyName("maxEntitiesBeforeDumping")]
    public int MaxEntitiesBeforeDumping { get; set; }
    
    [JsonProperty("dumpLocation")]
    [JsonPropertyName("dumpLocation")]
    public string DumpLocation { get; set; }
}