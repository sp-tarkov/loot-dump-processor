﻿using LootDumpProcessor.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LootDumpProcessor.Serializers.Json;

public class NewtonsoftJsonSerializer : IJsonSerializer
{
    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        Converters =
        {
            new NewtonsoftJsonKeyConverter(),
            new StringEnumConverter()
        }
    };

    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, _settings);
    }

    public T Deserialize<T>(string obj)
    {
        return JsonConvert.DeserializeObject<T>(obj, _settings);
    }
}