using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output.LooseLoot;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Storage;

namespace LootDumpProcessor.Process.Processor.v2.LooseLootProcessor
{
    public interface ILooseLootProcessor
    {
        PreProcessedLooseLoot PreProcessLooseLoot(List<Template> looseLoot);

        Dictionary<string, LooseLootRoot> CreateLooseLootDistribution(
            Dictionary<string, int> mapCounts,
            Dictionary<string, IKey> looseLootCounts
        );
    }
}