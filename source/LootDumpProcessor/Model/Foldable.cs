namespace LootDumpProcessor.Model;

public class Foldable : ICloneable
{
    public bool? Folded { get; set; }

    public object Clone() => new Foldable
    {
        Folded = Folded
    };
}