using FluentAssertions;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Process.Processor.LooseLootProcessor;
using LootDumpProcessor.Process.Services.ComposedKeyGenerator;
using LootDumpProcessor.Process.Services.ForcedItemsProvider;
using LootDumpProcessor.Process.Services.KeyGenerator;
using LootDumpProcessor.Process.Services.TarkovItemsProvider;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Storage.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace LootDumpProcessor.Tests.Unit;

public class LooseLootProcessorTests
{
    // PreProcessLooseLoot correctly processes list of templates and returns PreProcessedLooseLoot object
    [Fact]
    public void PreProcessLooseLoot_ProcessesTemplateList_ReturnsPreProcessedLooseLoot()
    {
        // Arrange
        var logger = Mock.Of<ILogger<LooseLootProcessor>>();
        var dataStorage = new Mock<IDataStorage>();
        var tarkovItemsProvider = Mock.Of<ITarkovItemsProvider>();
        var composedKeyGenerator = Mock.Of<IComposedKeyGenerator>();
        var keyGenerator = new NumericKeyGenerator();
        var config = Options.Create(new Config());
        var forcedItemsProvider = Mock.Of<IForcedItemsProvider>();

        var templates = new List<Template>
        {
            new()
            {
                Id = "template1", Items = new List<Item> { new() { Id = "item1" } }, Position = new Vector3(1, 0, 0)
            },
            new()
            {
                Id = "template2", Items = new List<Item> { new() { Id = "item2" } }, Position = new Vector3(0, 1, 0)
            }
        };

        var processor = new LooseLootProcessor(logger, dataStorage.Object, tarkovItemsProvider,
            composedKeyGenerator, keyGenerator, config, forcedItemsProvider);

        // Act
        var result = processor.PreProcessLooseLoot(templates);

        // Assert
        result.Should().NotBeNull();
        result.MapSpawnpointCount.Should().Be(2);
        result.Counts.Should().HaveCount(2);
        result.ItemProperties.Should().NotBeNull();
        dataStorage.Verify(x => x.Store(It.IsAny<SubdivisionedKeyableDictionary<string, List<Template>>>()),
            Times.Once);
    }

    // Handle empty input list of templates in PreProcessLooseLoot
    [Fact]
    public void PreProcessLooseLoot_WithEmptyTemplateList_ReturnsEmptyPreProcessedLooseLoot()
    {
        // Arrange
        var logger = Mock.Of<ILogger<LooseLootProcessor>>();
        var dataStorage = new Mock<IDataStorage>();
        var tarkovItemsProvider = Mock.Of<ITarkovItemsProvider>();
        var composedKeyGenerator = Mock.Of<IComposedKeyGenerator>();
        var keyGenerator = new NumericKeyGenerator();

        var config = Options.Create(new Config());
        var forcedItemsProvider = Mock.Of<IForcedItemsProvider>();

        var templates = new List<Template>();

        var processor = new LooseLootProcessor(logger, dataStorage.Object, tarkovItemsProvider,
            composedKeyGenerator, keyGenerator, config, forcedItemsProvider);

        // Act
        var result = processor.PreProcessLooseLoot(templates);

        // Assert
        result.Should().NotBeNull();
        result.MapSpawnpointCount.Should().Be(0);
        result.Counts.Should().BeEmpty();
        result.ItemProperties.Should().NotBeNull();
        dataStorage.Verify(x => x.Store(It.IsAny<SubdivisionedKeyableDictionary<string, List<Template>>>()),
            Times.Once);
    }

    // Process templates with duplicate sanitized IDs
    [Fact]
    public void PreProcessLooseLoot_WithDuplicateSanitizedIds_CombinesDuplicatesInCounts()
    {
        // Arrange
        var logger = Mock.Of<ILogger<LooseLootProcessor>>();
        var dataStorage = new Mock<IDataStorage>();
        var tarkovItemsProvider = Mock.Of<ITarkovItemsProvider>();
        var composedKeyGenerator = Mock.Of<IComposedKeyGenerator>();
        var keyGenerator = new NumericKeyGenerator();
        var config = Options.Create(new Config());
        var forcedItemsProvider = Mock.Of<IForcedItemsProvider>();

        var templates = new List<Template>
        {
            new() { Id = "template1", Items = new List<Item> { new() { Id = "item1", Tpl = "tpl1" } } },
            new() { Id = "template1", Items = new List<Item> { new() { Id = "item2", Tpl = "tpl1" } } }
        };

        var processor = new LooseLootProcessor(logger, dataStorage.Object, tarkovItemsProvider,
            composedKeyGenerator, keyGenerator, config, forcedItemsProvider);

        // Act
        var result = processor.PreProcessLooseLoot(templates);

        // Assert
        result.Should().NotBeNull();
        result.MapSpawnpointCount.Should().Be(2);
        result.Counts.Should().HaveCount(1);
        dataStorage.Verify(x => x.Store(It.IsAny<SubdivisionedKeyableDictionary<string, List<Template>>>()),
            Times.Once);
    }
}