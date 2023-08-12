using System.Text.Json.Serialization;
using LootDumpProcessor.Model.Processing;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model;

public class ComposedKey
{
    [JsonProperty("key")]
    [JsonPropertyName("key")]
    public string Key { get; init; }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public Item? FirstItem { get; }

    public ComposedKey(Template template) : this(template.Items)
    {
    }
    
    public ComposedKey(List<Item>? items)
    {
        Key = items?.Select(i => i.Tpl)
            .Where(i => !string.IsNullOrEmpty(i) &&
                        !LootDumpProcessorContext.GetTarkovItems().IsBaseClass(i, BaseClasses.Ammo))
            .Cast<string>()
            .Select(i => (double)i.GetHashCode())
            .Sum()
            .ToString() ?? Guid.NewGuid().ToString();
        FirstItem = items?[0];
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ComposedKey key)
            return false;
        return this.Key == key.Key;
    }

    public override int GetHashCode() => Key.GetHashCode();
}