using System.Collections.Frozen;
using System.Collections.Immutable;
using LootDumpProcessor.Model.Config;

namespace LootDumpProcessor.Process;

public interface IForcedItemsProvider
{
    Task<ForcedStatic> GetForcedStatic();
    Task<FrozenDictionary<string, ImmutableHashSet<string>>> GetForcedLooseItems();
}