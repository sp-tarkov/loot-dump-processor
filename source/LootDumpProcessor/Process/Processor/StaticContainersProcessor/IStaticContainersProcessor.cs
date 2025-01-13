using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output.StaticContainer;

namespace LootDumpProcessor.Process.Processor.StaticContainersProcessor;

public interface IStaticContainersProcessor
{
    Task<MapStaticLoot> CreateStaticWeaponsAndForcedContainers(RootData rawMapDump);
    Task<IReadOnlyList<Template>> CreateDynamicStaticContainers(RootData rawMapDump);
}