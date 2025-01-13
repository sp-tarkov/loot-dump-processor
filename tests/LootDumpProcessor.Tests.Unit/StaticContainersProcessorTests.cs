using System.Collections.Frozen;
using FluentAssertions;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Process.Processor.StaticContainersProcessor;
using LootDumpProcessor.Process.Services.ForcedItemsProvider;
using Microsoft.Extensions.Logging;
using Moq;

namespace LootDumpProcessor.Tests.Unit;

public class StaticContainersProcessorTests
{
    // Successfully process static weapons and forced containers from valid map data
    [Fact]
    public async Task CreateStaticWeaponsAndForcedContainers_WithValidMapData_ReturnsExpectedResult()
    {
        // Arrange
        var logger = Mock.Of<ILogger<StaticContainersProcessor>>();
        var forcedItemsProvider = new Mock<IForcedItemsProvider>();

        var staticWeaponId = "weapon123";
        var mapId = "customs";
        var forcedStatic = new ForcedStatic
        (
            [staticWeaponId],
            new Dictionary<string, IReadOnlyList<StaticForced>>
            {
                {
                    mapId, new List<StaticForced>
                    {
                        new() { ContainerId = "forced1" }
                    }
                }
            }.ToFrozenDictionary()
        );

        forcedItemsProvider.Setup(x => x.GetForcedStatic())
            .ReturnsAsync(forcedStatic);

        var processor = new StaticContainersProcessor(logger, forcedItemsProvider.Object);

        var rootData = new RootData
        {
            Data = new Data
            {
                LocationLoot = new LocationLoot
                {
                    Id = mapId,
                    Loot = new List<Template>
                    {
                        new()
                        {
                            Id = "container1",
                            IsContainer = true,
                            Items = new List<Item> { new() { Tpl = staticWeaponId } }
                        }
                    }
                }
            }
        };

        // Act
        var result = await processor.CreateStaticWeaponsAndForcedContainers(rootData);

        // Assert
        result.StaticWeapons.Should().HaveCount(1);
        result.StaticWeapons.First().Items.First().Tpl.Should().Be(staticWeaponId);
        result.StaticForced.Should().HaveCount(1);
        result.StaticForced.First().ContainerId.Should().Be("forced1");
    }
    
    // Process map data with no static weapons or forced containers
    [Fact]
    public async Task CreateStaticWeaponsAndForcedContainers_WithNoStaticWeapons_ReturnsEmptyCollections()
    {
        // Arrange
        var logger = Mock.Of<ILogger<StaticContainersProcessor>>();
        var forcedItemsProvider = new Mock<IForcedItemsProvider>();

        forcedItemsProvider.Setup(x => x.GetForcedStatic())
            .ReturnsAsync(new ForcedStatic([],
                new Dictionary<string, IReadOnlyList<StaticForced>>().ToFrozenDictionary()));

        var processor = new StaticContainersProcessor(logger, forcedItemsProvider.Object);

        var rootData = new RootData
        {
            Data = new Data
            {
                LocationLoot = new LocationLoot
                {
                    Id = "customs",
                    Loot = new List<Template>
                    {
                        new()
                        {
                            Id = "container1",
                            IsContainer = true,
                            Items = new List<Item> { new() { Tpl = "nonweapon" } }
                        }
                    }
                }
            }
        };

        // Act
        var result = await processor.CreateStaticWeaponsAndForcedContainers(rootData);

        // Assert
        result.StaticWeapons.Should().BeEmpty();
        result.StaticForced.Should().BeEmpty();
    }
}