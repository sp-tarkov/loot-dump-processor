using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model;

public class ComposedKey(string key, Item? firstItem)
{
    public string Key { get; init; } = key;

    [JsonIgnore] public Item? FirstItem { get; } = firstItem;

    public override bool Equals(object? obj)
    {
        if (obj is not ComposedKey key)
            return false;
        return Key == key.Key;
    }

    public override int GetHashCode() => Key.GetHashCode();
}