using LootDumpProcessor.Serializers.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LootDumpProcessor.Serializers.Json;

public class NewtonsoftJsonSerializer : IJsonSerializer
{
    private static readonly JsonSerializerSettings _settings = new()
    {
        Converters =
        {
            new NewtonsoftJsonKeyConverter(),
            new StringEnumConverter(),
            new NewtonsoftDateTimeConverter()
        }
    };

    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, _settings);
    }

    public T? Deserialize<T>(string obj)
    {
        return JsonConvert.DeserializeObject<T>(obj, _settings);
    }
}