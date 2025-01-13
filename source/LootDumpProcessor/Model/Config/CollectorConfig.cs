using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using LootDumpProcessor.Process.Collector;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record CollectorConfig(
    [Required] CollectorType CollectorType,
    [Required] int MaxEntitiesBeforeDumping,
    [Required] string DumpLocation
);