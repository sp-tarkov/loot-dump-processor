using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using LootDumpProcessor.Storage;


namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record DataStorageConfig(
    [Required] string FileDataStorageTempLocation,
    [Required] DataStorageTypes DataStorageType = DataStorageTypes.File
);