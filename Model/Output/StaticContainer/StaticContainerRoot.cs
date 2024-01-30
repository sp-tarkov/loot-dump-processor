using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output.StaticContainer
{
    public class StaticContainerRoot
    {
        [JsonProperty("Laboratory", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Laboratory")]
        public MapStaticLoot? Laboratory { get; set; }

        [JsonProperty("Shoreline", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Shoreline")]
        public MapStaticLoot? Shoreline { get; set; }

        [JsonProperty("Streets of Tarkov", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Streets of Tarkov")]
        public MapStaticLoot? StreetsofTarkov { get; set; }

        [JsonProperty("Interchange", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Interchange")]
        public MapStaticLoot? Interchange { get; set; }

        [JsonProperty("Customs", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Customs")]
        public MapStaticLoot? Customs { get; set; }

        [JsonProperty("Woods", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Woods")]
        public MapStaticLoot? Woods { get; set; }

        [JsonProperty("Factory", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Factory")]
        public MapStaticLoot? Factory { get; set; }

        [JsonProperty("ReserveBase", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ReserveBase")]
        public MapStaticLoot? ReserveBase { get; set; }

        [JsonProperty("Lighthouse", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Lighthouse")]
        public MapStaticLoot? Lighthouse { get; set; }

        [JsonProperty("Sandbox", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Sandbox")]
        public MapStaticLoot? GroundZero { get; set; }
    }
}