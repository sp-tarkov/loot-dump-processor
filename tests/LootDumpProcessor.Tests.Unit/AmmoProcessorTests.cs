using FluentAssertions;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor.AmmoProcessor;
using LootDumpProcessor.Process.Services.TarkovItemsProvider;
using Microsoft.Extensions.Logging;
using Moq;

namespace LootDumpProcessor.Tests.Unit;

public class AmmoProcessorTests
{
    // Process list of containers and return dictionary mapping calibers to ammo distributions
    [Fact]
    public void CreateAmmoDistribution_WithValidContainers_ReturnsExpectedDistribution()
    {
        // Arrange
        var logger = Mock.Of<ILogger<AmmoProcessor>>();
        var tarkovItemsProvider = new Mock<ITarkovItemsProvider>();

        var containers = new List<PreProcessedStaticLoot>
        {
            new() { Items = new List<Item> { new() { Tpl = "ammo1" }, new() { Tpl = "ammo2" } } }
        };

        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo1", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo2", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo1")).Returns("5.56x45");
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo2")).Returns("5.56x45");

        var processor = new AmmoProcessor(logger, tarkovItemsProvider.Object);

        // Act
        var result = processor.CreateAmmoDistribution("woods", containers);

        // Assert
        result.Should().ContainKey("5.56x45");
        result["5.56x45"].Should().HaveCount(2);
        result["5.56x45"].Sum(x => x.RelativeProbability).Should().Be(2);
    }

    // Handle empty containers list
    [Fact]
    public void CreateAmmoDistribution_WithEmptyContainers_ReturnsEmptyDictionary()
    {
        // Arrange
        var logger = Mock.Of<ILogger<AmmoProcessor>>();
        var tarkovItemsProvider = Mock.Of<ITarkovItemsProvider>();
        var processor = new AmmoProcessor(logger, tarkovItemsProvider);
        var emptyContainers = new List<PreProcessedStaticLoot>();

        // Act
        var result = processor.CreateAmmoDistribution("woods", emptyContainers);

        // Assert
        result.Should().BeEmpty();
    }

    // Handle containers with no ammo items
    [Fact]
    public void CreateAmmoDistribution_WithNoAmmoItems_ReturnsEmptyDictionary()
    {
        // Arrange
        var logger = Mock.Of<ILogger<AmmoProcessor>>();
        var tarkovItemsProvider = new Mock<ITarkovItemsProvider>();

        var containers = new List<PreProcessedStaticLoot>
        {
            new() { Items = new List<Item> { new() { Tpl = "non_ammo1" }, new() { Tpl = "non_ammo2" } } }
        };

        tarkovItemsProvider
            .Setup(x => x.IsBaseClass(It.IsAny<string>(), BaseClasses.Ammo))
            .Returns(false);

        var processor = new AmmoProcessor(logger, tarkovItemsProvider.Object);

        // Act
        var result = processor.CreateAmmoDistribution("woods", containers);

        // Assert
        result.Should().BeEmpty();
    }

    // Group ammo items by template and calculate count for each template
    [Fact]
    public void CreateAmmoDistribution_WithMultipleCalibers_ReturnsGroupedDistribution()
    {
        // Arrange
        var logger = Mock.Of<ILogger<AmmoProcessor>>();
        var tarkovItemsProvider = new Mock<ITarkovItemsProvider>();

        var containers = new List<PreProcessedStaticLoot>
        {
            new()
            {
                Items = new List<Item> { new() { Tpl = "ammo1" }, new() { Tpl = "ammo2" }, new() { Tpl = "ammo3" } }
            }
        };

        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo1", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo2", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo3", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo1")).Returns("5.56x45");
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo2")).Returns("7.62x39");
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo3")).Returns("5.56x45");

        var processor = new AmmoProcessor(logger, tarkovItemsProvider.Object);

        // Act
        var result = processor.CreateAmmoDistribution("customs", containers);

        // Assert
        result.Should().ContainKey("5.56x45");
        result.Should().ContainKey("7.62x39");
        result["5.56x45"].Should().HaveCount(2);
        result["7.62x39"].Should().HaveCount(1);
        result["5.56x45"].Sum(x => x.RelativeProbability).Should().Be(2);
        result["7.62x39"].Sum(x => x.RelativeProbability).Should().Be(1);
    }

    // Calculate relative probabilities for ammo distributions within each caliber group
    [Fact]
    public void CreateAmmoDistribution_WithMultipleCalibers_ReturnsExpectedDistribution()
    {
        // Arrange
        var logger = Mock.Of<ILogger<AmmoProcessor>>();
        var tarkovItemsProvider = new Mock<ITarkovItemsProvider>();

        var containers = new List<PreProcessedStaticLoot>
        {
            new()
            {
                Items = new List<Item> { new() { Tpl = "ammo1" }, new() { Tpl = "ammo2" }, new() { Tpl = "ammo3" } }
            }
        };

        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo1", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo2", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.IsBaseClass("ammo3", BaseClasses.Ammo)).Returns(true);
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo1")).Returns("5.56x45");
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo2")).Returns("7.62x39");
        tarkovItemsProvider.Setup(x => x.AmmoCaliber("ammo3")).Returns("5.56x45");

        var processor = new AmmoProcessor(logger, tarkovItemsProvider.Object);

        // Act
        var result = processor.CreateAmmoDistribution("customs", containers);

        // Assert
        result.Should().ContainKey("5.56x45");
        result.Should().ContainKey("7.62x39");
        result["5.56x45"].Should().HaveCount(2);
        result["7.62x39"].Should().HaveCount(1);
        result["5.56x45"].Sum(x => x.RelativeProbability).Should().Be(2);
        result["7.62x39"].Sum(x => x.RelativeProbability).Should().Be(1);
    }
}