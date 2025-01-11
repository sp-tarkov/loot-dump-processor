namespace LootDumpProcessor.Model.Input;

public readonly record struct ServiceItemCost(IReadOnlyDictionary<string, ItemCost> Costs);