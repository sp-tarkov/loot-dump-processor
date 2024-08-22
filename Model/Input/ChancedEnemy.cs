using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class ChancedEnemy
    {
        [JsonProperty("EnemyChance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EnemyChance")]
        public int? EnemyChance { get; set; }

        [JsonProperty("Role", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Role")]
        public string? Role { get; set; }
    }
}