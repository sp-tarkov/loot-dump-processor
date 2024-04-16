using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output.StaticContainer
{
    public class MapStaticLoot
    {
        [JsonProperty("staticWeapons", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("staticWeapons")]
        public List<Template>? StaticWeapons { get; set; }

        [JsonProperty("staticContainers", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("staticContainers")]
        public List<StaticDataPoint>? StaticContainers { get; set; }

        [JsonProperty("staticForced", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("staticForced")]
        public List<StaticForced>? StaticForced { get; set; }
    }
}