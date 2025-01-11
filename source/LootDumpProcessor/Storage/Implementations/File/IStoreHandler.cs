namespace LootDumpProcessor.Storage.Implementations.File;

public interface IStoreHandler
{
    void Store<TEntity>(TEntity entity, bool failIfDuplicate = true) where TEntity : IKeyable;
    TEntity? Retrieve<TEntity>(IKey key) where TEntity : IKeyable;
    bool Exists(IKey key);
}