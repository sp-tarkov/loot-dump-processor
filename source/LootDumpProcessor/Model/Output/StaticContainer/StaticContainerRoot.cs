using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Output.StaticContainer;

public class StaticContainerRoot
{
    public MapStaticLoot? Laboratory { get; set; }


    public MapStaticLoot? Shoreline { get; set; }


    [JsonPropertyName("Streets of Tarkov")] public MapStaticLoot? StreetsofTarkov { get; set; }


    public MapStaticLoot? Interchange { get; set; }


    public MapStaticLoot? Customs { get; set; }


    public MapStaticLoot? Woods { get; set; }


    public MapStaticLoot? Factory { get; set; }


    public MapStaticLoot? ReserveBase { get; set; }


    public MapStaticLoot? Lighthouse { get; set; }


    [JsonPropertyName("Sandbox")] public MapStaticLoot? GroundZero { get; set; }
}