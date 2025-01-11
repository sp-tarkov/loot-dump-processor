using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Output.LooseLoot;

public class LooseLootRoot
{
    [JsonPropertyName("spawnpointCount")] public SpawnPointCount? SpawnPointCount { get; set; }


    [JsonPropertyName("spawnpointsForced")] public List<SpawnPointsForced>? SpawnPointsForced { get; set; }


    [JsonPropertyName("spawnpoints")] public List<SpawnPoint>? SpawnPoints { get; set; }
}