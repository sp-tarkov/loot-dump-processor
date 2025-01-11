using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model.Tarkov;

public class Chamber
{
    [JsonPropertyName("_name")] public string Name { get; set; }


    [JsonPropertyName("_id")] public string Id { get; set; }


    [JsonPropertyName("_parent")] public string Parent { get; set; }


    [JsonPropertyName("_props")] public ChamberProps Props { get; set; }


    [JsonPropertyName("_required")] public bool Required { get; set; }


    [JsonPropertyName("_mergeSlotWithChildren")] public bool MergeSlotWithChildren { get; set; }


    [JsonPropertyName("_proto")] public string Proto { get; set; }
}