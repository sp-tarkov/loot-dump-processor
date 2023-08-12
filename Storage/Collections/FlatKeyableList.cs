namespace LootDumpProcessor.Storage.Collections;

public class FlatKeyableList<T> : List<T>, IKeyable
{
    public string __ID { get; } = Guid.NewGuid().ToString();

    public IKey GetKey()
    {
        return new FlatUniqueKey(new[] { __ID });
    }
}