namespace LootDumpProcessor.Storage.Implementations.File.Handlers;

public class SubdivisionedStoreHandler : AbstractStoreHandler
{
    protected override string GetLocation(IKey key)
    {
        var location = $"{GetBaseLocation()}/{string.Join("/", key.GetLookupIndex().SkipLast(1))}";

        if (!Directory.Exists(location)) Directory.CreateDirectory(location);

        return $"{location}/{key.GetLookupIndex().Last()}.json";
    }

    protected override string GetBaseLocation() => $"{base.GetBaseLocation()}/subdivisioned";
}