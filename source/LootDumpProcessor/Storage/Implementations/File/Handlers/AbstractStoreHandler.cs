using System.Text.Json;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Storage.Implementations.File.Handlers;

public abstract class AbstractStoreHandler : IStoreHandler
{
    public void Store<T>(T obj, bool failIfDuplicate = true) where T : IKeyable
    {
        var locationWithFile = GetLocation(obj.GetKey());
        if (System.IO.File.Exists(locationWithFile) && failIfDuplicate)
            throw new Exception($"Attempted to save duplicated object into data storage: {locationWithFile}");

        System.IO.File.WriteAllText(locationWithFile, JsonSerializer.Serialize(obj, JsonSerializerSettings.Default));
    }

    public T? Retrieve<T>(IKey obj) where T : IKeyable
    {
        var locationWithFile = GetLocation(obj);
        if (!System.IO.File.Exists(locationWithFile)) return default;

        return JsonSerializer.Deserialize<T>(System.IO.File.ReadAllText(locationWithFile),
            JsonSerializerSettings.Default);
    }

    public bool Exists(IKey obj)
    {
        var locationWithFile = GetLocation(obj);
        return System.IO.File.Exists(locationWithFile);
    }

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