using LootDumpProcessor.Process.Impl;

namespace LootDumpProcessor.Process.Processor;

public static class FileProcessorFactory
{
    public static IFileProcessor GetInstance()
    {
        // implement actual factory someday
        return new FileProcessor();
    }
}