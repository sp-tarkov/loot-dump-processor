using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model.Tarkov;

public class Slot
{
    [JsonPropertyName("_name")] public string Name { get; set; }


    [JsonPropertyName("_required")] public bool Required { get; set; }
}