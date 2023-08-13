using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LootDumpProcessor.Serializers.Json.Converters;

public class NetDateTimeConverter : JsonConverter<DateTime>
{
    private static string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringDate = reader.GetString() ?? "";
        if (!DateTime.TryParseExact(stringDate, _dateTimeFormat, null, DateTimeStyles.None, out var parsedDate))
            throw new Exception($"Invalid value for DateTime format: {_dateTimeFormat}");
        return parsedDate;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateTimeFormat));
    }
}