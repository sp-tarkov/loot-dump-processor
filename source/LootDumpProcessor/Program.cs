using LootDumpProcessor.Process;
using Microsoft.Extensions.DependencyInjection;

namespace LootDumpProcessor;

public static class Program
{
    public static async Task Main()
    {
        var services = new ServiceCollection();
        services.AddLootDumpProcessor();

        await using var serviceProvider = services.BuildServiceProvider();

        var pipeline = serviceProvider.GetRequiredService<IPipeline>();
        await pipeline.Execute();
    }
}