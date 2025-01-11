using LootDumpProcessor.Process;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Processor.v2.AmmoProcessor;
using LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Process.Reader.Filters;
using LootDumpProcessor.Process.Reader.Intake;
using LootDumpProcessor.Process.Reader.PreProcess;
using LootDumpProcessor.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor;

public static class Program
{
    public static async Task Main()
    {
        var services = new ServiceCollection();
        RegisterServices(services);

        await using var serviceProvider = services.BuildServiceProvider();
        
        // Setup Data storage
        DataStorageFactory.GetInstance().Setup();
        
        // startup the pipeline
        var pipeline = serviceProvider.GetRequiredService<IPipeline>();
        await pipeline.Execute();
    }

    private static void RegisterServices(ServiceCollection services)
    {
        services.AddLogging(configure => configure.AddConsole());
        
        services.AddTransient<IStaticLootProcessor, StaticLootProcessor>();
        services.AddTransient<IStaticContainersProcessor, StaticContainersProcessor>();
        services.AddTransient<IAmmoProcessor, AmmoProcessor>();
        services.AddTransient<ILooseLootProcessor, LooseLootProcessor>();

        services.AddSingleton<ITarkovItemsProvider, TarkovItemsProvider>();
        services.AddSingleton<IDataStorage>(_ => DataStorageFactory.GetInstance());
        services.AddTransient<IComposedKeyGenerator, ComposedKeyGenerator>();

        services.AddTransient<IIntakeReader, JsonFileIntakeReader>();
        services.AddTransient<IFileFilter, JsonDumpFileFilter>();
        services.AddTransient<IPreProcessReader, SevenZipPreProcessReader>();
        services.AddTransient<IFileProcessor, FileProcessor>();
        services.AddTransient<IDumpProcessor, MultithreadSteppedDumpProcessor>();
        services.AddTransient<IPipeline, QueuePipeline>();
    }
}