using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Config;

public class ReaderConfig
{
    public IntakeReaderConfig? IntakeReaderConfig { get; set; }
    public List<string>? DumpFilesLocation { get; set; }
    public string? ThresholdDate { get; set; }
    public bool ProcessSubFolders { get; set; }
}