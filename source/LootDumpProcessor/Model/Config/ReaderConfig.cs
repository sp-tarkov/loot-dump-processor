using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record ReaderConfig(
    [Required] IntakeReaderConfig IntakeReaderConfig,
    [Required] IReadOnlyList<string> DumpFilesLocation,
    string? ThresholdDate,
    bool ProcessSubFolders = true
);