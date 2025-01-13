using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output.LooseLoot;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Storage;

namespace LootDumpProcessor.Process.Processor.LooseLootProcessor;

public interface ILooseLootProcessor
{
    PreProcessedLooseLoot PreProcessLooseLoot(List<Template> looseLoot);

    Task<LooseLootRoot> CreateLooseLootDistribution(string mapId,
        int mapCount,
        IKey looseLootCountKey);
}