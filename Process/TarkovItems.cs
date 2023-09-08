using LootDumpProcessor.Logger;
using LootDumpProcessor.Model.Tarkov;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process;

public class TarkovItems
{
    private static readonly IJsonSerializer _jsonSerializer = JsonSerializerFactory.GetInstance();

    private Dictionary<string, TemplateFileItem> _items;
    
    public TarkovItems(string items)
    {
        _items = _jsonSerializer.Deserialize<Dictionary<string, TemplateFileItem>>(File.ReadAllText(items));
    }

    public virtual bool IsBaseClass(string tpl, string baseclass_id)
    {
        if (!_items.TryGetValue(tpl, out var item_template))
        {
            LoggerFactory.GetInstance().Log($"[IsBaseClass] Item template '{tpl}' with base class id '{baseclass_id}' was not found on the server items!", LogLevel.Error);
            return false;
        }
        
        if (string.IsNullOrEmpty(item_template.Parent))
            return false;

        return item_template.Parent == baseclass_id || IsBaseClass(item_template.Parent, baseclass_id);
    }

    public virtual bool IsQuestItem(string tpl)
    {
        if (!_items.TryGetValue(tpl, out var item_template))
        {
            LoggerFactory.GetInstance().Log($"[IsQuestItem] Item template '{tpl}' was not found on the server items!", LogLevel.Error);
            return false;
        }
        return item_template.Props.QuestItem;
    }

    public virtual string? MaxDurability(string tpl)
    {
        if (!_items.TryGetValue(tpl, out var item_template))
        {
            LoggerFactory.GetInstance().Log($"[MaxDurability] Item template '{tpl}' was not found on the server items!", LogLevel.Error);
            return null;
        }
        return item_template.Props.MaxDurability?.ToString() ?? "";
    }

    public virtual string? AmmoCaliber(string tpl)
    {
        if (!_items.TryGetValue(tpl, out var item_template))
        {
            LoggerFactory.GetInstance().Log($"[AmmoCaliber] Item template '{tpl}' was not found on the server items!", LogLevel.Error);
            return null;
        }
        return item_template.Props.Caliber;
    }
}