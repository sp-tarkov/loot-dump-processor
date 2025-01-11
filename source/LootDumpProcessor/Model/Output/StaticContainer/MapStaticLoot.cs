namespace LootDumpProcessor.Model.Output.StaticContainer;

public class MapStaticLoot
{
    public List<Template>? StaticWeapons { get; set; }


    public List<StaticDataPoint>? StaticContainers { get; set; }


    public List<StaticForced>? StaticForced { get; set; }
}