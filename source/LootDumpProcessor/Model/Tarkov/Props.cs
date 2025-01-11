using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model.Tarkov;

public class Props
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public double? MaxDurability { get; set; }
    public string? Caliber { get; set; }

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


    [JsonPropertyName("defAmmo")] public string DefaultAmmo { get; set; }
}