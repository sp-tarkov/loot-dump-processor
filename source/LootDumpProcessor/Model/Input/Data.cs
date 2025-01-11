namespace LootDumpProcessor.Model.Input;

public readonly record struct Data(
    string ServerId,
    ServerSettings ServerSettings,
    object Profile,
    LocationLoot LocationLoot
);