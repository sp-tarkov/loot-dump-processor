using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class ProcessorConfig
{
    [JsonProperty("spawnPointToleranceForForced")]
    [JsonPropertyName("spawnPointToleranceForForced")]
    public double SpawnPointToleranceForForced { get; set; } = 99D;
}