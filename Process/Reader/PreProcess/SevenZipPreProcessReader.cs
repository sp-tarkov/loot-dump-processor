using LootDumpProcessor.Logger;
using SevenZip;
using SevenZip.Sdk.Compression.Lzma;

namespace LootDumpProcessor.Process.Reader.PreProcess;

public class SevenZipPreProcessReader : AbstractPreProcessReader
{
    public override string GetHandleExtension() => "7z";

    static SevenZipPreProcessReader()
    {
        SevenZipBase.SetLibraryPath("./x64/7z.dll");
    }

    public override bool TryPreProcess(string file, out List<string> files, out List<string> directories)
    {
        var fileRaw = Path.GetFileNameWithoutExtension(file);
        // SevenZip library doesnt like forward slashes for some reason
        var outPath = $"{_tempFolder}\\{fileRaw}".Replace("/", "\\");
        LoggerFactory.GetInstance().Log(
            $"Unzipping {file} into temp path {outPath}, this may take a while...",
            LogLevel.Info);
        var extractor = new SevenZipExtractor(file);
        extractor.Extracting += (_, args) =>
        {
            if (args.PercentDone % 10 == 0)
                LoggerFactory.GetInstance().Log($"Unzip progress: {args.PercentDone}%", LogLevel.Info);
        };
        extractor.ExtractArchive(outPath);
        LoggerFactory.GetInstance().Log($"Finished unzipping {file} into temp path {outPath}", LogLevel.Info);

        files = Directory.GetFiles(outPath).ToList();
        directories = Directory.GetDirectories(outPath).ToList();
        return true;
    }
}