namespace LootDumpProcessor.Storage.Implementations.File;

public class FileDataStorage : IDataStorage
{
    public void Store<TEntity>(TEntity entity) where TEntity : IKeyable
    {
        StoreHandlerFactory.GetInstance(entity.GetKey().GetKeyType()).Store(entity);
    }

    public bool Exists(IKey key) => StoreHandlerFactory.GetInstance(key.GetKeyType()).Exists(key);

    public T GetItem<T>(IKey key) where T : IKeyable =>
        StoreHandlerFactory.GetInstance(key.GetKeyType()).Retrieve<T>(key);

    public void Clear()
    {
        // leaving empty so this the File version can still be used it needed
    }
}