namespace LootDumpProcessor.Model.Input;

public readonly record struct Requirements(IReadOnlyList<CompletedQuest> CompletedQuests, object Standings);