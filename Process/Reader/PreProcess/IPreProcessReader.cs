namespace LootDumpProcessor.Process.Reader.PreProcess;

public interface IPreProcessReader
{
    string GetHandleExtension();

    bool TryPreProcess(string file, out List<string> files, out List<string> directories);

    // Custom dispose, not IDisposable
    void Dispose();
}