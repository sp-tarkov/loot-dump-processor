namespace LootDumpProcessor.Storage;

public abstract class AbstractKey(string[] indexes) : IKey
{
    private string[] _indexes = indexes;
    public abstract KeyType GetKeyType();

    public string SerializedKey
    {
        get => string.Join("|", _indexes);
        set => _indexes = value.Split("|");
    }

    public string[] GetLookupIndex() => _indexes;
}