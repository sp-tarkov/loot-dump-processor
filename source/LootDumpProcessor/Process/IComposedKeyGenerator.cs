using LootDumpProcessor.Model;

namespace LootDumpProcessor.Process;

public interface IComposedKeyGenerator
{
    ComposedKey Generate(IReadOnlyList<Item>? items);
}