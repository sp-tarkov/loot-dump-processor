using System.Text.Json.Serialization;
using LootDumpProcessor.Serializers.Json.Converters;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class DumpProcessorConfig
{
    [JsonProperty("spawnContainerChanceIncludeAfterDate")]
    [JsonPropertyName("spawnContainerChanceIncludeAfterDate")]
    [Newtonsoft.Json.JsonConverter(typeof(NewtonsoftDateTimeConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(NetDateTimeConverter))]
    public DateTime SpawnContainerChanceIncludeAfterDate { get; set; }
}