namespace LootDumpProcessor.Model.Input;

public class AdditionalHostilitySetting
{
    public string? BotRole { get; set; }
    public List<string>? AlwaysEnemies { get; set; }
    public List<ChancedEnemy>? ChancedEnemies { get; set; }
    public List<string>? Warn { get; set; }
    public List<string>? Neutral { get; set; }
    public List<string>? AlwaysFriends { get; set; }
    public string? SavagePlayerBehaviour { get; set; }
    public string? BearPlayerBehaviour { get; set; }
    public int? BearEnemyChance { get; set; }
    public string? UsecPlayerBehaviour { get; set; }
    public int? UsecEnemyChance { get; set; }
}