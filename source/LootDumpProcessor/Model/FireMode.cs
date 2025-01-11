using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model;

public readonly record struct FireMode([property: JsonPropertyName("FireMode")] string FireModeType);