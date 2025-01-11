using System.Text.Json.Serialization;
using LootDumpProcessor.Serializers.Json.Converters;
using LootDumpProcessor.Storage;

namespace LootDumpProcessor.Model.Processing;

public class PreProcessedLooseLoot
{
    public Dictionary<string, int> Counts { get; set; }

    [JsonConverter(typeof(NetJsonKeyConverter))] public IKey ItemProperties { get; set; }

    public int MapSpawnpointCount { get; set; }
}