using System.Text.Json.Serialization;

namespace LootDumpProcessor.Storage.Collections;

public class SubdivisionedKeyableDictionary<K, V> : Dictionary<K, V>, IKeyable
{
    public SubdivisionedKeyableDictionary(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        Id = id;
    }

    [JsonPropertyName("__id__")] private string Id { get; set; }

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
            subdivisions.Add(Id);
            return new SubdivisionedUniqueKey(subdivisions.ToArray());
        }

        return new SubdivisionedUniqueKey(["dictionaries", Id]);
    }
}