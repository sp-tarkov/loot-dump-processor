using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Utils;

namespace LootDumpProcessor.Process;

public class ComposedKeyGenerator(ITarkovItemsProvider tarkovItemsProvider) : IComposedKeyGenerator
{
    private readonly ITarkovItemsProvider _tarkovItemsProvider =
        tarkovItemsProvider ?? throw new ArgumentNullException(nameof(tarkovItemsProvider));

    public ComposedKey Generate(IEnumerable<Item> items)
    {
        var key = items?.Select(i => i.Tpl)
            .Where(i => !string.IsNullOrEmpty(i) &&
                        !_tarkovItemsProvider.IsBaseClass(i, BaseClasses.Ammo))
            .Cast<string>()
            .Select(i => (double)i.GetHashCode())
            .Sum()
            .ToString() ?? KeyGenerator.GetNextKey();
        var firstItem = items?.FirstOrDefault();
        return new ComposedKey(key, firstItem);
    }
}