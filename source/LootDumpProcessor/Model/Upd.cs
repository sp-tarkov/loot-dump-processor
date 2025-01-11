using LootDumpProcessor.Utils;


namespace LootDumpProcessor.Model;

public class Upd : ICloneable
{
    public object? StackObjectsCount { get; set; }


    public FireMode? FireMode { get; set; }


    public Foldable? Foldable { get; set; }


    public Repairable? Repairable { get; set; }

    public object Clone() => new Upd
    {
        StackObjectsCount = StackObjectsCount,
        FireMode = ProcessorUtil.Copy(FireMode),
        Foldable = ProcessorUtil.Copy(Foldable),
        Repairable = ProcessorUtil.Copy(Repairable)
    };
}