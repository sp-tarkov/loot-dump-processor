using Newtonsoft.Json;

namespace LootDumpProcessor.Storage;

public class NewtonsoftJsonKeyConverter : JsonConverter<AbstractKey>
{
    public override void WriteJson(JsonWriter writer, AbstractKey? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
        }
        else
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue(value.GetKeyType().ToString());
            writer.WritePropertyName("serializedKey");
            writer.WriteValue(value.SerializedKey);
            writer.WriteEndObject();
        }
    }

    public override AbstractKey? ReadJson(
        JsonReader reader,
        Type objectType,
        AbstractKey? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    )
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        while (reader.Read())
        {
            var property = reader.Value?.ToString() ?? "";
            reader.Read();
            var value = reader.Value?.ToString() ?? "";
            values.Add(property, value);
            if (values.Count == 2)
                break;
        }

        reader.Read();

        if (!values.TryGetValue("type", out var type))
        {
            throw new Exception("Key type was missing from json definition");
        }

        if (!values.TryGetValue("serializedKey", out var serializedKey))
        {
            throw new Exception("Key serializedKey was missing from json definition");
        }

        AbstractKey key;
        switch (Enum.Parse<KeyType>(type))
        {
            case KeyType.Subdivisioned:
                key = new SubdivisionedUniqueKey(serializedKey.Split("|"));
                break;
            case KeyType.Unique:
                key = new FlatUniqueKey(serializedKey.Split("|"));
                break;
            default:
                throw new Exception("Unknown key type used!");
        }

        return key;
    }
}