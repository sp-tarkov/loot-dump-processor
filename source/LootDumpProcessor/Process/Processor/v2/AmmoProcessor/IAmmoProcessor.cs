using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.v2.AmmoProcessor;

public interface IAmmoProcessor
{
    IReadOnlyDictionary<string, List<AmmoDistribution>> CreateAmmoDistribution(
        string mapId,
        List<PreProcessedStaticLoot> containers);
}