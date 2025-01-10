using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class AdditionalHostilitySetting
    {
        [JsonProperty("BotRole", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotRole")]
        public string? BotRole { get; set; }

        [JsonProperty("AlwaysEnemies", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AlwaysEnemies")]
        public List<string>? AlwaysEnemies { get; set; }

        [JsonProperty("ChancedEnemies", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ChancedEnemies")]
        public List<ChancedEnemy>? ChancedEnemies { get; set; }

        [JsonProperty("Warn", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Warn")]
        public List<string>? Warn { get; set; }

        [JsonProperty("Neutral", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Neutral")]
        public List<string>? Neutral { get; set; }

        [JsonProperty("AlwaysFriends", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AlwaysFriends")]
        public List<string>? AlwaysFriends { get; set; }

        [JsonProperty("SavagePlayerBehaviour", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("SavagePlayerBehaviour")]
        public string? SavagePlayerBehaviour { get; set; }

        [JsonProperty("BearPlayerBehaviour", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BearPlayerBehaviour")]
        public string? BearPlayerBehaviour { get; set; }

        [JsonProperty("BearEnemyChance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BearEnemyChance")]
        public int? BearEnemyChance { get; set; }

        [JsonProperty("UsecPlayerBehaviour", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("UsecPlayerBehaviour")]
        public string? UsecPlayerBehaviour { get; set; }

        [JsonProperty("UsecEnemyChance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("UsecEnemyChance")]
        public int? UsecEnemyChance { get; set; }
    }
}