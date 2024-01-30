using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Collector;

public interface ICollector
{
    void Setup();
    void Hold(PartialData parsedDump);

    List<PartialData> Retrieve();
}