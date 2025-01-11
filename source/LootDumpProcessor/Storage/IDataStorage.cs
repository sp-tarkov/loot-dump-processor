namespace LootDumpProcessor.Storage;

public interface IDataStorage
{
    void Store<TEntity>(TEntity entity) where TEntity : IKeyable;
    TEntity? GetItem<TEntity>(IKey key) where TEntity : IKeyable;
    bool Exists(IKey key);
    void Clear();
}