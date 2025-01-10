using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Storage;

public abstract class AbstractKey(string[] indexes) : IKey
{
    public abstract KeyType GetKeyType();

    [JsonProperty("serializedKey")]
    [JsonPropertyName("serializedKey")]
    public string SerializedKey
    {
        get => string.Join("|", indexes);
        set => indexes = value.Split("|");
    }

    public string[] GetLookupIndex()
    {
        return indexes;
    }
}