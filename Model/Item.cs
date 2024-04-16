using System.Text.Json.Serialization;
using LootDumpProcessor.Utils;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Item : ICloneable
    {
        [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("_id")]
        public string? Id { get; set; }

        [JsonProperty("_tpl", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("_tpl")]
        public string? Tpl { get; set; }

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("parentId")]
        public string? ParentId { get; set; }

        [JsonProperty("slotId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("slotId")]
        public string? SlotId { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("location")]
        public object? Location { get; set; }

        [JsonProperty("upd", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("upd")]
        public Upd? Upd { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not Item parsed)
                return false;
            return parsed.Tpl == Tpl && parsed.ParentId == ParentId;
        }

        public override int GetHashCode()
        {
            return (Tpl?.GetHashCode() + ParentId?.GetHashCode()) ?? base.GetHashCode();
        }

        public object Clone()
        {
            return new Item
            {
                Id = Id,
                Tpl = Tpl,
                ParentId = ParentId,
                SlotId = SlotId,
                Location = Location,
                Upd = ProcessorUtil.Copy(Upd)
            };
        }
    }
}