namespace LootDumpProcessor.Storage.Collections;

public class FlatKeyableList<T> : List<T>, IKeyable
{
    public string Id { get; }

    public FlatKeyableList(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        Id = id;
    }

    public IKey GetKey() => new FlatUniqueKey([Id]);
}