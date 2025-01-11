using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model;

public class FireMode : ICloneable
{
    [JsonPropertyName("FireMode")] public string? FireModeType { get; set; }

    public object Clone() => new FireMode
    {
        FireModeType = FireModeType
    };
}