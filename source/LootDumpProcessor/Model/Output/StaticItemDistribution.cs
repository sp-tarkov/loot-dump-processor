using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Output;

public class StaticItemDistribution
{
    [JsonPropertyName("itemcountDistribution")]
    public IReadOnlyList<ItemCountDistribution>? ItemCountDistribution { get; set; }


    public IReadOnlyList<StaticDistribution>? ItemDistribution { get; set; }
}