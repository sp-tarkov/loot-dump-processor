namespace LootDumpProcessor.Model.Input;

public readonly record struct NonWaveGroupScenario(int MinToBeGroup, int MaxToBeGroup, float Chance, bool Enabled);