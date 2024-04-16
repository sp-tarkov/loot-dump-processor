namespace LootDumpProcessor.Serializers.Json;

public static class JsonSerializerFactory
{
    private static readonly Dictionary<JsonSerializerTypes, IJsonSerializer> _jsonSerializers = new();
    private static object lockObject = new();

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
                serializer = type switch
                {
                    JsonSerializerTypes.Newtonsoft => new NewtonsoftJsonSerializer(),
                    JsonSerializerTypes.DotNet => new NetJsonSerializer(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };

                _jsonSerializers.Add(type, serializer);
            }
        }

        return serializer;
    }
}