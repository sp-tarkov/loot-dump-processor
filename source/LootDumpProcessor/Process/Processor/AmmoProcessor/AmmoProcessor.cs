using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Services.TarkovItemsProvider;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Processor.AmmoProcessor;

public class AmmoProcessor(ILogger<AmmoProcessor> logger, ITarkovItemsProvider tarkovItemsProvider) : IAmmoProcessor
{
    private readonly ILogger<AmmoProcessor> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly ITarkovItemsProvider _tarkovItemsProvider =
        tarkovItemsProvider ?? throw new ArgumentNullException(nameof(tarkovItemsProvider));

    public IReadOnlyDictionary<string, List<AmmoDistribution>> CreateAmmoDistribution(List<PreProcessedStaticLoot> containers)
    {
        var ammoTemplates = containers
            .SelectMany(container => container.Items)
            .Where(item => _tarkovItemsProvider.IsBaseClass(item.Tpl, BaseClasses.Ammo))
            .Select(item => item.Tpl)
            .ToList();

        var caliberTemplateCounts = ammoTemplates
            .GroupBy(tpl => tpl)
            .Select(group => new CaliberTemplateCount
            {
                Caliber = _tarkovItemsProvider.AmmoCaliber(group.Key),
                Template = group.Key,
                Count = group.Count()
            })
            .OrderBy(ctc => ctc.Caliber)
            .ToList();

        var ammoDistribution = caliberTemplateCounts
            .GroupBy(ctc => ctc.Caliber)
            .ToDictionary(
                group => group.Key,
                group => group.Select(ctc => new AmmoDistribution
                {
                    Tpl = ctc.Template,
                    RelativeProbability = ctc.Count
                }).ToList()
            );

        return ammoDistribution;
    }
}