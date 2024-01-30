namespace LootDumpProcessor.Process.Processor.FileProcessor;

public static class FileProcessorFactory
{
    public static IFileProcessor GetInstance()
    {
        // implement actual factory someday
        return new FileProcessor();
    }
}