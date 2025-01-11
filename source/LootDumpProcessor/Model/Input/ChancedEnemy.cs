namespace LootDumpProcessor.Model.Input;

public readonly record struct ChancedEnemy(
    int EnemyChance,
    string Role
);