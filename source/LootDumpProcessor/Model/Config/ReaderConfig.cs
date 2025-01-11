using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class ReaderConfig
{
    [JsonProperty("intakeReaderConfig")]
    [JsonPropertyName("intakeReaderConfig")]
    public IntakeReaderConfig? IntakeReaderConfig { get; set; }

    [JsonProperty("preProcessorConfig")]
    [JsonPropertyName("preProcessorConfig")]
    public PreProcessorConfig? PreProcessorConfig { get; set; }

    [JsonProperty("dumpFilesLocation")]
    [JsonPropertyName("dumpFilesLocation")]
    public List<string>? DumpFilesLocation { get; set; }

    [JsonProperty("thresholdDate")]
    [JsonPropertyName("thresholdDate")]
    public string? ThresholdDate { get; set; }

    [JsonProperty("processSubFolders")]
    [JsonPropertyName("processSubFolders")]
    public bool ProcessSubFolders { get; set; }
}