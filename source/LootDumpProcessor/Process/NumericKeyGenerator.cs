namespace LootDumpProcessor.Process;

public class NumericKeyGenerator : IKeyGenerator
{
    private ulong _currentKey;

    public string Generate()
    {
        var key = _currentKey;
        Interlocked.Increment(ref _currentKey);
        return key.ToString();
    }
}