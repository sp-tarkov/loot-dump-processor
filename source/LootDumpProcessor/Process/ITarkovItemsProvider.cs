namespace LootDumpProcessor.Process;

public interface ITarkovItemsProvider
{
    bool IsBaseClass(string tpl, string baseclassId);
    bool IsQuestItem(string tpl);
    string? MaxDurability(string tpl);
    string? AmmoCaliber(string tpl);
}