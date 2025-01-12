using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model;

public class GroupPosition : ICloneable
{
    [JsonPropertyName("Name")] public string Name { get; set; }
    [JsonPropertyName("Weight")] public int Weight { get; set; }
    [JsonPropertyName("Position")] public Vector3 Position { get; set; }
    [JsonPropertyName("Rotation")] public Vector3 Rotation { get; set; }

    public object Clone() => new GroupPosition
    {
        Name = Name,
        Weight = Weight,
        Position = Position,
        Rotation = Rotation
    };
}