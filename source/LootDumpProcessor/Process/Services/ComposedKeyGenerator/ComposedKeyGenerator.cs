using System.Globalization;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Services.KeyGenerator;
using LootDumpProcessor.Process.Services.TarkovItemsProvider;

namespace LootDumpProcessor.Process.Services.ComposedKeyGenerator;

public class ComposedKeyGenerator(ITarkovItemsProvider tarkovItemsProvider, IKeyGenerator keyGenerator)
    : IComposedKeyGenerator
{
    private readonly ITarkovItemsProvider _tarkovItemsProvider =
        tarkovItemsProvider ?? throw new ArgumentNullException(nameof(tarkovItemsProvider));

    private readonly IKeyGenerator
        _keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));

    public ComposedKey Generate(IReadOnlyList<Item>? items)
    {
        var key = items?.Select(i => i.Tpl)
            .Where(i => !string.IsNullOrEmpty(i) &&
                        !_tarkovItemsProvider.IsBaseClass(i, BaseClasses.Ammo))
            .Cast<string>()
            .Select(i => (double)i.GetHashCode())
            .Sum()
            .ToString(CultureInfo.InvariantCulture) ?? _keyGenerator.Generate();
        var firstItem = items?[0];

        return new ComposedKey(key, firstItem);
    }
}