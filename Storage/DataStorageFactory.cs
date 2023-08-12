using LootDumpProcessor.Storage.Implementations;
using LootDumpProcessor.Storage.Implementations.Memory;

namespace LootDumpProcessor.Storage;

public static class DataStorageFactory
{
    private static readonly Dictionary<DataStorageTypes, IDataStorage> _dataStorage =
        new Dictionary<DataStorageTypes, IDataStorage>();

    private static object lockObject = new object();

    /**
     * Requires LootDumpProcessorContext to be initialized before using
     */
    public static IDataStorage GetInstance()
    {
        return GetInstance(LootDumpProcessorContext.GetConfig().DataStorageConfig.DataStorageType);
    }

    public static IDataStorage GetInstance(DataStorageTypes type)
    {
        IDataStorage dataStorage;
        lock (lockObject)
        {
            if (!_dataStorage.TryGetValue(type, out dataStorage))
            {
                switch (type)
                {
                    case DataStorageTypes.File:
                        dataStorage = new FileDataStorage();
                        break;
                    case DataStorageTypes.Memory:
                        dataStorage = new MemoryDataStorage();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                _dataStorage.Add(type, dataStorage);
            }
        }

        return dataStorage;
    }
}