using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model.Tarkov;

public class GridProps
{
    [JsonPropertyName("cellsH")] public int CellsHorizontal { get; set; }


    [JsonPropertyName("cellsV")] public int CellsVertical { get; set; }


    [JsonPropertyName("minCount")] public int MinCount { get; set; }


    [JsonPropertyName("maxCount")] public int MaxCount { get; set; }


    [JsonPropertyName("filters")] public List<GridFilter> Filters { get; set; }
}