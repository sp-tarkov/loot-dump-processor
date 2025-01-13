using System.Collections.Frozen;
using FluentAssertions;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor.StaticLootProcessor;
using LootDumpProcessor.Process.Services.ForcedItemsProvider;
using Microsoft.Extensions.Logging;
using Moq;

namespace LootDumpProcessor.Tests.Unit;

public class StaticLootProcessorTests
{
    // Process non-weapon containers from static loot list
    [Fact]
    public async Task PreProcessStaticLoot_WithNonWeaponContainers_ReturnsProcessedContainers()
    {
        // Arrange
        var logger = Mock.Of<ILogger<StaticLootProcessor>>();
        var forcedItemsProviderMock = new Mock<IForcedItemsProvider>();
        forcedItemsProviderMock.Setup(provider => provider.GetForcedStatic())
            .ReturnsAsync(new ForcedStatic(new List<string> { "weapon_tpl" },
                FrozenDictionary<string, IReadOnlyList<StaticForced>>.Empty));

        var processor = new StaticLootProcessor(logger, forcedItemsProviderMock.Object);

        var staticLoot = new List<Template>
        {
            new("1", "pos1", false, true, true, new Vector3(0, 0, 0),
                new Vector3(0, 0, 0), false, [], false,
                "parent1", new List<Item>
                {
                    new() { Id = "container1", Tpl = "container_tpl" },
                    new() { Id = "item1", Tpl = "item_tpl", ParentId = "container1" }
                })
        };

        // Act
        var result = await processor.PreProcessStaticLoot(staticLoot);

        // Assert
        result.Should().HaveCount(1);
        result.First().Type.Should().Be("container_tpl");
        result.First().ContainerId.Should().Be("container1");
        result.First().Items.Should().HaveCount(1);
    }

    // Handle empty static loot list
    [Fact]
    public async Task PreProcessStaticLoot_WithEmptyList_ReturnsEmptyResult()
    {
        // Arrange
        var logger = Mock.Of<ILogger<StaticLootProcessor>>();
        var forcedItemsProvider = new ForcedItemsProvider();
        var processor = new StaticLootProcessor(logger, forcedItemsProvider);

        var staticLoot = new List<Template>();

        // Act
        var result = await processor.PreProcessStaticLoot(staticLoot);

        // Assert
        result.Should().BeEmpty();
    }

    // Create distribution dictionary for different container types
    [Fact]
    public void CreateStaticLootDistribution_WithValidContainers_ReturnsCorrectDistribution()
    {
        // Arrange
        var logger = Mock.Of<ILogger<StaticLootProcessor>>();
        var forcedItemsProvider = new ForcedItemsProvider();
        var processor = new StaticLootProcessor(logger, forcedItemsProvider);

        var containers = new List<PreProcessedStaticLoot>
        {
            new()
            {
                Type = "container_type1",
                ContainerId = "container1",
                Items = new List<Item>
                {
                    new() { Id = "item1", Tpl = "item_tpl", ParentId = "container1" },
                    new() { Id = "item2", Tpl = "item2_tpl", ParentId = "container1" }
                }
            },
            new()
            {
                Type = "container_type2",
                ContainerId = "container2",
                Items = new List<Item>
                {
                    new() { Id = "item3", Tpl = "item3_tpl", ParentId = "container1" },
                }
            }
        };

        // Act
        var result = processor.CreateStaticLootDistribution("map", containers);

        // Assert
        result.Should().ContainKey("container_type1");
        var firstDistribution = result["container_type1"];
        firstDistribution.ItemCountDistribution.Should().HaveCount(1);
        firstDistribution.ItemCountDistribution.First().RelativeProbability.Should().Be(1);
        firstDistribution.ItemDistribution.Should().HaveCount(2);
    }
}