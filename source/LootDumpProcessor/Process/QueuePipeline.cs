using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using LootDumpProcessor.Model.Config;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Process.Collector;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Reader.Filters;
using LootDumpProcessor.Process.Reader.Intake;
using LootDumpProcessor.Process.Writer;
using LootDumpProcessor.Serializers.Json;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor.Process;

public class QueuePipeline(
    IFileProcessor fileProcessor, IDumpProcessor dumpProcessor, ILogger<QueuePipeline> logger, IFileFilter fileFilter,
    IIntakeReader intakeReader, ICollector collector, IDataStorage dataStorage, IOptions<Config> config, IWriter writer
)
    : IPipeline
{
    private readonly IFileProcessor _fileProcessor =
        fileProcessor ?? throw new ArgumentNullException(nameof(fileProcessor));

    private readonly IDumpProcessor _dumpProcessor =
        dumpProcessor ?? throw new ArgumentNullException(nameof(dumpProcessor));

    private readonly ILogger<QueuePipeline> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IFileFilter _fileFilter = fileFilter ?? throw new ArgumentNullException(nameof(fileFilter));

    private readonly IIntakeReader
        _intakeReader = intakeReader ?? throw new ArgumentNullException(nameof(intakeReader));

    private readonly ICollector _collector = collector ?? throw new ArgumentNullException(nameof(collector));

    private readonly IDataStorage _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

    private readonly IWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));

    private readonly Config _config = (config ?? throw new ArgumentNullException(nameof(config))).Value;

    private readonly List<string> _filesToRename = new();
    private readonly BlockingCollection<string> _filesToProcess = new();

    private IReadOnlyList<string> MapNames => _config.MapsToProcess;


    public async Task Execute()
    {
        var stopwatch = Stopwatch.StartNew();
        // Single collector instance to collect results
        _collector.Setup();

        _logger.LogInformation("Gathering files to begin processing");

        try
        {
            await FixFilesFromDumps();
            foreach (var mapName in MapNames) await ProcessFilesFromDumpsPerMap(_collector, mapName);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Dumps processed in {@Time}", stopwatch.Elapsed);
        }
    }

    private List<string> GatherFiles()
    {
        // Read locations
        var inputPath = _config.ReaderConfig.DumpFilesLocation;

        if (inputPath == null || inputPath.Count == 0)
            throw new Exception("Reader dumpFilesLocations must be set to a valid value");

        // We gather up all files into a queue
        var queuedFilesToProcess = GetFileQueue(inputPath);
        // Then we preprocess the files in the queue and get them ready for processing
        return PreProcessQueuedFiles(queuedFilesToProcess);
    }

    private List<string> PreProcessQueuedFiles(Queue<string> queuedFilesToProcess)
    {
        var gatheredFiles = new List<string>();

        if (queuedFilesToProcess.Count == 0)
            throw new Exception("No files matched accepted extension types in configs");

        while (queuedFilesToProcess.TryDequeue(out var file))
        {
            var extensionFull = Path.GetExtension(file);
            if (extensionFull.Length > 1)
            {
                if (_fileFilter.Accept(file)) gatheredFiles.Add(file);

                else gatheredFiles.Add(file);
            }
            else
            {
                // Handle invalid extension
                _logger.LogWarning("File '{File}' has an invalid extension.", file);
            }
        }

        return gatheredFiles;
    }

    private Queue<string> GetFileQueue(IReadOnlyList<string> inputPath)
    {
        var queuedPathsToProcess = new Queue<string>();
        var queuedFilesToProcess = new Queue<string>();

        foreach (var path in inputPath) queuedPathsToProcess.Enqueue(path);

        while (queuedPathsToProcess.TryDequeue(out var path))
        {
            // Check the input file to be sure its going to have data on it.
            if (!Directory.Exists(path)) throw new Exception($"Input directory \"{path}\" could not be found");

            // If we should process subfolder, queue them up as well
            if (_config.ReaderConfig.ProcessSubFolders)
                foreach (var directory in Directory.GetDirectories(path))
                    queuedPathsToProcess.Enqueue(directory);

            var files = Directory.GetFiles(path);

            foreach (var file in files) queuedFilesToProcess.Enqueue(file);
        }

        return queuedFilesToProcess;
    }

    private async Task ProcessFilesFromDumpsPerMap(ICollector collector, string mapName)
    {
        // Gather all files, sort them by date descending and then add them into the processing queue
        GatherFiles().FindAll(f => f.ToLower().Contains($"{mapName}--")).OrderByDescending(f =>
            {
                FileDateParser.TryParseFileDate(f, out var date);
                return date;
            }
        ).ToList().ForEach(f => _filesToProcess.Add(f));

        _logger.LogInformation("Files sorted and ready to begin pre-processing");

        await Parallel.ForEachAsync(_filesToProcess, async (file, _) =>
        {
            try
            {
                if (!_intakeReader.Read(file, out var basicInfo)) return;

                var partialData = await _fileProcessor.Process(basicInfo);
                collector.Hold(partialData);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while processing file {File}", file);
            }
        });

        _logger.LogInformation("Pre-processing finished");

        // Single writer instance to collect results
        // Single collector instance to collect results
        var partialData = collector.Retrieve();
        var processedDumps = await _dumpProcessor.ProcessDumps(partialData);
        _writer.WriteAll(processedDumps);

        // clear collector and datastorage as we process per map now
        collector.Clear();
        _dataStorage.Clear();
    }

    /// <summary>
    /// Adds map name to file if they don't have it already.
    /// </summary>
    private async Task FixFilesFromDumps()
    {
        var inputPath = _config.ReaderConfig.DumpFilesLocation;

        if (inputPath == null || inputPath.Count == 0)
            throw new Exception("Reader dumpFilesLocations must be set to a valid value");

        GetFileQueue(inputPath).ToList().ForEach(f => _filesToRename.Add(f));

        await Parallel.ForEachAsync(_filesToRename, async (file, cancellationToken) =>
        {
            if (MapNames.Any(file.Contains)) return;

            try
            {
                var data = await File.ReadAllTextAsync(file, cancellationToken);
                var fileData = JsonSerializer.Deserialize<RootData>(data, JsonSerializerSettings.Default);
                var newPath = file.Replace("resp", $"{fileData.Data.LocationLoot.Id.ToLower()}--resp");
                File.Move(file, newPath);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while processing file {File}", file);
            }
        });

        _logger.LogInformation("File-processing finished");
    }
}