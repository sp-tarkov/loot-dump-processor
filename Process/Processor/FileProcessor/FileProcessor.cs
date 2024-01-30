using LootDumpProcessor.Logger;
using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Storage;

namespace LootDumpProcessor.Process.Processor.FileProcessor;

public class FileProcessor : IFileProcessor
{
    public PartialData Process(BasicInfo parsedData)
    {
        LoggerFactory.GetInstance().Log($"Processing file {parsedData.FileName}...", LogLevel.Info);
        List<Template> looseLoot = new List<Template>();
        List<Template> staticLoot = new List<Template>();

        foreach (var item in parsedData.Data.Data.Loot)
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

        PartialData data = new PartialData
        {
            BasicInfo = parsedData,
            ParsedDumpKey = (AbstractKey)dumpData.GetKey()
        };

        if (!DataStorageFactory.GetInstance().Exists(dumpData.GetKey()))
        {
            LoggerFactory.GetInstance().Log(
                $"Cached not found for {string.Join("/", dumpData.GetKey().GetLookupIndex())} processing.",
                LogLevel.Info
            );
            dumpData.Containers = StaticLootProcessor.PreProcessStaticLoot(staticLoot);
            dumpData.LooseLoot = LooseLootProcessor.PreProcessLooseLoot(looseLoot);
            DataStorageFactory.GetInstance().Store(dumpData);
        }

        LoggerFactory.GetInstance().Log($"File {parsedData.FileName} finished processing!", LogLevel.Info);
        return data;
    }
}