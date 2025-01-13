using LootDumpProcessor.Model;

namespace LootDumpProcessor.Process.Services.ComposedKeyGenerator;

public interface IComposedKeyGenerator
{
    ComposedKey Generate(IReadOnlyList<Item>? items);
}