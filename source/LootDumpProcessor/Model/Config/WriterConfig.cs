using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Config;

public class WriterConfig
{
    [JsonPropertyName("outputLocation")] public string? OutputLocation { get; set; }
}