﻿using LootDumpProcessor.Storage.Implementations.Handlers;

namespace LootDumpProcessor.Storage.Implementations;

public class StoreHandlerFactory
{
    private static Dictionary<KeyType, IStoreHandler> _handlers = new Dictionary<KeyType, IStoreHandler>();
    private static object lockObject = new object();

    public static IStoreHandler GetInstance(KeyType type)
    {
        IStoreHandler handler;
        lock (lockObject)
        {
            if (!_handlers.TryGetValue(type, out handler))
            {
                handler = type switch
                {
                    KeyType.Unique => new FlatStoreHandler(),
                    KeyType.Subdivisioned => new SubdivisionedStoreHandler(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };
                _handlers.Add(type, handler);
            }
        }

        return handler;
    }
}