using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class TraderServices
    {
        [JsonProperty("ExUsecLoyalty", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ExUsecLoyalty")]
        public TraderService? ExUsecLoyalty { get; set; }

        [JsonProperty("ZryachiyAid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ZryachiyAid")]
        public TraderService? ZryachiyAid { get; set; }

        [JsonProperty("CultistsAid", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("CultistsAid")]
        public TraderService? CultistsAid { get; set; }

        [JsonProperty("PlayerTaxi", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PlayerTaxi")]
        public TraderService? PlayerTaxi { get; set; }

        [JsonProperty("BtrItemsDelivery", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BtrItemsDelivery")]
        public TraderService? BtrItemsDelivery { get; set; }

        [JsonProperty("BtrBotCover", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BtrBotCover")]
        public TraderService? BtrBotCover { get; set; }

        [JsonProperty("TransitItemsDelivery", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("TransitItemsDelivery")]
        public TraderService? TransitItemsDelivery { get; set; }
    }
}