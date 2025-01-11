using System.Collections.Concurrent;
using LootDumpProcessor.Storage.Implementations.File.Handlers;

namespace LootDumpProcessor.Storage.Implementations.File;

public class StoreHandlerFactory
{
    private static readonly ConcurrentDictionary<KeyType, IStoreHandler> Handlers = new();

    public static IStoreHandler GetInstance(KeyType type)
    {
        if (Handlers.TryGetValue(type, out var handler)) return handler;
        
        handler = type switch
        {
            KeyType.Unique => new FlatStoreHandler(),
            KeyType.Subdivisioned => new SubdivisionedStoreHandler(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        Handlers.TryAdd(type, handler);

        return handler;
    }
}