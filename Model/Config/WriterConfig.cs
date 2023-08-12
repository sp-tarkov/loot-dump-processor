using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class WriterConfig
{
    [JsonProperty("outputLocation")]
    [JsonPropertyName("outputLocation")]
    public string? OutputLocation { get; set; }
}