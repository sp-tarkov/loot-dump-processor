namespace LootDumpProcessor.Model;

public class Repairable : ICloneable
{
    public int? Durability { get; set; }


    public int? MaxDurability { get; set; }

    public object Clone() => new Repairable
    {
        Durability = Durability,
        MaxDurability = MaxDurability
    };
}