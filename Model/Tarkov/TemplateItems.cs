using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Tarkov;

public class TemplateFileItem
{
    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonProperty("_name")]
    [JsonPropertyName("_name")]
    public string Name { get; set; }

    [JsonProperty("_parent")]
    [JsonPropertyName("_parent")]
    public string? Parent { get; set; }

    [JsonProperty("_type")]
    [JsonPropertyName("_type")]
    public string Type { get; set; }

    [JsonProperty("_props")]
    [JsonPropertyName("_props")]
    public Props Props { get; set; }
}

public class Props
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public double? MaxDurability { get; set; }
    public string? Caliber { get; set; }

    [JsonProperty("ammoCaliber")]
    [JsonPropertyName("ammoCaliber")]
    public string? AmmoCaliber { get; set; }

    public bool QuestItem { get; set; }
    public int SpawnChance { get; set; }
    public List<string> SpawnFilter { get; set; }
    public List<Grid> Grids { get; set; }
    public string Rarity { get; set; }

    public int ExtraSizeLeft { get; set; }
    public int ExtraSizeRight { get; set; }
    public int ExtraSizeUp { get; set; }
    public int ExtraSizeDown { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int? StackMinRandom { get; set; }
    public int? StackMaxRandom { get; set; }

    public List<Chamber> Chambers { get; set; }
    public List<Slot> Slots { get; set; }

    [JsonProperty("defAmmo")]
    [JsonPropertyName("defAmmo")]
    public string DefaultAmmo { get; set; }
}

public class Grid
{
    [JsonProperty("_props")]
    [JsonPropertyName("_props")]
    public GridProps Props { get; set; }
}

public class GridProps
{
    [JsonProperty("cellsH")]
    [JsonPropertyName("cellsH")]
    public int CellsHorizontal { get; set; }

    [JsonProperty("cellsV")]
    [JsonPropertyName("cellsV")]
    public int CellsVertical { get; set; }

    [JsonProperty("minCount")]
    [JsonPropertyName("minCount")]
    public int MinCount { get; set; }

    [JsonProperty("maxCount")]
    [JsonPropertyName("maxCount")]
    public int MaxCount { get; set; }

    [JsonProperty("filters")]
    [JsonPropertyName("filters")]
    public List<GridFilter> Filters { get; set; }
}

public class GridFilter
{
}

public class Chamber
{
    [JsonProperty("_name")]
    [JsonPropertyName("_name")]
    public string Name { get; set; }

    [JsonProperty("_id")]
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonProperty("_parent")]
    [JsonPropertyName("_parent")]
    public string Parent { get; set; }

    [JsonProperty("_props")]
    [JsonPropertyName("_props")]
    public ChamberProps Props { get; set; }

    [JsonProperty("_required")]
    [JsonPropertyName("_required")]
    public bool Required { get; set; }

    [JsonProperty("_mergeSlotWithChildren")]
    [JsonPropertyName("_mergeSlotWithChildren")]
    public bool MergeSlotWithChildren { get; set; }

    [JsonProperty("_proto")]
    [JsonPropertyName("_proto")]
    public string Proto { get; set; }
}

public class Slot
{
    [JsonProperty("_name")]
    [JsonPropertyName("_name")]
    public string Name { get; set; }

    [JsonProperty("_required")]
    [JsonPropertyName("_required")]
    public bool Required { get; set; }
}

public class ChamberProps
{
    [JsonProperty("filters")]
    [JsonPropertyName("filters")]
    public List<FilterClass> Filters { get; set; }
}

public class FilterClass
{
    [JsonProperty("filter")]
    [JsonPropertyName("filter")]
    public List<string> Filter { get; set; }
}