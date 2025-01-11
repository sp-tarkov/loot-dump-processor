namespace LootDumpProcessor.Process;

public interface IPipeline
{
    Task DoProcess();
}