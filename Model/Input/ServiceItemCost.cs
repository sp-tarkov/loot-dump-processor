using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ServiceItemCost
    {
        [JsonProperty]
        public Dictionary<string, ItemCost>? Costs { get; set; }
    }
}