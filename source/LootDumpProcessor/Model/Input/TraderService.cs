namespace LootDumpProcessor.Model.Input;

public readonly record struct TraderService(
    string TraderId, string TraderServiceType, Requirements Requirements, object ServiceItemCost,
    IReadOnlyList<object> UniqueItems
);