using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Processor.v2.AmmoProcessor;

public class AmmoProcessor(ILogger<AmmoProcessor> logger) : IAmmoProcessor
{
    private readonly ILogger<AmmoProcessor> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public IReadOnlyDictionary<string, List<AmmoDistribution>> CreateAmmoDistribution(
        string mapId,
        List<PreProcessedStaticLoot> containers)
    {
        var ammoTemplates = containers
            .SelectMany(container => container.Items)
            .Where(item => LootDumpProcessorContext.GetTarkovItems().IsBaseClass(item.Tpl, BaseClasses.Ammo))
            .Select(item => item.Tpl)
            .ToList();

        var caliberTemplateCounts = ammoTemplates
            .GroupBy(tpl => tpl)
            .Select(group => new CaliberTemplateCount
            {
                Caliber = LootDumpProcessorContext.GetTarkovItems().AmmoCaliber(group.Key),
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

        _logger.LogInformation("Created ammo distribution for Map {MapId}.", mapId);

        return ammoDistribution;
    }
}