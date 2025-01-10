using System.Text.Json.Serialization;
using LootDumpProcessor.Process.Reader.Filters;
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

    [JsonProperty("acceptedFileExtensions")]
    [JsonPropertyName("acceptedFileExtensions")]
    public List<string> AcceptedFileExtensions { get; set; } = new();

    [JsonProperty("processSubFolders")]
    [JsonPropertyName("processSubFolders")]
    public bool ProcessSubFolders { get; set; }
    
    [JsonProperty("fileFilters")]
    [JsonPropertyName("fileFilters")]
    public List<FileFilterTypes>? FileFilters { get; set; }
}