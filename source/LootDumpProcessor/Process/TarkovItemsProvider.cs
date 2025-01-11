using System.Collections.Frozen;
using System.Text.Json;
using LootDumpProcessor.Model.Tarkov;
using Microsoft.Extensions.Logging;

namespace LootDumpProcessor.Process;

public class TarkovItemsProvider : ITarkovItemsProvider
{
    private readonly ILogger<TarkovItemsProvider> _logger;
    private readonly FrozenDictionary<string, TemplateFileItem>? _items;

    private static readonly string ItemsFilePath = Path.Combine(
        LootDumpProcessorContext.GetConfig().ServerLocation,
        "project", "assets", "database", "templates", "items.json");

    public TarkovItemsProvider(ILogger<TarkovItemsProvider> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        try
        {
            var jsonContent = File.ReadAllText(ItemsFilePath);
            _items = (JsonSerializer.Deserialize<Dictionary<string, TemplateFileItem>>(jsonContent)
                      ?? throw new InvalidOperationException()).ToFrozenDictionary();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load server items from {ItemsPath}", ItemsFilePath);
            throw new InvalidOperationException(
                "The server items couldn't be found or loaded. Check server config is pointing to the correct place.",
                ex);
        }
    }

    public bool IsBaseClass(string tpl, string baseclassId)
    {
        if (_items == null)
        {
            _logger.LogError("The server items are null. Check server config is pointing to the correct place.");
            throw new InvalidOperationException(
                "The server items couldn't be found or loaded. Check server config is pointing to the correct place.");
        }

        if (!_items.TryGetValue(tpl, out var itemTemplate))
        {
            _logger.LogError(
                "Item template '{Tpl}' with base class id '{BaseclassId}' was not found in the server items!", tpl,
                baseclassId);
            return false;
        }

        if (string.IsNullOrEmpty(itemTemplate.Parent))
            return false;

        return itemTemplate.Parent == baseclassId || IsBaseClass(itemTemplate.Parent, baseclassId);
    }

    public bool IsQuestItem(string tpl)
    {
        if (_items == null)
        {
            _logger.LogError("The server items are null. Check server config is pointing to the correct place.");
            throw new InvalidOperationException(
                "The server items couldn't be found or loaded. Check server config is pointing to the correct place.");
        }

        if (!_items.TryGetValue(tpl, out var itemTemplate))
        {
            _logger.LogError("Item template '{Tpl}' was not found in the server items!", tpl);
            return false;
        }

        return itemTemplate.Props.QuestItem;
    }

    public string? MaxDurability(string tpl)
    {
        if (_items == null)
        {
            _logger.LogError("The server items are null. Check server config is pointing to the correct place.");
            throw new InvalidOperationException(
                "The server items couldn't be found or loaded. Check server config is pointing to the correct place.");
        }

        if (!_items.TryGetValue(tpl, out var itemTemplate))
        {
            _logger.LogError("Item template '{Tpl}' was not found in the server items!", tpl);
            return null;
        }

        return itemTemplate.Props.MaxDurability?.ToString() ?? string.Empty;
    }

    public string? AmmoCaliber(string tpl)
    {
        if (_items == null)
        {
            _logger.LogError("The server items are null. Check server config is pointing to the correct place.");
            throw new InvalidOperationException(
                "The server items couldn't be found or loaded. Check server config is pointing to the correct place.");
        }

        if (!_items.TryGetValue(tpl, out var itemTemplate))
        {
            _logger.LogError("Item template '{Tpl}' was not found in the server items!", tpl);
            return null;
        }

        return itemTemplate.Props.Caliber;
    }
}