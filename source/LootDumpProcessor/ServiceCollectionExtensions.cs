using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Process;
using LootDumpProcessor.Process.Collector;
using LootDumpProcessor.Process.Processor.AmmoProcessor;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Processor.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.StaticContainersProcessor;
using LootDumpProcessor.Process.Processor.StaticLootProcessor;
using LootDumpProcessor.Process.Reader.Filters;
using LootDumpProcessor.Process.Reader.Intake;
using LootDumpProcessor.Process.Services.ComposedKeyGenerator;
using LootDumpProcessor.Process.Services.ForcedItemsProvider;
using LootDumpProcessor.Process.Services.KeyGenerator;
using LootDumpProcessor.Process.Services.TarkovItemsProvider;
using LootDumpProcessor.Process.Writer;
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
        AddCollector(services);
        AddDataStorage(services);
        RegisterProcessors(services);

        services.AddSingleton<StoreHandlerFactory>();

        services.AddSingleton<IForcedItemsProvider, ForcedItemsProvider>();
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