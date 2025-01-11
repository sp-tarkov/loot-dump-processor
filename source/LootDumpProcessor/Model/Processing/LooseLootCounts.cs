using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;


namespace LootDumpProcessor.Model.Processing;

public class LooseLootCounts : IKeyable
{
    [JsonPropertyName("__id__")] private string Id { get; set; }

    public IKey Counts { get; set; }
    public IKey ItemProperties { get; set; }
    public List<int> MapSpawnpointCount { get; set; } = new();

    public LooseLootCounts(string id, IKey counts, IKey itemProperties)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        Id = id;
        Counts = counts ?? throw new ArgumentNullException(nameof(counts));
        ItemProperties = itemProperties ?? throw new ArgumentNullException(nameof(itemProperties));
    }

    public IKey GetKey() => new FlatUniqueKey([Id]);
}