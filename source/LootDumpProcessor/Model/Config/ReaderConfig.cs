using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Config;

public class ReaderConfig
{
    [JsonPropertyName("intakeReaderConfig")] public IntakeReaderConfig? IntakeReaderConfig { get; set; }


    [JsonPropertyName("preProcessorConfig")] public PreProcessorConfig? PreProcessorConfig { get; set; }


    [JsonPropertyName("dumpFilesLocation")] public List<string>? DumpFilesLocation { get; set; }


    [JsonPropertyName("thresholdDate")] public string? ThresholdDate { get; set; }


    [JsonPropertyName("processSubFolders")] public bool ProcessSubFolders { get; set; }
}