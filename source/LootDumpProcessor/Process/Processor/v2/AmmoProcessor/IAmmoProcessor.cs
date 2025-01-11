using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.v2.AmmoProcessor;

public interface IAmmoProcessor
{
    IReadOnlyDictionary<string, IReadOnlyDictionary<string, List<AmmoDistribution>>> CreateAmmoDistribution(
        IReadOnlyDictionary<string, List<PreProcessedStaticLoot>> containerCounts);
}