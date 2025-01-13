using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;

public interface IStaticLootProcessor
{
    Task<IReadOnlyList<PreProcessedStaticLoot>> PreProcessStaticLoot(IReadOnlyList<Template> staticLoot);

    IReadOnlyDictionary<string, StaticItemDistribution> CreateStaticLootDistribution(
        string mapName,
        IReadOnlyList<PreProcessedStaticLoot> containers);
}