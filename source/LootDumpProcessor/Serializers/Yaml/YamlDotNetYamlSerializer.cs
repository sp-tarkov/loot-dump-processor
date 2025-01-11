using YamlDotNet.Serialization;

namespace LootDumpProcessor.Serializers.Yaml;

public class YamlDotNetYamlSerializer : IYamlSerializer
{
    private static readonly IDeserializer yamlDeserializer = new DeserializerBuilder().Build();
    private static readonly YamlDotNet.Serialization.ISerializer yamlSerializer = new SerializerBuilder().Build();

    public string Serialize<T>(T obj) => yamlSerializer.Serialize(obj);

    public T Deserialize<T>(string obj) => yamlDeserializer.Deserialize<T>(obj);
}