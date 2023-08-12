using LootDumpProcessor.Serializers;

namespace LootDumpProcessor.Storage.Implementations.Serializers;

public interface IDataStorageFileSerializer
{
    string GetExtension();
    ISerializer GetSerializer();
}