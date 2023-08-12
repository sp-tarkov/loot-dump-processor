using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Collector;

public class HashSetCollector : ICollector
{
    private HashSet<PartialData> processedDumps = new HashSet<PartialData>();

    private object lockObject = new object();


    public void Setup()
    {
    }

    public void Hold(PartialData outputData)
    {
        lock (lockObject)
        {
            processedDumps.Add(outputData);
        }
    }

    public List<PartialData> Retrieve()
    {
        return processedDumps.ToList();
    }
}