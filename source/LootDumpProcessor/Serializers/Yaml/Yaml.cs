using YamlDotNet.Serialization;

namespace LootDumpProcessor.Serializers.Yaml;

public static class Yaml
{
    public static readonly ISerializer Serializer = new SerializerBuilder().Build();

    public static readonly IDeserializer Deserializer = new DeserializerBuilder().Build();
}