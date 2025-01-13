using System.Text.Json;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Serializers.Json;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor.Storage.Implementations.File.Handlers;

public abstract class AbstractStoreHandler(IOptions<Config> config) : IStoreHandler
{
    private readonly Config _config = (config ?? throw new ArgumentNullException(nameof(config))).Value;

    public void Store<TEntity>(TEntity entity, bool failIfDuplicate = true) where TEntity : IKeyable
    {
        var locationWithFile = GetLocation(entity.GetKey());
        if (System.IO.File.Exists(locationWithFile) && failIfDuplicate)
            throw new Exception($"Attempted to save duplicated object into data storage: {locationWithFile}");

        System.IO.File.WriteAllText(locationWithFile, JsonSerializer.Serialize(entity, JsonSerializerSettings.Default));
    }

    public TEntity? Retrieve<TEntity>(IKey key) where TEntity : IKeyable
    {
        var locationWithFile = GetLocation(key);
        if (!System.IO.File.Exists(locationWithFile)) return default;

        return JsonSerializer.Deserialize<TEntity>(System.IO.File.ReadAllText(locationWithFile),
            JsonSerializerSettings.Default);
    }

    public bool Exists(IKey key)
    {
        var locationWithFile = GetLocation(key);
        return System.IO.File.Exists(locationWithFile);
    }

    protected abstract string GetLocation(IKey key);

    protected virtual string GetBaseLocation()
    {
        var location =
            string.IsNullOrEmpty(_config.DataStorageConfig.FileDataStorageTempLocation)
                ? _config.DataStorageConfig.FileDataStorageTempLocation
                : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        return $"{location}/SPT/tmp/LootGen";
    }
}