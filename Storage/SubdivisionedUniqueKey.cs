namespace LootDumpProcessor.Storage;

public class SubdivisionedUniqueKey : AbstractKey
{
    public override KeyType GetKeyType() => KeyType.Subdivisioned;

    public SubdivisionedUniqueKey(string[] indexes) : base(indexes)
    {
    }
}