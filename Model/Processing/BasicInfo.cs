using LootDumpProcessor.Model.Input;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Processing;

public class BasicInfo
{
    public string Map { get; set; }
    public string FileHash { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public RootData Data { get; set; }
    public DateTime Date { get; set; }
    public string FileName { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is BasicInfo info)
            return FileHash == info.FileHash;
        return false;
    }

    public override int GetHashCode()
    {
        return FileHash.GetHashCode();
    }
}