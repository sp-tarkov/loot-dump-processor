namespace LootDumpProcessor.Storage;

public class FlatUniqueKey : AbstractKey
{
    public override KeyType GetKeyType() => KeyType.Unique;

    public FlatUniqueKey(string[] indexes) : base(indexes)
    {
    }
}