using FluentAssertions;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor.AmmoProcessor;
using LootDumpProcessor.Process.Services.TarkovItemsProvider;
using LootDumpProcessor.Tests.Unit.Processors.Ammo.Tables;
using Microsoft.Extensions.Logging;
using Moq;

namespace LootDumpProcessor.Tests.Unit.Processors.Ammo;

[Binding]
public class AmmoProcessorSteps(ScenarioContext scenarioContext)
{
    private readonly ScenarioContext _scenarioContext =
        scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));

    [Given("the following ammo:")]
    public void GivenTheFollowingAmmo(Table ammoTable)
    {
        var ammoRows = ammoTable.GetAmmo();

        var itemsProviderMock = new Mock<ITarkovItemsProvider>();
        foreach (var ammo in ammoRows)
        {
            itemsProviderMock.Setup(mock => mock.IsBaseClass(ammo.Tpl, BaseClasses.Ammo))
                .Returns(true);
            itemsProviderMock.Setup(mock => mock.AmmoCaliber(ammo.Tpl))
                .Returns(ammo.Caliber);
        }

        _scenarioContext.Set(ammoRows);
        _scenarioContext.Set(itemsProviderMock.Object);
    }

    [Given("a container with items:")]
    public void GivenAContainerWithItems(Table containerItemsTable)
    {
        var containerItemRows = containerItemsTable.GetContainerItems();
        var containerItemTpls = containerItemRows.Select(row => row.Tpl);

        var ammo = _scenarioContext.Get<IReadOnlyList<AmmoTable.Row>>();

        var items = ammo
            .Where(item => containerItemTpls.Contains(item.Tpl))
            .Select(item => new Item
            {
                Tpl = item.Tpl,
            })
            .ToList();

        var loot = new PreProcessedStaticLoot
        {
            Items = items
        };

        if (_scenarioContext.TryGetValue(out List<PreProcessedStaticLoot> lootList))
        {
            lootList.Add(loot);
            _scenarioContext.Set(lootList);
        }
        else _scenarioContext.Set(new List<PreProcessedStaticLoot> { loot });
    }

    [When("I create an ammo distribution")]
    public void WhenICreateAnAmmoDistribution()
    {
        var logger = Mock.Of<ILogger<AmmoProcessor>>();
        var itemsProvider = _scenarioContext.Get<ITarkovItemsProvider>();

        var ammoProcessor = new AmmoProcessor(logger, itemsProvider);

        var containers = _scenarioContext.Get<List<PreProcessedStaticLoot>>();
        var distribution = ammoProcessor.CreateAmmoDistribution(containers);
        _scenarioContext.Set(distribution);
    }

    [Then("the distribution should only contain the following calibers:")]
    public void ThenTheDistributionShouldOnlyContainTheFollowingCalibers(Table calibersTable)
    {
        var caliberRows = calibersTable.GetCalibers();
        var actual = _scenarioContext.Get<IReadOnlyDictionary<string, List<AmmoDistribution>>>();

        var calibers = caliberRows.Select(row => row.Caliber);
        if (actual.Any()) actual.Should().OnlyContain(pair => calibers.Contains(pair.Key));
        else actual.Should().BeEmpty();
    }

    [Then("the calibers counts should be:")]
    public void ThenTheCalibersCountsShouldBe(Table caliberCountsTable)
    {
        var caliberCounts = caliberCountsTable.GetCalibersCount();
        var actual = _scenarioContext.Get<IReadOnlyDictionary<string, List<AmmoDistribution>>>();

        foreach (var caliberCount in caliberCounts)
        {
            actual.Should().ContainKey(caliberCount.Caliber);
            actual[caliberCount.Caliber].Count.Should().Be(caliberCount.Count);
        }
    }

    [Then("relative probabilities should be:")]
    public void ThenRelativeProbabilitiesShouldBe(Table table)
    {
        var probabilities = table.GetProbabilities();
        var actual = _scenarioContext.Get<IReadOnlyDictionary<string, List<AmmoDistribution>>>();

        foreach (var probability in probabilities)
        {
            actual.Should().ContainKey(probability.Caliber);
            actual[probability.Caliber].Count.Should().Be(probability.Count);
        }
    }
}