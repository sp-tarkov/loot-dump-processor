using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output.StaticContainer;

namespace LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;

public interface IStaticContainersProcessor
{
    (string, MapStaticLoot) CreateStaticWeaponsAndForcedContainers(RootData rawMapDump);
    IReadOnlyList<Template> CreateDynamicStaticContainers(RootData rawMapDump);
}