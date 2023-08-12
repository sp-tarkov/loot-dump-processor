using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process;

public interface IFileProcessor
{
    PartialData Process(BasicInfo parsedData);
}