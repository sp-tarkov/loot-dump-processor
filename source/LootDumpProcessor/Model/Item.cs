using System.Text.Json.Serialization;
using LootDumpProcessor.Utils;


namespace LootDumpProcessor.Model;

public class Item : ICloneable
{
    [JsonPropertyName("_id")] public string? Id { get; set; }


    [JsonPropertyName("_tpl")] public string? Tpl { get; set; }


    public Upd? Upd { get; set; }


    public string? ParentId { get; set; }


    public string? SlotId { get; set; }


    public object? Location { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Item parsed)
            return false;
        return parsed.Tpl == Tpl && parsed.ParentId == ParentId;
    }

    public override int GetHashCode() => Tpl?.GetHashCode() + ParentId?.GetHashCode() ?? base.GetHashCode();

    public object Clone() => new Item
    {
        Id = Id,
        Tpl = Tpl,
        ParentId = ParentId,
        SlotId = SlotId,
        Location = Location,
        Upd = ProcessorUtil.Copy(Upd)
    };
}