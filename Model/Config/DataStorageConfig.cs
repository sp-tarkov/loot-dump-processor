using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class DataStorageConfig
{
    [JsonProperty("dataStorageType")]
    [JsonPropertyName("dataStorageType")]
    public DataStorageTypes DataStorageType { get; set; } = DataStorageTypes.File;
    
    [JsonProperty("fileDataStorageTempLocation")]
    [JsonPropertyName("fileDataStorageTempLocation")]
    public string? FileDataStorageTempLocation { get; set; }
}