using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Config;

public class PreProcessorConfig
{
    [JsonPropertyName("preProcessorTempFolder")] public string? PreProcessorTempFolder { get; set; }


    [JsonPropertyName("cleanupTempFolderAfterProcess")] public bool CleanupTempFolderAfterProcess { get; set; } = true;
}