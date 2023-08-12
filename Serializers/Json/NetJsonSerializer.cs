﻿using System.Text.Json;
using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;

namespace LootDumpProcessor.Serializers.Json;

public class NetJsonSerializer : IJsonSerializer
{
    private static JsonSerializerOptions _serializeOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new NetJsonKeyConverter(),
            new JsonStringEnumConverter()
        }
    };
    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, _serializeOptions);
    }

    public T Deserialize<T>(string obj)
    {
        return JsonSerializer.Deserialize<T>(obj, _serializeOptions);
    }
}