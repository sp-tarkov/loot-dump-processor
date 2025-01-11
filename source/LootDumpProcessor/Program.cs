using LootDumpProcessor.Process;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Processor.v2.AmmoProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticContainersProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LoggerFactory = LootDumpProcessor.Logger.LoggerFactory;

namespace LootDumpProcessor;

public static class Program
{
    public static async Task Main()
    {
        var services = new ServiceCollection();
        
        services.AddLogging(configure => configure.AddConsole());
        services.AddTransient<IStaticLootProcessor, StaticLootProcessor>();
        services.AddTransient<IStaticContainersProcessor, StaticContainersProcessor>();
        services.AddTransient<IAmmoProcessor, AmmoProcessor>();
        services.AddTransient<StaticLootProcessor>();

        services.AddTransient<IFileProcessor, FileProcessor>();
        services.AddTransient<IDumpProcessor, MultithreadSteppedDumpProcessor>();
        services.AddTransient<IPipeline, QueuePipeline>();

        await using var serviceProvider = services.BuildServiceProvider();
        
        // Bootstrap the config before anything else, its required by the whole application to work
        LootDumpProcessorContext.GetConfig();
        // Some loggers may need a startup and stop mechanism
        LoggerFactory.GetInstance().Setup();
        // Setup Data storage
        DataStorageFactory.GetInstance().Setup();
        // startup the pipeline
        var pipeline = serviceProvider.GetRequiredService<IPipeline>();
        pipeline.DoProcess();
        // stop loggers at the end
        LoggerFactory.GetInstance().Stop();
        Thread.Sleep(10000);
    }
}