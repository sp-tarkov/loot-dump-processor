using System.Text.Json.Serialization;


namespace LootDumpProcessor.Model.Input;

public class LocationLoot
{
    public bool? Enabled { get; set; }
    public bool? EnableCoop { get; set; }
    public bool? ForceOnlineRaidInPVE { get; set; }
    public bool? Locked { get; set; }
    public bool? Insurance { get; set; }
    public bool? SafeLocation { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Scene? Scene { get; set; }
    public float? Area { get; set; }
    public int? RequiredPlayerLevelMin { get; set; }
    public int? RequiredPlayerLevelMax { get; set; }
    public int? PmcMaxPlayersInGroup { get; set; }
    public int? ScavMaxPlayersInGroup { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public int? MaxCoopGroup { get; set; }
    public int? ExitCount { get; set; }
    public int? ExitAccessTime { get; set; }
    public int? ExitTime { get; set; }
    public Preview? Preview { get; set; }
    public int? IconX { get; set; }
    public int? IconY { get; set; }
    public List<object>? FilterEx { get; set; }
    public List<object>? Waves { get; set; }
    public List<object>? Limits { get; set; }
    public int? AveragePlayTime { get; set; }
    public int? AveragePlayerLevel { get; set; }
    public int? EscapeTimeLimit { get; set; }
    public int? EscapeTimeLimitPVE { get; set; }
    public int? EscapeTimeLimitCoop { get; set; }
    public string? Rules { get; set; }
    public bool? IsSecret { get; set; }
    public List<object>? Doors { get; set; }
    [JsonPropertyName("tmp_location_field_remove_me")] public int? TmpLocationFieldRemoveMe { get; set; }
    public int? MinDistToExitPoint { get; set; }
    public int? MaxDistToFreePoint { get; set; }
    public int? MinDistToFreePoint { get; set; }
    public int? MaxBotPerZone { get; set; }
    public string? OpenZones { get; set; }
    public bool? OcculsionCullingEnabled { get; set; }
    public float? GlobalLootChanceModifier { get; set; }
    public float? GlobalLootChanceModifierPvE { get; set; }
    public bool? OldSpawn { get; set; }
    public bool? OfflineOldSpawn { get; set; }
    public bool? NewSpawn { get; set; }
    public bool? OfflineNewSpawn { get; set; }
    public int? BotMax { get; set; }


    public int? BotMaxPvE { get; set; }


    public int? BotStart { get; set; }


    public int? BotStartPlayer { get; set; }


    public int? BotStop { get; set; }


    public int? BotMaxTimePlayer { get; set; }


    public int? BotSpawnTimeOnMin { get; set; }


    public int? BotSpawnTimeOnMax { get; set; }


    public int? BotSpawnTimeOffMin { get; set; }


    public int? BotSpawnTimeOffMax { get; set; }


    public int? BotMaxPlayer { get; set; }


    public int? BotEasy { get; set; }


    public int? BotNormal { get; set; }


    public int? BotHard { get; set; }


    public int? BotImpossible { get; set; }


    public int? BotAssault { get; set; }


    public int? BotMarksman { get; set; }


    public string? DisabledScavExits { get; set; }


    public int? MinPlayerLvlAccessKeys { get; set; }

    public List<object>? AccessKeys { get; set; }


    public int? UnixDateTime { get; set; }


    [JsonPropertyName("users_gather_seconds")] public int? UsersGatherSeconds { get; set; }


    [JsonPropertyName("users_spawn_seconds_n")] public int? UsersSpawnSecondsN { get; set; }


    [JsonPropertyName("users_spawn_seconds_n2")] public int? UsersSpawnSecondsN2 { get; set; }


    [JsonPropertyName("users_summon_seconds")] public int? UsersSummonSeconds { get; set; }


    [JsonPropertyName("sav_summon_seconds")] public int? SavSummonSeconds { get; set; }


    [JsonPropertyName("matching_min_seconds")] public int? MatchingMinSeconds { get; set; }


    public bool? GenerateLocalLootCache { get; set; }


    public int? PlayersRequestCount { get; set; }


    public NonWaveGroupScenario? NonWaveGroupScenario { get; set; }


    public int? BotSpawnCountStep { get; set; }


    public int? BotSpawnPeriodCheck { get; set; }


    public float? GlobalContainerChanceModifier { get; set; }


    public List<object>? MinMaxBots { get; set; }


    public BotLocationModifier? BotLocationModifier { get; set; }


    public List<Exit>? Exits { get; set; }


    public bool? DisabledForScav { get; set; }


    public List<object>? BossLocationSpawn { get; set; }


    public List<SpawnPointParam>? SpawnPointParams { get; set; }


    public List<object>? MaxItemCountInLocation { get; set; }


    public List<AirdropParameter>? AirdropParameters { get; set; }


    public List<MatchMakerMinPlayersByWaitTime>? MatchMakerMinPlayersByWaitTime { get; set; }


    public List<Transit>? Transits { get; set; }


    public required string Id { get; set; }


    [JsonPropertyName("_Id")] public string? Id0 { get; set; }


    public required List<Template> Loot { get; set; }


    public List<Banner>? Banners { get; set; }
}