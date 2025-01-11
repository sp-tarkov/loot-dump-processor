namespace LootDumpProcessor.Storage;

public abstract class AbstractKey(string[] indexes) : IKey
{
    public abstract KeyType GetKeyType();

    public string SerializedKey
    {
        get => string.Join("|", indexes);
        set => indexes = value.Split("|");
    }

    public string[] GetLookupIndex() => indexes;
}