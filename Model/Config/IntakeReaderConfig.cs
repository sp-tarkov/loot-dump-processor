using System.Text.Json.Serialization;
using LootDumpProcessor.Process.Reader;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class IntakeReaderConfig
{
    [JsonProperty("readerType")]
    [JsonPropertyName("readerType")]
    public IntakeReaderTypes IntakeReaderType { get; set; } = IntakeReaderTypes.Json;
    
    [JsonProperty("ignoredDumpLocations")]
    [JsonPropertyName("ignoredDumpLocations")]
    public List<string> IgnoredDumpLocations { get; set; } = new List<string>();
}