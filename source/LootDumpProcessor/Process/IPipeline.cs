namespace LootDumpProcessor.Process;

public interface IPipeline
{
    Task Execute();
}