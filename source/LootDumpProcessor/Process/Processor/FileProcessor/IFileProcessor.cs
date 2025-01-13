using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.FileProcessor;

public interface IFileProcessor
{
    Task<PartialData> Process(BasicInfo parsedData);
}