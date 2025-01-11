using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class IntakeReaderConfig
{
    [JsonProperty("maxDumpsPerMap")]
    [JsonPropertyName("maxDumpsPerMap")]
    public int MaxDumpsPerMap { get; set; } = 1500;
    
    
    [JsonProperty("ignoredDumpLocations")]
    [JsonPropertyName("ignoredDumpLocations")]
    public List<string> IgnoredDumpLocations { get; set; } = new();
}