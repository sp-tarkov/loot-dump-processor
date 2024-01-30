namespace LootDumpProcessor.Process.Reader.Intake;

public static class IntakeReaderFactory
{
    public static IIntakeReader GetInstance()
    {
        return (LootDumpProcessorContext.GetConfig().ReaderConfig.IntakeReaderConfig?.IntakeReaderType ?? IntakeReaderTypes.Json) switch
        {
            IntakeReaderTypes.Json => new JsonFileIntakeReader(),
            _ => throw new ArgumentOutOfRangeException(
                "IntakeReaderType",
                "Value was not defined on IntakeReaderConfig"
            )
        };
    }
}