using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.StaticLootProcessor;
using LootDumpProcessor.Storage;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process.Processor.FileProcessor;

public class FileProcessor(
    IStaticLootProcessor staticLootProcessor,
    ILooseLootProcessor looseLootProcessor,
    ILogger<FileProcessor> logger, IDataStorage dataStorage
)
    : IFileProcessor
{
    private readonly IStaticLootProcessor _staticLootProcessor = staticLootProcessor
                                                                 ?? throw new ArgumentNullException(
                                                                     nameof(staticLootProcessor));

    private readonly ILooseLootProcessor _looseLootProcessor = looseLootProcessor
                                                               ?? throw new ArgumentNullException(
                                                                   nameof(looseLootProcessor));

    private readonly ILogger<FileProcessor> _logger = logger
                                                      ?? throw new ArgumentNullException(nameof(logger));

    private readonly IDataStorage _dataStorage = dataStorage ?? throw new ArgumentNullException(nameof(dataStorage));

    public async Task<PartialData> Process(BasicInfo parsedData)
    {
        _logger.LogDebug("Processing file {FileName}...", parsedData.FileName);

        var looseLoot = new List<Template>();
        var staticLoot = new List<Template>();

        foreach (var item in parsedData.Data.Data.LocationLoot.Loot)
            if (item.IsContainer) staticLoot.Add(item);
            else looseLoot.Add(item);

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

        if (!_dataStorage.Exists(dumpData.GetKey()))
        {
            _logger.LogDebug(
                "Cache not found for {LookupIndex} processing.",
                string.Join("/", dumpData.GetKey().GetLookupIndex())
            );

            dumpData.Containers = await _staticLootProcessor.PreProcessStaticLoot(staticLoot);
            dumpData.LooseLoot = _looseLootProcessor.PreProcessLooseLoot(looseLoot);
            dataStorage.Store(dumpData);
        }

        _logger.LogDebug("File {FileName} finished processing!", parsedData.FileName);
        return data;
    }
}