using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model;

public readonly record struct Upd(
    [property: JsonPropertyName("StackObjectsCount")] object? StackObjectsCount,
    [property: JsonPropertyName("FireMode")] FireMode? FireMode,
    [property: JsonPropertyName("Foldable")] Foldable? Foldable,
    [property: JsonPropertyName("Repairable")] Repairable? Repairable
);