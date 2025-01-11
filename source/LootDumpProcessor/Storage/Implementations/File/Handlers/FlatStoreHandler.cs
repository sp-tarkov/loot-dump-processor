namespace LootDumpProcessor.Storage.Implementations.File.Handlers;

public class FlatStoreHandler : AbstractStoreHandler
{
    protected override string GetLocation(IKey key)
    {
        var baseLocation = GetBaseLocation();
        if (!Directory.Exists(baseLocation)) Directory.CreateDirectory(baseLocation);

        return $"{baseLocation}/{string.Join("-", key.GetLookupIndex())}.json";
    }

    protected override string GetBaseLocation() => $"{base.GetBaseLocation()}/flat";
}