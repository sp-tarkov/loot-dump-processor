using System.Globalization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Serializers.Json.Converters;

public class NewtonsoftDateTimeConverter : JsonConverter<DateTime>
{
    private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(DateTimeFormat));
    }

    public override DateTime ReadJson(
        JsonReader reader,
        Type objectType,
        DateTime existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    )
    {
        var stringDate = reader.Value?.ToString() ?? "";
        if (!DateTime.TryParseExact(stringDate, DateTimeFormat, null, DateTimeStyles.None, out var parsedDate))
            throw new Exception($"Invalid value for DateTime format: {DateTimeFormat}");
        return parsedDate;
    }
}