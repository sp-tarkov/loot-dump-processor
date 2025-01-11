namespace LootDumpProcessor.Model;

public readonly record struct Upd(
    object? StackObjectsCount, FireMode FireMode, Foldable Foldable, Repairable Repairable
);