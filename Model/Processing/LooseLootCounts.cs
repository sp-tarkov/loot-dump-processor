using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Processing;

public class LooseLootCounts : IKeyable
{
    [JsonProperty("__id__")]
    [JsonPropertyName("__id__")]
    public string __ID { get; set; } = Guid.NewGuid().ToString();

    public IKey Counts { get; set; }

    // public IKey Items { get; set; }
    public IKey ItemProperties { get; set; }
    public List<int> MapSpawnpointCount { get; set; } = new();

    public IKey GetKey()
    {
        return new FlatUniqueKey(new[] { __ID });
    }
}