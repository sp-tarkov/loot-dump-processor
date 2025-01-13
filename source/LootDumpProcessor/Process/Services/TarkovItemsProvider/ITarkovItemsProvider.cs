namespace LootDumpProcessor.Process.Services.TarkovItemsProvider;

public interface ITarkovItemsProvider
{
    bool IsBaseClass(string tpl, string baseclassId);
    bool IsQuestItem(string tpl);
    string? MaxDurability(string tpl);
    string? AmmoCaliber(string tpl);
}