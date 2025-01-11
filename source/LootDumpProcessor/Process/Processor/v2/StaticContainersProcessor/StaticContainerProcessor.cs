using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Utils;
using Microsoft.Extensions.Logging;
using LootDumpProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;

public class StaticContainersProcessor : IStaticContainersProcessor
{
    private readonly ILogger<StaticContainersProcessor> _logger;

    public StaticContainersProcessor(ILogger<StaticContainersProcessor> logger)
        => _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public MapStaticLoot CreateStaticWeaponsAndForcedContainers(RootData rawMapDump)
    {
        var locationLoot = rawMapDump.Data.LocationLoot;
        var mapId = locationLoot.Id.ToLower();
        var staticLootPositions = locationLoot.Loot
            .Where(loot => loot.IsContainer.GetValueOrDefault())
            .OrderBy(loot => loot.Id)
            .ToList();

        var staticWeapons = new List<Template>();

        foreach (var lootPosition in staticLootPositions)
        {
            if (lootPosition.Items == null || lootPosition.Items.Count == 0)
            {
                _logger.LogWarning("Loot position with ID {LootId} has no items.", lootPosition.Id);
                continue;
            }

            var firstItemTpl = lootPosition.Items.First().Tpl;

            if (!LootDumpProcessorContext.GetStaticWeaponIds().Contains(firstItemTpl))
                continue;

            var copiedLoot = ProcessorUtil.Copy(lootPosition);
            staticWeapons.Add(copiedLoot);
            _logger.LogDebug("Added static weapon with ID {WeaponId} to Map {MapId}.", copiedLoot.Id, mapId);
        }

        var forcedStaticItems = LootDumpProcessorContext.GetForcedItems().TryGetValue(mapId, out var forcedItems)
            ? forcedItems
            : new List<StaticForced>();

        var mapStaticLoot = new MapStaticLoot
        {
            StaticWeapons = staticWeapons,
            StaticForced = forcedStaticItems
        };

        _logger.LogInformation("Created static weapons and forced containers for Map {MapId}.", mapId);
        return mapStaticLoot;
    }

    public IReadOnlyList<Template> CreateDynamicStaticContainers(RootData rawMapDump)
    {
        var dynamicContainers = rawMapDump.Data.LocationLoot.Loot
            .Where(loot => loot.IsContainer.GetValueOrDefault() &&
                           !LootDumpProcessorContext.GetStaticWeaponIds().Contains(loot.Items.FirstOrDefault()?.Tpl))
            .ToList();

        foreach (var container in dynamicContainers)
        {
            if (container.Items == null || !container.Items.Any())
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