namespace LootDumpProcessor.Process.Writer;

public static class WriterFactory
{
    public static IWriter GetInstance() =>
        // implement actual factory someday
        new FileWriter();
}