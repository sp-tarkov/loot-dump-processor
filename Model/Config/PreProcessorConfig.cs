using System.Text.Json.Serialization;
using LootDumpProcessor.Process.Reader.PreProcess;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class PreProcessorConfig
{
    [JsonProperty("preProcessors")]
    [JsonPropertyName("preProcessors")]
    public List<PreProcessReaderTypes>? PreProcessors { get; set; }

    [JsonProperty("preProcessorTempFolder")]
    [JsonPropertyName("preProcessorTempFolder")]
    public string? PreProcessorTempFolder { get; set; }

    [JsonProperty("cleanupTempFolderAfterProcess")]
    [JsonPropertyName("cleanupTempFolderAfterProcess")]
    public bool CleanupTempFolderAfterProcess { get; set; } = true;
}