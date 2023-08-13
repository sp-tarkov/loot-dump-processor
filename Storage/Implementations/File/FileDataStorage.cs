﻿namespace LootDumpProcessor.Storage.Implementations;

public class FileDataStorage : IDataStorage
{
    public void Setup()
    {
    }

    public void Store<T>(T t) where T : IKeyable
    {
        StoreHandlerFactory.GetInstance(t.GetKey().GetKeyType()).Store(t);
    }

    public bool Exists(IKey t)
    {
        return StoreHandlerFactory.GetInstance(t.GetKeyType()).Exists(t);
    }

    public T GetItem<T>(IKey key) where T : IKeyable
    {
        return StoreHandlerFactory.GetInstance(key.GetKeyType()).Retrieve<T>(key);
    }

    public List<T> GetAll<T>()
    {
        throw new NotImplementedException();
    }
}