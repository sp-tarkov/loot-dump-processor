namespace LootDumpProcessor.Process.Collector;

public static class CollectorFactory
{
    public static ICollector GetInstance()
    {
        // TODO: implement real factory
        return new HashSetCollector();
    }
}