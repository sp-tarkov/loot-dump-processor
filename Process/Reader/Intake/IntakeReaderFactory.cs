using LootDumpProcessor.Process.Impl;

namespace LootDumpProcessor.Process.Reader;

public static class IntakeReaderFactory
{
    public static IIntakeReader GetInstance()
    {
        return LootDumpProcessorContext.GetConfig().ReaderConfig.IntakeReaderConfig.IntakeReaderType switch
        {
            IntakeReaderTypes.Json => new JsonFileIntakeReader(),
            _ => throw new ArgumentOutOfRangeException(
                "IntakeReaderType",
                "Value was not defined on IntakeReaderConfig"
            )
        };
    }
}