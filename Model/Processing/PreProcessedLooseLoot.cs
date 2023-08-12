using LootDumpProcessor.Storage;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Processing;

public class PreProcessedLooseLoot : IKeyable
{
    public Dictionary<string, int> Counts { get; set; }

    [JsonConverter(typeof(NewtonsoftJsonKeyConverter))]
    public IKey ItemProperties { get; set; }

    public int MapSpawnpointCount { get; set; }

    public IKey GetKey()
    {
        throw new NotImplementedException();
    }
}