namespace LootDumpProcessor;

public class GCHandler
{
    public static void Collect()
    {
        if (LootDumpProcessorContext.GetConfig().ManualGarbageCollectionCalls)
        {
            GC.Collect();
        }
    }
}