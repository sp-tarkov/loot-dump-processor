using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Tarkov;

public class TemplateFileItem
{
    [JsonPropertyName("_id")] public string Id { get; set; }


    [JsonPropertyName("_name")] public string Name { get; set; }


    [JsonPropertyName("_parent")] public string? Parent { get; set; }


    [JsonPropertyName("_type")] public string Type { get; set; }


    [JsonPropertyName("_props")] public Props Props { get; set; }
}