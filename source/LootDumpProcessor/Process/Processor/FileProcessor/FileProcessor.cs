using LootDumpProcessor.Logger;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Process.Processor.v2.LooseLootProcessor;
using LootDumpProcessor.Process.Processor.v2.StaticLootProcessor;
using LootDumpProcessor.Storage;

namespace LootDumpProcessor.Process.Processor.FileProcessor;

public class FileProcessor(IStaticLootProcessor staticLootProcessor, ILooseLootProcessor looseLootProcessor)
    : IFileProcessor
{
    private readonly IStaticLootProcessor _staticLootProcessor =
        staticLootProcessor ?? throw new ArgumentNullException(nameof(staticLootProcessor));

    private readonly ILooseLootProcessor _looseLootProcessor =
        looseLootProcessor ?? throw new ArgumentNullException(nameof(looseLootProcessor));

    public PartialData Process(BasicInfo parsedData)
    {
        if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Debug))
            LoggerFactory.GetInstance().Log($"Processing file {parsedData.FileName}...", LogLevel.Debug);

        List<Template> looseLoot = new List<Template>();
        List<Template> staticLoot = new List<Template>();

        foreach (var item in parsedData.Data.Data.LocationLoot.Loot)
        {
            if (item.IsContainer ?? false)
                staticLoot.Add(item);
            else
                looseLoot.Add(item);
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
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Debug))
                LoggerFactory.GetInstance().Log(
                    $"Cached not found for {string.Join("/", dumpData.GetKey().GetLookupIndex())} processing.",
                    LogLevel.Debug
                );
            dumpData.Containers = _staticLootProcessor.PreProcessStaticLoot(staticLoot);
            dumpData.LooseLoot = _looseLootProcessor.PreProcessLooseLoot(looseLoot);
            DataStorageFactory.GetInstance().Store(dumpData);
        }

        if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Debug))
            LoggerFactory.GetInstance().Log($"File {parsedData.FileName} finished processing!", LogLevel.Debug);
        return data;
    }
}