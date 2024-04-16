using System.Text.Json.Serialization;
using LootDumpProcessor.Process.Processor;
using LootDumpProcessor.Utils;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Upd : ICloneable
    {
        [JsonProperty("StackObjectsCount", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("StackObjectsCount")]
        public int? StackObjectsCount { get; set; }

        [JsonProperty("FireMode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("FireMode")]
        public FireMode? FireMode { get; set; }

        [JsonProperty("Foldable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Foldable")]
        public Foldable? Foldable { get; set; }

        [JsonProperty("Repairable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Repairable")]
        public Repairable? Repairable { get;set; }

        public object Clone()
        {
            return new Upd
            {
                StackObjectsCount = StackObjectsCount,
                FireMode = ProcessorUtil.Copy(FireMode),
                Foldable = ProcessorUtil.Copy(Foldable),
                Repairable = ProcessorUtil.Copy(Repairable)
            };
        }
    }
}