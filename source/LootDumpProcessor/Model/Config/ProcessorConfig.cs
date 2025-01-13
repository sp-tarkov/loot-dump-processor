using JetBrains.Annotations;

namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record ProcessorConfig(double SpawnPointToleranceForForced = 99, double LooseLootCountTolerancePercentage = 75);