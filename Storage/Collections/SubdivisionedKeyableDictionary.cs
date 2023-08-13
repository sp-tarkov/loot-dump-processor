﻿using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Storage.Collections;

public class SubdivisionedKeyableDictionary<K, V> : Dictionary<K, V>, IKeyable
{
    [JsonProperty("__id__")]
    [JsonPropertyName("__id__")]
    public string __ID { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("extras")]
    [JsonPropertyName("extras")]
    public string? Extras
    {
        get
        {
            if (ExtraSubdivisions == null)
                return null;
            return string.Join(",", ExtraSubdivisions);
        }
        set { ExtraSubdivisions = value.Split(","); }
    }

    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    private string[]? ExtraSubdivisions { get; set; }

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
        else
        {
            return new SubdivisionedUniqueKey(new[] { "dictionaries", __ID });
        }
    }

    public void AddExtraSubdivisions(string[] extras)
    {
        ExtraSubdivisions = extras;
    }
}