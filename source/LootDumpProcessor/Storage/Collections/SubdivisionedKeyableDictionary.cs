using System.Text.Json.Serialization;
using LootDumpProcessor.Utils;

namespace LootDumpProcessor.Storage.Collections;

public class SubdivisionedKeyableDictionary<K, V> : Dictionary<K, V>, IKeyable
{
    [JsonPropertyName("__id__")] public string __ID { get; set; } = KeyGenerator.GetNextKey();

    public string? Extras
    {
        get => ExtraSubdivisions == null ? null : string.Join(",", ExtraSubdivisions);
        set => ExtraSubdivisions = value?.Split(",");
    }

    [JsonIgnore] private string[]? ExtraSubdivisions { get; set; }

    public IKey GetKey()
    {
        if (ExtraSubdivisions != null)
        {
            var subdivisions = new List<string>
            {
                "dictionaries"
            };
            subdivisions.AddRange(ExtraSubdivisions);
            subdivisions.Add(__ID);
            return new SubdivisionedUniqueKey(subdivisions.ToArray());
        }

        return new SubdivisionedUniqueKey(["dictionaries", __ID]);
    }
}