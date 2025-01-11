using System.Collections.Concurrent;

namespace LootDumpProcessor.Storage.Implementations.Memory;

public class MemoryDataStorage : IDataStorage
{
    private readonly ConcurrentDictionary<string, object> _storage = new();

    public void Store<TEntity>(TEntity entity) where TEntity : IKeyable =>
        _storage.TryAdd(GetLookupKey(entity.GetKey()), entity);

    public TEntity? GetItem<TEntity>(IKey key) where TEntity : IKeyable
    {
        if (_storage.TryGetValue(GetLookupKey(key), out var value)) return (TEntity)value;
        return default;
    }

    public bool Exists(IKey key) => _storage.ContainsKey(GetLookupKey(key));

    private string GetLookupKey(IKey key) => string.Join("-", key.GetLookupIndex());
    public void Clear() => _storage.Clear();
}