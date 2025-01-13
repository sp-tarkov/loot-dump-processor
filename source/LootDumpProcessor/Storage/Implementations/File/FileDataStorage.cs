namespace LootDumpProcessor.Storage.Implementations.File;

public class FileDataStorage(StoreHandlerFactory storeHandlerFactory) : IDataStorage
{
    private readonly StoreHandlerFactory _storeHandlerFactory =
        storeHandlerFactory ?? throw new ArgumentNullException(nameof(storeHandlerFactory));

    public void Store<TEntity>(TEntity entity) where TEntity : IKeyable
    {
        _storeHandlerFactory.GetInstance(entity.GetKey().GetKeyType()).Store(entity);
    }

    public bool Exists(IKey key) => _storeHandlerFactory.GetInstance(key.GetKeyType()).Exists(key);

    public T GetItem<T>(IKey key) where T : IKeyable =>
        _storeHandlerFactory.GetInstance(key.GetKeyType()).Retrieve<T>(key);

    public void Clear()
    {
        // leaving empty so this the File version can still be used it needed
    }
}