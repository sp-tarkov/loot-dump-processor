using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Storage.Collections;

public class FlatKeyableDictionary<K, V> : Dictionary<K, V>, IKeyable
{
    [JsonProperty("__id__")]
    [JsonPropertyName("__id__")]
    public string __ID { get; set; } = Guid.NewGuid().ToString();

    public IKey GetKey()
    {
        return new FlatUniqueKey(new[] { __ID });
    }
}