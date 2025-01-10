namespace LootDumpProcessor.Process.Reader.PreProcess;

public static class PreProcessReaderFactory
{
    private static readonly Dictionary<PreProcessReaderTypes, IPreProcessReader> _proProcessReaders = new();

    public static IPreProcessReader GetInstance(PreProcessReaderTypes type)
    {
        if (!_proProcessReaders.TryGetValue(type, out var preProcessReader))
        {
            preProcessReader = type switch
            {
                PreProcessReaderTypes.SevenZip => new SevenZipPreProcessReader(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            _proProcessReaders.Add(type, preProcessReader);
        }

        return preProcessReader;
    }
}