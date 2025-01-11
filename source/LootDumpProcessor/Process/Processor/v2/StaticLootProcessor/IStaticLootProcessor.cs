using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;

public interface IStaticLootProcessor
{
    IReadOnlyList<PreProcessedStaticLoot> PreProcessStaticLoot(IReadOnlyList<Template> staticLoot);

    IReadOnlyDictionary<string, IReadOnlyDictionary<string, StaticItemDistribution>> CreateStaticLootDistribution(
        IReadOnlyDictionary<string, List<PreProcessedStaticLoot>> containerCounts,
        IReadOnlyDictionary<string, MapStaticLoot> staticContainers);
}