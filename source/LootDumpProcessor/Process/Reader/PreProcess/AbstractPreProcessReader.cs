using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Reader.PreProcess;

public abstract class AbstractPreProcessReader : IPreProcessReader
{
    protected readonly string _tempFolder;

    public AbstractPreProcessReader(ILogger logger)
    {
        var tempFolder = LootDumpProcessorContext.GetConfig().ReaderConfig.PreProcessorConfig?.PreProcessorTempFolder;
        if (string.IsNullOrEmpty(tempFolder))
        {
            tempFolder = GetBaseDirectory();
            logger.LogWarning(
                "No temp folder was assigned preProcessorTempFolder in PreProcessorConfig, defaulting to {tempFolder}",
                tempFolder
            );
        }

        // Cleanup the temp directory before starting the process
        if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);

        Directory.CreateDirectory(tempFolder);

        _tempFolder = tempFolder;
    }

    public abstract string GetHandleExtension();
    public abstract bool TryPreProcess(string file, out List<string> files, out List<string> directories);

    protected string GetBaseDirectory() =>
        $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\SPT\tmp\PreProcessor";

    public void Dispose()
    {
        if (LootDumpProcessorContext.GetConfig().ReaderConfig.PreProcessorConfig?.CleanupTempFolderAfterProcess ?? true)
            Directory.Delete(_tempFolder, true);
    }
}