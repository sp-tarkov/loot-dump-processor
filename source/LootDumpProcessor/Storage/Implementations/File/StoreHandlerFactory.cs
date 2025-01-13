using System.Collections.Concurrent;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Storage.Implementations.File.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor.Storage.Implementations.File;

public class StoreHandlerFactory(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    private static readonly ConcurrentDictionary<KeyType, IStoreHandler> Handlers = new();

    public IStoreHandler GetInstance(KeyType type)
    {
        if (Handlers.TryGetValue(type, out var handler)) return handler;

        var config = _serviceProvider.GetRequiredService<IOptions<Config>>();
        handler = type switch
        {
            KeyType.Unique => new FlatStoreHandler(config),
            KeyType.Subdivisioned => new SubdivisionedStoreHandler(config),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        Handlers.TryAdd(type, handler);

        return handler;
    }
}