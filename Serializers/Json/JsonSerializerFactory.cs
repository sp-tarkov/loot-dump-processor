namespace LootDumpProcessor.Serializers.Json;

public static class JsonSerializerFactory
{
    private static readonly Dictionary<JsonSerializerTypes, IJsonSerializer> _jsonSerializers =
        new Dictionary<JsonSerializerTypes, IJsonSerializer>();

    private static object lockObject = new object();

    /**
     * Requires LootDumpProcessorContext to be initialized before using
     */
    public static IJsonSerializer GetInstance()
    {
        return GetInstance(LootDumpProcessorContext.GetConfig().JsonSerializer);
    }

    public static IJsonSerializer GetInstance(JsonSerializerTypes type)
    {
        IJsonSerializer serializer;
        lock (lockObject)
        {
            if (!_jsonSerializers.TryGetValue(type, out serializer))
            {
                switch (type)
                {
                    case JsonSerializerTypes.Newtonsoft:
                        serializer = new NewtonsoftJsonSerializer();
                        break;
                    case JsonSerializerTypes.DotNet:
                        serializer = new NetJsonSerializer();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }

                _jsonSerializers.Add(type, serializer);
            }
        }

        return serializer;
    }
}