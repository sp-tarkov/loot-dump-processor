using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Output.LooseLoot;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Storage.Collections;
using LootDumpProcessor.Utils;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;

public class LooseLootProcessor(
    ILogger<LooseLootProcessor> logger, IDataStorage dataStorage, ITarkovItemsProvider tarkovItemsProvider,
    IComposedKeyGenerator composedKeyGenerator, IKeyGenerator keyGenerator
)
    : ILooseLootProcessor
{
    private readonly ILogger<LooseLootProcessor>
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IDataStorage
        _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

    private readonly ITarkovItemsProvider _tarkovItemsProvider =
        tarkovItemsProvider ?? throw new ArgumentNullException(nameof(tarkovItemsProvider));

    private readonly IComposedKeyGenerator _composedKeyGenerator =
        composedKeyGenerator ?? throw new ArgumentNullException(nameof(composedKeyGenerator));

    private readonly IKeyGenerator
        _keyGenerator = keyGenerator ?? throw new ArgumentNullException(nameof(keyGenerator));

    public PreProcessedLooseLoot PreProcessLooseLoot(List<Template> looseLoot)
    {
        var preProcessedLoot = new PreProcessedLooseLoot
        {
            Counts = new Dictionary<string, int>()
        };

        var itemPropertiesDictionary =
            new SubdivisionedKeyableDictionary<string, List<Template>>(_keyGenerator.Generate());
        preProcessedLoot.ItemProperties = (AbstractKey)itemPropertiesDictionary.GetKey();
        preProcessedLoot.MapSpawnpointCount = looseLoot.Count;

        var uniqueLootIds = new Dictionary<string, object>();

        // Rounding rotation to prevent duplicate spawnpoints due to slight variations
        foreach (var template in looseLoot)
        {
            var sanitizedId = template.GetSaneId();
            if (!uniqueLootIds.ContainsKey(sanitizedId))
            {
                uniqueLootIds[sanitizedId] = template.Id;
                preProcessedLoot.Counts[sanitizedId] =
                    preProcessedLoot.Counts.TryGetValue(sanitizedId, out var count)
                        ? count + 1
                        : 1;
            }

            if (!itemPropertiesDictionary.TryGetValue(sanitizedId, out var templates))
            {
                templates = new FlatKeyableList<Template>(_keyGenerator.Generate());
                itemPropertiesDictionary.Add(sanitizedId, templates);
            }

            templates.Add(template);
        }

        _dataStorage.Store(itemPropertiesDictionary);
        return preProcessedLoot;
    }

    public LooseLootRoot CreateLooseLootDistribution(
        string mapId,
        int mapCount,
        IKey looseLootCountKey
    )
    {
        var config = LootDumpProcessorContext.GetConfig();
        var spawnPointTolerance = config.ProcessorConfig.SpawnPointToleranceForForced / 100.0;
        var looseLootDistribution = new LooseLootRoot();

        var probabilities = new Dictionary<string, double>();
        var looseLootCountsItem = _dataStorage.GetItem<LooseLootCounts>(looseLootCountKey);

        var counts = _dataStorage.GetItem<FlatKeyableDictionary<string, int>>(looseLootCountsItem.Counts);
        foreach (var (itemId, count) in counts) probabilities[itemId] = (double)count / mapCount;

        var spawnPointCount = looseLootCountsItem.MapSpawnpointCount.Select(Convert.ToDouble).ToList();
        var initialMean = CalculateMean(spawnPointCount);
        var tolerancePercentage = config.ProcessorConfig.LooseLootCountTolerancePercentage / 100.0;
        var highThreshold = initialMean * (1 + tolerancePercentage);

        looseLootCountsItem.MapSpawnpointCount = looseLootCountsItem.MapSpawnpointCount
            .Where(count => count <= highThreshold)
            .ToList();

        looseLootDistribution.SpawnPointCount = new SpawnPointCount
        {
            Mean = CalculateMean(looseLootCountsItem.MapSpawnpointCount.Select(Convert.ToDouble).ToList()),
            Std = CalculateStandardDeviation(looseLootCountsItem.MapSpawnpointCount.Select(Convert.ToDouble)
                .ToList())
        };
        looseLootDistribution.SpawnPointsForced = new List<SpawnPointsForced>();
        looseLootDistribution.SpawnPoints = new List<SpawnPoint>();

        var itemProperties =
            _dataStorage.GetItem<FlatKeyableDictionary<string, IKey>>(looseLootCountsItem.ItemProperties);
        foreach (var (spawnPoint, itemListKey) in itemProperties)
        {
            var itemCounts = new Dictionary<ComposedKey, int>();
            var savedTemplates = _dataStorage.GetItem<FlatKeyableList<Template>>(itemListKey);

            foreach (var template in savedTemplates)
            {
                var composedKey = _composedKeyGenerator.Generate(template.Items);
                if (!itemCounts.TryAdd(composedKey, 1))
                    itemCounts[composedKey]++;
            }

            var groupedTemplates = savedTemplates
                .Select(t => (t.GetSaneId(), t))
                .GroupBy(g => g.Item1)
                .ToList();

            if (groupedTemplates.Count > 1)
                throw new Exception("Multiple sanitized IDs found for a single spawn point.");

            var spawnPoints = groupedTemplates.First().Select(g => g.t).ToList();
            var locationId = spawnPoints[0].GetLocationId();
            var templateCopy = ProcessorUtil.Copy(spawnPoints[0]);
            var itemDistributions = itemCounts.Select(kv => new ItemDistribution
            {
                ComposedKey = kv.Key,
                RelativeProbability = kv.Value
            }).ToList();

            if (itemDistributions.Count == 1 &&
                (_tarkovItemsProvider.IsQuestItem(itemDistributions[0].ComposedKey?.FirstItem?.Tpl) ||
                 LootDumpProcessorContext.GetForcedLooseItems()[mapId]
                     .Contains(itemDistributions[0].ComposedKey?.FirstItem?.Tpl)))
            {
                var forcedSpawnPoint = new SpawnPointsForced
                {
                    LocationId = locationId,
                    Probability = probabilities[spawnPoint],
                    Template = templateCopy
                };
                looseLootDistribution.SpawnPointsForced.Add(forcedSpawnPoint);
            }
            else if (probabilities[spawnPoint] > spawnPointTolerance)
            {
                var forcedSpawnPoint = new SpawnPointsForced
                {
                    LocationId = locationId,
                    Probability = probabilities[spawnPoint],
                    Template = templateCopy
                };
                looseLootDistribution.SpawnPointsForced.Add(forcedSpawnPoint);
                _logger.LogWarning(
                    "Item: {ItemId} has > {Tolerance}% spawn chance in spawn point: {LocationId} but isn't in forced loot, adding to forced",
                    templateCopy.Id,
                    config.ProcessorConfig.SpawnPointToleranceForForced,
                    forcedSpawnPoint.LocationId
                );
            }
            else
            {
                var spawnPointEntry = new SpawnPoint
                {
                    LocationId = locationId,
                    Probability = probabilities[spawnPoint],
                    Template = templateCopy,
                    ItemDistribution = itemDistributions
                };

                templateCopy.Items = new List<Item>();

                var groupedByKey = spawnPoints
                    .GroupBy(t => _composedKeyGenerator.Generate(t.Items))
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var distribution in itemDistributions)
                    if (groupedByKey.TryGetValue(distribution.ComposedKey, out var items))
                    {
                        var itemList = items.First().Items;
                        var originalItem = itemList.Find(i => string.IsNullOrEmpty(i.ParentId));
                        var originalId = originalItem.Id;
                        originalItem.Id = distribution.ComposedKey.Key;
                        foreach (var childItem in itemList.Where(i => i.ParentId == originalId))
                            childItem.ParentId = originalItem.Id;

                        templateCopy.Items.AddRange(itemList);
                    }
                    else
                    {
                        _logger.LogError(
                            "Item template {TemplateId} on loose loot distribution for spawn point {SpawnPointId} not found in spawn points.",
                            distribution.ComposedKey?.FirstItem?.Tpl,
                            templateCopy.Id
                        );
                    }

                looseLootDistribution.SpawnPoints.Add(spawnPointEntry);
            }
        }

        looseLootDistribution.SpawnPoints = looseLootDistribution.SpawnPoints
            .OrderBy(x => x.Template.Id)
            .ToList();

        var configuredForcedTemplates =
            new HashSet<string>(LootDumpProcessorContext.GetForcedLooseItems()[mapId].Select(item => item));
        var foundForcedTemplates =
            new HashSet<string>(looseLootDistribution.SpawnPointsForced.Select(fp => fp.Template.Items[0].Tpl));

        foreach (var expectedTpl in configuredForcedTemplates)
            if (!foundForcedTemplates.Contains(expectedTpl))
                _logger.LogError(
                    "Expected item: {ItemTpl} defined in forced_loose.yaml config not found in forced loot",
                    expectedTpl
                );

        foreach (var foundTpl in foundForcedTemplates)
            if (!configuredForcedTemplates.Contains(foundTpl))
                _logger.LogWarning(
                    "Map: {MapName} Item: {ItemTpl} not defined in forced_loose.yaml config but was flagged as forced by code",
                    mapId,
                    foundTpl
                );

        return looseLootDistribution;
    }

    private double CalculateMean(IReadOnlyList<double> numbers)
    {
        if (!numbers.Any()) return 0;

        return numbers.Average();
    }

    private double CalculateStandardDeviation(IReadOnlyList<double> numbers)
    {
        if (numbers.Count == 0) return 0;

        var mean = CalculateMean(numbers);
        var variance = numbers.Sum(num => Math.Pow(num - mean, 2)) / numbers.Count;
        return Math.Sqrt(variance);
    }
}