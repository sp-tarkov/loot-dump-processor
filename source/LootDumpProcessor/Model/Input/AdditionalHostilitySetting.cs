namespace LootDumpProcessor.Model.Input;

public readonly record struct AdditionalHostilitySetting(
    string BotRole,
    IReadOnlyList<string> AlwaysEnemies,
    IReadOnlyList<ChancedEnemy> ChancedEnemies,
    IReadOnlyList<string> Warn,
    IReadOnlyList<string> Neutral,
    IReadOnlyList<string> AlwaysFriends,
    string SavagePlayerBehaviour,
    string BearPlayerBehaviour,
    int BearEnemyChance,
    string UsecPlayerBehaviour,
    int UsecEnemyChance
);