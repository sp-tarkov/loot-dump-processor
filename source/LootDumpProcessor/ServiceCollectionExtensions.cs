﻿using System.Collections.Frozen;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Process;
using LootDumpProcessor.Process.Collector;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Processor.v2.AmmoProcessor;
using LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Process.Reader.Filters;
using LootDumpProcessor.Process.Reader.Intake;
using LootDumpProcessor.Process.Writer;
using LootDumpProcessor.Serializers.Yaml;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Storage.Implementations.File;
using LootDumpProcessor.Storage.Implementations.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor;

public static class ServiceCollectionExtensions
{
    public static void AddLootDumpProcessor(this ServiceCollection services)
    {
        services.AddLogging(configure => configure.AddConsole());
        AddConfiguration(services);
        AddForcedStatic(services);
        AddCollector(services);
        AddDataStorage(services);
        RegisterProcessors(services);

        services.AddSingleton<StoreHandlerFactory>();

        services.AddSingleton<ITarkovItemsProvider, TarkovItemsProvider>();
        services.AddSingleton<IKeyGenerator, NumericKeyGenerator>();
        services.AddTransient<IComposedKeyGenerator, ComposedKeyGenerator>();

        services.AddTransient<IWriter, FileWriter>();
        services.AddTransient<IIntakeReader, JsonFileIntakeReader>();
        services.AddTransient<IFileFilter, JsonDumpFileFilter>();
        services.AddTransient<IFileProcessor, FileProcessor>();
        services.AddTransient<IDumpProcessor, MultithreadSteppedDumpProcessor>();
        services.AddTransient<IPipeline, QueuePipeline>();
    }

    private static void AddConfiguration(IServiceCollection services)
    {
        const string configPath = "Config/config.json";
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configPath)
            .AddEnvironmentVariables()
            .Build();

        services.AddOptions<Config>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddForcedStatic(IServiceCollection services)
    {
        const string forcedStaticPath = "Config/forced_static.yaml";
        var forcedStaticContent = File.ReadAllText(forcedStaticPath);

        // Workaround needed because YamlDotNet cannot deserialize properly
        var forcedStaticDto = Yaml.Deserializer.Deserialize<ForcedStaticDto>(forcedStaticContent);
        var forcedStatic = new ForcedStatic(
            forcedStaticDto.StaticWeaponIds.AsReadOnly(),
            forcedStaticDto.ForcedItems.ToFrozenDictionary(
                kvp => kvp.Key,
                IReadOnlyList<StaticForced> (kvp) => kvp.Value.AsReadOnly()
            ));

        services.AddSingleton(forcedStatic);
    }

    private static void RegisterProcessors(IServiceCollection services)
    {
        services.AddTransient<IStaticLootProcessor, StaticLootProcessor>();
        services.AddTransient<IStaticContainersProcessor, StaticContainersProcessor>();
        services.AddTransient<IAmmoProcessor, AmmoProcessor>();
        services.AddTransient<ILooseLootProcessor, LooseLootProcessor>();
    }

    private static void AddCollector(IServiceCollection services)
    {
        services.AddSingleton<ICollector>(provider =>
        {
            var config = provider.GetRequiredService<IOptions<Config>>();
            var collectorType = config.Value.CollectorConfig.CollectorType;
            return collectorType switch
            {
                CollectorType.Memory => new HashSetCollector(),
                CollectorType.Dump => new DumpCollector(config),
                _ => throw new ArgumentOutOfRangeException($"CollectorType '{collectorType} is not supported'")
            };
        });
    }

    private static void AddDataStorage(IServiceCollection services)
    {
        services.AddSingleton<IDataStorage>(provider =>
        {
            var config = provider.GetRequiredService<IOptions<Config>>().Value;
            var dataStorageType = config.DataStorageConfig.DataStorageType;
            return dataStorageType switch
            {
                DataStorageTypes.File => new FileDataStorage(provider.GetRequiredService<StoreHandlerFactory>()),
                DataStorageTypes.Memory => new MemoryDataStorage(),
                _ => throw new ArgumentOutOfRangeException($"DataStorageType '{dataStorageType} is not supported'")
            };
        });
    }
}