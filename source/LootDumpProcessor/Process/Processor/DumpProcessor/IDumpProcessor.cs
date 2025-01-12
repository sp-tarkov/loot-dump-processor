using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.DumpProcessor;

public interface IDumpProcessor
{
    Task<Dictionary<OutputFileType, object>> ProcessDumps(List<PartialData> dumps);
}