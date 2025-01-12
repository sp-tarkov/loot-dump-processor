using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Process;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Processor.v2.AmmoProcessor;
using LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Process.Reader.Filters;
using LootDumpProcessor.Process.Reader.Intake;
using LootDumpProcessor.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor;

public static class ServiceCollectionExtensions
{
    public static void AddLootDumpProcessor(this ServiceCollection services)
    {
        services.AddLogging(configure => configure.AddConsole());
        AddConfiguration(services);
        RegisterProcessors(services);

        services.AddSingleton<ITarkovItemsProvider, TarkovItemsProvider>();
        services.AddSingleton<IDataStorage>(_ => DataStorageFactory.GetInstance());
        services.AddSingleton<IKeyGenerator, NumericKeyGenerator>();
        services.AddTransient<IComposedKeyGenerator, ComposedKeyGenerator>();

        services.AddTransient<IIntakeReader, JsonFileIntakeReader>();
        services.AddTransient<IFileFilter, JsonDumpFileFilter>();
        services.AddTransient<IFileProcessor, FileProcessor>();
        services.AddTransient<IDumpProcessor, MultithreadSteppedDumpProcessor>();
        services.AddTransient<IPipeline, QueuePipeline>();
    }

    private static void RegisterProcessors(IServiceCollection services)
    {
        services.AddTransient<IStaticLootProcessor, StaticLootProcessor>();
        services.AddTransient<IStaticContainersProcessor, StaticContainersProcessor>();
        services.AddTransient<IAmmoProcessor, AmmoProcessor>();
        services.AddTransient<ILooseLootProcessor, LooseLootProcessor>();
    }

    private static void AddConfiguration(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("Config/config.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddOptions<Config>()
            .Bind(configuration)
            .ValidateOnStart();
    }
}