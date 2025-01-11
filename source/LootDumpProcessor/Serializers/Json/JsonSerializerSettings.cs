using System.Text.Json;
using System.Text.Json.Serialization;
using LootDumpProcessor.Serializers.Json.Converters;

namespace LootDumpProcessor.Serializers.Json;

public static class JsonSerializerSettings
{
    public static readonly JsonSerializerOptions Default = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        Converters =
        {
            new NetJsonKeyConverter(),
            new JsonStringEnumConverter(),
            new NetDateTimeConverter()
        },
        WriteIndented = true
    };
}