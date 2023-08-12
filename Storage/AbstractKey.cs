using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Storage;

public abstract class AbstractKey : IKey
{
    public abstract KeyType GetKeyType();

    private string[] indexes;

    [JsonProperty("serializedKey")]
    [JsonPropertyName("serializedKey")]
    public string SerializedKey
    {
        get { return string.Join("|", this.indexes); }
        set { indexes = value.Split("|"); }
    }

    public AbstractKey(string[] indexes)
    {
        this.indexes = indexes;
    }

    public string[] GetLookupIndex()
    {
        return this.indexes;
    }
}