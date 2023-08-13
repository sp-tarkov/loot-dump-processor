using LootDumpProcessor.Model.Tarkov;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process;

public class TarkovItems
{
    private static readonly IJsonSerializer _jsonSerializer = JsonSerializerFactory.GetInstance();

    private Dictionary<string, TemplateFileItem> _items;
    private HandbookRoot _handbook;

    public TarkovItems(string items, string handbook)
    {
        _items = _jsonSerializer.Deserialize<Dictionary<string, TemplateFileItem>>(File.ReadAllText(items));
        _handbook = _jsonSerializer.Deserialize<HandbookRoot>(File.ReadAllText(handbook));
    }

    public virtual bool IsBaseClass(string tpl, string baseclass_id)
    {
        var item_template = _items[tpl];
        if (string.IsNullOrEmpty(item_template.Parent))
            return false;

        return item_template.Parent == baseclass_id || IsBaseClass(item_template.Parent, baseclass_id);
    }

    public virtual bool IsQuestItem(string tpl)
    {
        var item_template = _items[tpl];
        return item_template.Props.QuestItem;
    }

    public virtual string? MaxDurability(string tpl)
    {
        var item_template = _items[tpl];
        return item_template.Props.MaxDurability?.ToString() ?? "";
    }

    public virtual string? AmmoCaliber(string tpl)
    {
        var item_template = _items[tpl];
        return item_template.Props.Caliber;
    }
}