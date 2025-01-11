using LootDumpProcessor.Process.Reader.PreProcess;
using SevenZip;
using Microsoft.Extensions.Logging;

public class SevenZipPreProcessReader : AbstractPreProcessReader
{
    private readonly ILogger<SevenZipPreProcessReader> _logger;

    public SevenZipPreProcessReader(ILogger<SevenZipPreProcessReader> logger) : base(logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override string GetHandleExtension() => "7z";

    static SevenZipPreProcessReader()
    {
        SevenZipBase.SetLibraryPath("./x64/7z.dll");
    }

    public override bool TryPreProcess(string file, out List<string> files, out List<string> directories)
    {
        files = new List<string>();
        directories = new List<string>();

        var fileRaw = Path.GetFileNameWithoutExtension(file);
        // SevenZip library doesn't handle forward slashes properly
        var outPath = $"{_tempFolder}\\{fileRaw}".Replace("/", "\\");

        _logger.LogInformation("Unzipping {File} into temp path {OutPath}, this may take a while...", file, outPath);

        var extractor = new SevenZipExtractor(file);

        // Log progress in debug mode
        extractor.Extracting += (_, args) =>
        {
            if (args.PercentDone % 10 == 0)
            {
                _logger.LogDebug("Unzip progress: {PercentDone}%", args.PercentDone);
            }
        };

        extractor.ExtractArchive(outPath);
        _logger.LogInformation("Finished unzipping {File} into temp path {OutPath}", file, outPath);

        files = Directory.GetFiles(outPath).ToList();
        directories = Directory.GetDirectories(outPath).ToList();

        return true;
    }
}