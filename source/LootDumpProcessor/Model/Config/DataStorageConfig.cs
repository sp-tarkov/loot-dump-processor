using LootDumpProcessor.Storage;


namespace LootDumpProcessor.Model.Config;

public class DataStorageConfig
{
    public DataStorageTypes DataStorageType { get; set; } = DataStorageTypes.File;
    public string? FileDataStorageTempLocation { get; set; }
}