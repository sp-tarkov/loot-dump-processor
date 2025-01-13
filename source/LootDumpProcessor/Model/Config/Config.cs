using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record Config
{
    [Required] public string ServerLocation { get; init; } = string.Empty;
    [Required] public bool ManualGarbageCollectionCalls { get; init; }
    [Required] public DataStorageConfig DataStorageConfig { get; init; } = null!;
    [Required] public ReaderConfig ReaderConfig { get; init; } = null!;
    [Required] public ProcessorConfig ProcessorConfig { get; init; } = null!;
    [Required] public DumpProcessorConfig DumpProcessorConfig { get; init; } = null!;
    [Required] public WriterConfig WriterConfig { get; init; } = null!;
    [Required] public CollectorConfig CollectorConfig { get; init; } = null!;
    [Required] public IReadOnlyDictionary<string, string[]> ContainerIgnoreList { get; init; } = null!;
    [Required] public IReadOnlyList<string> MapsToProcess { get; init; } = null!;
}