using System.Collections.Concurrent;
using System.Text.Json.Serialization;

namespace LootDumpProcessor.Storage.Collections;

public class FlatKeyableDictionary<K, V> : ConcurrentDictionary<K, V>, IKeyable
{
    [JsonPropertyName("__id__")] private string Id { get; set; }

    public FlatKeyableDictionary(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        Id = id;
    }

    public IKey GetKey() => new FlatUniqueKey([Id]);
}