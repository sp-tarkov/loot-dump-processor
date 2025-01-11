using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model.Tarkov;

public class Grid
{
    [JsonPropertyName("_props")] public GridProps Props { get; set; }
}