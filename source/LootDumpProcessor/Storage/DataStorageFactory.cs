using System.Collections.Concurrent;
using LootDumpProcessor.Storage.Implementations.File;
using LootDumpProcessor.Storage.Implementations.Memory;

namespace LootDumpProcessor.Storage;

public static class DataStorageFactory
{
    private static readonly ConcurrentDictionary<DataStorageTypes, IDataStorage> DataStorage = new();

    /**
     * Requires LootDumpProcessorContext to be initialized before using
     */
    public static IDataStorage GetInstance() =>
        GetInstance(LootDumpProcessorContext.GetConfig().DataStorageConfig.DataStorageType);

    private static IDataStorage GetInstance(DataStorageTypes type)
    {
        if (DataStorage.TryGetValue(type, out var dataStorage)) return dataStorage;
        
        dataStorage = type switch
        {
            DataStorageTypes.File => new FileDataStorage(),
            DataStorageTypes.Memory => new MemoryDataStorage(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        DataStorage.TryAdd(type, dataStorage);

        return dataStorage;
    }
}