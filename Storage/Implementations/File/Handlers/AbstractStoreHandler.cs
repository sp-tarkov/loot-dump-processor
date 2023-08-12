using LootDumpProcessor.Storage.Implementations.Serializers;

namespace LootDumpProcessor.Storage.Implementations.Handlers;

public abstract class AbstractStoreHandler : IStoreHandler
{
    protected readonly IDataStorageFileSerializer _serializer;

    public AbstractStoreHandler()
    {
        _serializer = DataStorageFileSerializerFactory.GetInstance();
    }

    public void Store<T>(T obj, bool failIfDuplicate = true) where T : IKeyable
    {
        var locationWithFile = GetLocation(obj.GetKey());
        if (File.Exists(locationWithFile) && failIfDuplicate)
        {
            throw new Exception($"Attempted to save duplicated object into data storage: {locationWithFile}");
        }

        File.WriteAllText(locationWithFile, _serializer.GetSerializer().Serialize(obj));
    }

    public T? Retrieve<T>(IKey obj) where T : IKeyable
    {
        var locationWithFile = GetLocation(obj);
        if (!File.Exists(locationWithFile))
        {
            return default;
        }

        return _serializer.GetSerializer().Deserialize<T>(File.ReadAllText(locationWithFile));
    }

    public bool Exists(IKey obj)
    {
        var locationWithFile = GetLocation(obj);
        return File.Exists(locationWithFile);
    }

    public abstract List<T> RetrieveAll<T>() where T : IKeyable;

    protected abstract string GetLocation(IKey key);

    protected virtual string GetBaseLocation()
    {
        var location =
            string.IsNullOrEmpty(LootDumpProcessorContext.GetConfig().DataStorageConfig.FileDataStorageTempLocation)
                ? LootDumpProcessorContext.GetConfig().DataStorageConfig.FileDataStorageTempLocation
                : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return $"{location}/SPT/tmp/LootGen";
    }
}