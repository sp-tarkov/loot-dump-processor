using System.Collections.Concurrent;
using LootDumpProcessor.Logger;
using LootDumpProcessor.Process.Collector;
using LootDumpProcessor.Process.Processor;
using LootDumpProcessor.Process.Processor.DumpProcessor;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Reader;
using LootDumpProcessor.Process.Reader.Filters;
using LootDumpProcessor.Process.Reader.Intake;
using LootDumpProcessor.Process.Reader.PreProcess;
using LootDumpProcessor.Process.Writer;
using LootDumpProcessor.Utils;

namespace LootDumpProcessor.Process;

public class QueuePipeline : IPipeline
{
    private static readonly BlockingCollection<string> _filesToProcess = new();
    private static readonly List<Task> Runners = new();

    private static readonly Dictionary<string, IPreProcessReader> _preProcessReaders;

    static QueuePipeline()
    {
        _preProcessReaders = LootDumpProcessorContext.GetConfig()
            .ReaderConfig
            .PreProcessorConfig
            ?.PreProcessors
            ?.ToDictionary(
                t => PreProcessReaderFactory.GetInstance(t).GetHandleExtension().ToLower(),
                PreProcessReaderFactory.GetInstance
            ) ?? new Dictionary<string, IPreProcessReader>();
    }

    public void DoProcess()
    {
        // Single collector instance to collect results
        var collector = CollectorFactory.GetInstance();
        collector.Setup();

        // We add 2 more threads to the total count to account for subprocesses and others
        int threads = LootDumpProcessorContext.GetConfig().Threads;
        ThreadPool.SetMaxThreads(threads + 2, threads + 2);
        if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Info))
            LoggerFactory.GetInstance().Log("Gathering files to begin processing", LogLevel.Info);
        try
        {
            // Gather all files, sort them by date descending and then add them into the processing queue
            GatherFiles().OrderByDescending(f =>
                {
                    FileDateParser.TryParseFileDate(f, out var date);
                    return date;
                }
            ).ToList().ForEach(f => _filesToProcess.Add(f));
            
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Info))
                LoggerFactory.GetInstance().Log("Files sorted and ready to begin pre-processing", LogLevel.Info);
            
            // We startup all the threads and collect them into a runners list
            for (int i = 0; i < threads; i++)
            {
                if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Info))
                    LoggerFactory.GetInstance().Log("Creating pre-processing threads", LogLevel.Info);
                Runners.Add(
                    Task.Factory.StartNew(
                        () =>
                        {
                            while (_filesToProcess.TryTake(out var file, TimeSpan.FromMilliseconds(5000)))
                            {
                                try
                                {
                                    var reader = IntakeReaderFactory.GetInstance();
                                    var processor = FileProcessorFactory.GetInstance();
                                    if (reader.Read(file, out var basicInfo))
                                        collector.Hold(processor.Process(basicInfo));
                                }
                                catch (Exception e)
                                {
                                    if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Error))
                                        LoggerFactory.GetInstance().Log(
                                            $"Error occurred while processing file {file}\n{e.Message}\n{e.StackTrace}",
                                            LogLevel.Error);
                                }
                            }
                        },
                        TaskCreationOptions.LongRunning)
                );
            }

            // Wait until all runners are done processing
            while (!Runners.All(r => r.IsCompleted))
            {
                if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Info))
                    LoggerFactory.GetInstance().Log(
                        $"One or more file processors are still processing files. Waiting {LootDumpProcessorContext.GetConfig().ThreadPoolingTimeoutMs}ms before checking again",
                        LogLevel.Info);
                Thread.Sleep(TimeSpan.FromMilliseconds(LootDumpProcessorContext.GetConfig().ThreadPoolingTimeoutMs));
            }
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Info))
                LoggerFactory.GetInstance().Log("Pre-processing finished", LogLevel.Info);
            // Single writer instance to collect results
            var writer = WriterFactory.GetInstance();
            // Single collector instance to collect results
            var dumpProcessor = DumpProcessorFactory.GetInstance();
            writer.WriteAll(dumpProcessor.ProcessDumps(collector.Retrieve()));
        }
        finally
        {
            // use dispose on the preprocessreaders to eliminate any temporary files generated
            foreach (var (_, value) in _preProcessReaders)
            {
                value.Dispose();
            }
        }
    }

    private List<string> GatherFiles()
    {
        // Read locations
        var inputPath = LootDumpProcessorContext.GetConfig().ReaderConfig.DumpFilesLocation;

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

        var fileFilters = GetFileFilters() ?? new Dictionary<string, IFileFilter>();

        while (queuedFilesToProcess.TryDequeue(out var file))
        {
            var extension = Path.GetExtension(file)[1..].ToLower();
            // if there is a preprocessor, call it and preprocess the file, then add them to the queue
            if (_preProcessReaders.TryGetValue(extension, out var preProcessor))
            {
                // if the preprocessor found something new to process or generated new files, add them to the queue
                if (preProcessor.TryPreProcess(file, out var outputFiles, out var outputDirectories))
                {
                    // all new directories need to be processed as well
                    GetFileQueue(outputDirectories).ToList()
                        .ForEach(queuedFilesToProcess.Enqueue);
                    // all output files need to be queued as well
                    outputFiles.ForEach(queuedFilesToProcess.Enqueue);
                }
            }
            else
            {
                // if there is no preprocessor for the file, means its ready to filter or accept
                if (fileFilters.TryGetValue(extension, out var filter))
                {
                    if (filter.Accept(file))
                        gatheredFiles.Add(file);
                }
                else
                {
                    gatheredFiles.Add(file);
                }
            }
        }

        return gatheredFiles;
    }

    private Queue<string> GetFileQueue(List<string> inputPath)
    {
        var queuedPathsToProcess = new Queue<string>();
        var queuedFilesToProcess = new Queue<string>();

        // Accepted file extensions on raw files
        var acceptedFileExtension = LootDumpProcessorContext.GetConfig()
            .ReaderConfig
            .AcceptedFileExtensions
            .Select(ex => ex.ToLower())
            .ToHashSet();
        inputPath.ForEach(p => queuedPathsToProcess.Enqueue(p));

        while (queuedPathsToProcess.TryDequeue(out var path))
        {
            // Check the input file to be sure its going to have data on it.
            if (!Directory.Exists(path))
                throw new Exception($"Input directory \"{inputPath}\" could not be found");
            // If we should process subfolder, queue them up as well
            if (LootDumpProcessorContext.GetConfig().ReaderConfig.ProcessSubFolders)
                foreach (var directory in Directory.GetDirectories(path))
                    queuedPathsToProcess.Enqueue(directory);

            var files = Directory.GetFiles(path);

            foreach (var file in files)
                if (acceptedFileExtension.Contains(Path.GetExtension(file)[1..].ToLower()))
                    queuedFilesToProcess.Enqueue(file);
        }

        return queuedFilesToProcess;
    }

    private Dictionary<string, IFileFilter>? GetFileFilters()
    {
        return LootDumpProcessorContext.GetConfig()
            .ReaderConfig
            .FileFilters
            ?.ToDictionary(
                t => FileFilterFactory.GetInstance(t).GetExtension(),
                FileFilterFactory.GetInstance
            );
    }
}