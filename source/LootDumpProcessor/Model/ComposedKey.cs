using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model;

public class ComposedKey(string key, Item? firstItem)
{
    [JsonProperty("key")] [JsonPropertyName("key")] public string Key { get; init; } = key;

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public Item? FirstItem { get; } = firstItem;

    public override bool Equals(object? obj)
    {
        if (obj is not ComposedKey key)
            return false;
        return Key == key.Key;
    }

    public override int GetHashCode() => Key.GetHashCode();
}