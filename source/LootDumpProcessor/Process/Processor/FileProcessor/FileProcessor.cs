using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor.FileProcessor;
using LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Storage;
using Microsoft.Extensions.Logging;

public class FileProcessor : IFileProcessor
{
    private readonly IStaticLootProcessor _staticLootProcessor;
    private readonly ILooseLootProcessor _looseLootProcessor;
    private readonly ILogger<FileProcessor> _logger;

    public FileProcessor(
        IStaticLootProcessor staticLootProcessor,
        ILooseLootProcessor looseLootProcessor,
        ILogger<FileProcessor> logger)
    {
        _staticLootProcessor = staticLootProcessor 
            ?? throw new ArgumentNullException(nameof(staticLootProcessor));
        _looseLootProcessor = looseLootProcessor 
            ?? throw new ArgumentNullException(nameof(looseLootProcessor));
        _logger = logger 
            ?? throw new ArgumentNullException(nameof(logger));
    }

    public PartialData Process(BasicInfo parsedData)
    {
        _logger.LogDebug("Processing file {FileName}...", parsedData.FileName);

        var looseLoot = new List<Template>();
        var staticLoot = new List<Template>();

        foreach (var item in parsedData.Data.Data.LocationLoot.Loot)
        {
            if (item.IsContainer ?? false) staticLoot.Add(item);
            else looseLoot.Add(item);
        }

        parsedData.Data = null;

        var dumpData = new ParsedDump
        {
            BasicInfo = parsedData
        };

        var data = new PartialData
        {
            BasicInfo = parsedData,
            ParsedDumpKey = (AbstractKey)dumpData.GetKey()
        };

        if (!DataStorageFactory.GetInstance().Exists(dumpData.GetKey()))
        {
            _logger.LogDebug(
                "Cache not found for {LookupIndex} processing.",
                string.Join("/", dumpData.GetKey().GetLookupIndex())
            );

            dumpData.Containers = _staticLootProcessor.PreProcessStaticLoot(staticLoot);
            dumpData.LooseLoot = _looseLootProcessor.PreProcessLooseLoot(looseLoot);
            DataStorageFactory.GetInstance().Store(dumpData);
        }

        _logger.LogDebug("File {FileName} finished processing!", parsedData.FileName);
        return data;
    }
}