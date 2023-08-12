using LootDumpProcessor.Serializers;
using LootDumpProcessor.Serializers.Yaml;

namespace LootDumpProcessor.Storage.Implementations.Serializers;

public class YamlDataStorageFileSerializer : IDataStorageFileSerializer
{
    public string GetExtension() => ".yaml";

    public ISerializer GetSerializer()
    {
        return YamlSerializerFactory.GetInstance();
    }
}