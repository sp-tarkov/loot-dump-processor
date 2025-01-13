using JetBrains.Annotations;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record IntakeReaderConfig(IReadOnlyList<string> IgnoredDumpLocations, int MaxDumpsPerMap = 1500);