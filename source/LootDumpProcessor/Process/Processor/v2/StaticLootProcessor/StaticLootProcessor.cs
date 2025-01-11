using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;

public class StaticLootProcessor(ILogger<StaticLootProcessor> logger) : IStaticLootProcessor
{
    private readonly ILogger<StaticLootProcessor> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    public IReadOnlyList<PreProcessedStaticLoot> PreProcessStaticLoot(IReadOnlyList<Template> staticLoot)
    {
        var nonWeaponContainers = new List<PreProcessedStaticLoot>();

        foreach (var lootSpawn in staticLoot)
        {
            if (lootSpawn.Items == null || lootSpawn.Items.Count == 0)
            {
                _logger.LogWarning("Loot spawn position with ID {LootId} has no items.", lootSpawn.Id);
                continue;
            }

            var firstItemTpl = lootSpawn.Items[0].Tpl;

            if (!LootDumpProcessorContext.GetStaticWeaponIds().Contains(firstItemTpl))
            {
                nonWeaponContainers.Add(new PreProcessedStaticLoot
                {
                    Type = firstItemTpl,
                    ContainerId = lootSpawn.Items[0].Id,
                    Items = lootSpawn.Items.Skip(1).ToList()
                });

                _logger.LogDebug("Added non-weapon container with ID {ContainerId} and Type {Type}.",
                    lootSpawn.Items[0].Id, firstItemTpl);
            }
        }

        _logger.LogInformation("Preprocessed {Count} static loot containers.", nonWeaponContainers.Count);
        return nonWeaponContainers;
    }

    public IReadOnlyDictionary<string, StaticItemDistribution> CreateStaticLootDistribution(
        string mapName,
        IReadOnlyList<PreProcessedStaticLoot> containers)
    {
        var staticLootDistribution = new Dictionary<string, StaticItemDistribution>();
        var uniqueContainerTypes = containers.Select(container => container.Type).Distinct();

        foreach (var containerType in uniqueContainerTypes)
        {
            var selectedContainers = containers.Where(container => container.Type == containerType).ToArray();
            var itemCounts = GetItemCountsInContainers(selectedContainers);
            var itemDistribution = GenerateItemDistribution(selectedContainers);

            staticLootDistribution[containerType] = new StaticItemDistribution
            {
                ItemCountDistribution = itemCounts
                    .GroupBy(count => count)
                    .Select(group => new ItemCountDistribution
                    {
                        Count = group.Key,
                        RelativeProbability = group.Count()
                    })
                    .ToList(),
                ItemDistribution = itemDistribution
            };

            _logger.LogDebug(
                "Processed static loot distribution for ContainerType `{ContainerType}` in Map `{MapName}`.",
                containerType, mapName);
        }

        _logger.LogInformation("Created static loot distribution for Map `{MapName}`.", mapName);
        return staticLootDistribution;
    }

    private static IReadOnlyList<int> GetItemCountsInContainers(
        IReadOnlyList<PreProcessedStaticLoot> selectedContainers)
    {
        return selectedContainers
            .Select(container => container.Items.Count(item => item.ParentId == container.ContainerId))
            .ToList();
    }

    private static IReadOnlyList<StaticDistribution> GenerateItemDistribution(
        IReadOnlyList<PreProcessedStaticLoot> selectedContainers)
    {
        var itemHitCounts = new Dictionary<string, int>();

        foreach (var container in selectedContainers)
        foreach (var item in container.Items.Where(item => item.ParentId == container.ContainerId))
            if (!itemHitCounts.TryAdd(item.Tpl, 1))
                itemHitCounts[item.Tpl]++;

        return itemHitCounts.Select(kv => new StaticDistribution
        {
            Tpl = kv.Key,
            RelativeProbability = kv.Value
        }).ToList();
    }
}