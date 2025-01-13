using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Utils;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;

public class StaticContainersProcessor(
    ILogger<StaticContainersProcessor> logger, IForcedItemsProvider forcedItemsProvider
)
    : IStaticContainersProcessor
{
    private readonly ILogger<StaticContainersProcessor> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IForcedItemsProvider _forcedItemsProvider =
        forcedItemsProvider ?? throw new ArgumentNullException(nameof(forcedItemsProvider));

    public async Task<MapStaticLoot> CreateStaticWeaponsAndForcedContainers(RootData rawMapDump)
    {
        var locationLoot = rawMapDump.Data.LocationLoot;
        var mapId = locationLoot.Id.ToLower();
        var staticLootPositions = locationLoot.Loot
            .Where(loot => loot.IsContainer)
            .OrderBy(loot => loot.Id)
            .ToList();

        var staticWeapons = new List<Template>();
        var forcedStatic = await _forcedItemsProvider.GetForcedStatic();

        foreach (var lootPosition in staticLootPositions)
        {
            if (lootPosition.Items == null || lootPosition.Items.Count == 0)
            {
                _logger.LogWarning("Loot position with ID {LootId} has no items.", lootPosition.Id);
                continue;
            }

            var firstItemTpl = lootPosition.Items.First().Tpl;

            if (!forcedStatic.StaticWeaponIds.Contains(firstItemTpl))
                continue;

            var copiedLoot = ProcessorUtil.Copy(lootPosition);
            staticWeapons.Add(copiedLoot);
            _logger.LogDebug("Added static weapon with ID {WeaponId} to Map {MapId}.", copiedLoot.Id, mapId);
        }

        var forcedStaticItems = forcedStatic.ForcedItems
            .TryGetValue(mapId, out var forcedItems)
            ? forcedItems
            : new List<StaticForced>();

        var mapStaticLoot = new MapStaticLoot
        {
            StaticWeapons = staticWeapons,
            StaticForced = forcedStaticItems.ToList()
        };

        _logger.LogInformation("Created static weapons and forced containers for Map {MapId}.", mapId);
        return mapStaticLoot;
    }

    public async Task<IReadOnlyList<Template>> CreateDynamicStaticContainers(RootData rawMapDump)
    {
        var forcedStatic = await _forcedItemsProvider.GetForcedStatic();

        var dynamicContainers = rawMapDump.Data.LocationLoot.Loot
            .Where(loot => loot.IsContainer &&
                           !forcedStatic.StaticWeaponIds.Contains(loot.Items.FirstOrDefault()?.Tpl))
            .ToList();

        foreach (var container in dynamicContainers)
        {
            if (container.Items == null || container.Items.Count == 0)
            {
                _logger.LogWarning("Dynamic container with ID {ContainerId} has no items.", container.Id);
                continue;
            }

            var firstItem = container.Items.First();
            container.Items = new List<Item> { firstItem };
            _logger.LogDebug("Retained only the first item in dynamic container with ID {ContainerId}.", container.Id);
        }

        _logger.LogInformation("Created {Count} dynamic static containers.", dynamicContainers.Count);
        return dynamicContainers;
    }
}