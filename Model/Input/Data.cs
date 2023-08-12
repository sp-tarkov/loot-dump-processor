using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Input
{
    public class Data
    {
        [JsonProperty("Enabled", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Enabled")]
        public bool? Enabled { get; set; }

        [JsonProperty("EnableCoop", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EnableCoop")]
        public bool? EnableCoop { get; set; }

        [JsonProperty("Locked", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Locked")]
        public bool? Locked { get; set; }

        [JsonProperty("Insurance", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Insurance")]
        public bool? Insurance { get; set; }

        [JsonProperty("SafeLocation", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("SafeLocation")]
        public bool? SafeLocation { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonProperty("Scene", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Scene")]
        public Preview? Scene { get; set; }

        [JsonProperty("Area", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Area")]
        public double? Area { get; set; }

        [JsonProperty("RequiredPlayerLevel", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("RequiredPlayerLevel")]
        public int? RequiredPlayerLevel { get; set; }

        [JsonProperty("PmcMaxPlayersInGroup", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("PmcMaxPlayersInGroup")]
        public int? PmcMaxPlayersInGroup { get; set; }

        [JsonProperty("ScavMaxPlayersInGroup", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ScavMaxPlayersInGroup")]
        public int? ScavMaxPlayersInGroup { get; set; }

        [JsonProperty("MinPlayers", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinPlayers")]
        public int? MinPlayers { get; set; }

        [JsonProperty("MaxPlayers", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxPlayers")]
        public int? MaxPlayers { get; set; }

        [JsonProperty("MaxCoopGroup", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxCoopGroup")]
        public int? MaxCoopGroup { get; set; }

        [JsonProperty("exit_count", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("exit_count")]
        public int? ExitCount { get; set; }

        [JsonProperty("exit_access_time", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("exit_access_time")]
        public int? ExitAccessTime { get; set; }

        [JsonProperty("exit_time", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("exit_time")]
        public int? ExitTime { get; set; }

        [JsonProperty("Preview", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Preview")]
        public Preview? Preview { get; set; }

        [JsonProperty("IconX", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("IconX")]
        public int? IconX { get; set; }

        [JsonProperty("IconY", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("IconY")]
        public int? IconY { get; set; }

        [JsonProperty("filter_ex", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("filter_ex")]
        public List<object>? FilterEx { get; set; }

        [JsonProperty("waves", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("waves")]
        public List<Wave>? Waves { get; set; }

        [JsonProperty("limits", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("limits")]
        public List<object>? Limits { get; set; }

        [JsonProperty("AveragePlayTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AveragePlayTime")]
        public int? AveragePlayTime { get; set; }

        [JsonProperty("AveragePlayerLevel", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AveragePlayerLevel")]
        public int? AveragePlayerLevel { get; set; }

        [JsonProperty("EscapeTimeLimit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EscapeTimeLimit")]
        public int? EscapeTimeLimit { get; set; }

        [JsonProperty("EscapeTimeLimitCoop", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("EscapeTimeLimitCoop")]
        public int? EscapeTimeLimitCoop { get; set; }

        [JsonProperty("Rules", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Rules")]
        public string? Rules { get; set; }

        [JsonProperty("IsSecret", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("IsSecret")]
        public bool? IsSecret { get; set; }

        [JsonProperty("doors", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("doors")]
        public List<object>? Doors { get; set; }

        [JsonProperty("tmp_location_field_remove_me", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("tmp_location_field_remove_me")]
        public int? TmpLocationFieldRemoveMe { get; set; }

        [JsonProperty("MinDistToExitPoint", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinDistToExitPoint")]
        public int? MinDistToExitPoint { get; set; }

        [JsonProperty("MaxDistToFreePoint", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxDistToFreePoint")]
        public int? MaxDistToFreePoint { get; set; }

        [JsonProperty("MinDistToFreePoint", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinDistToFreePoint")]
        public int? MinDistToFreePoint { get; set; }

        [JsonProperty("MaxBotPerZone", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MaxBotPerZone")]
        public int? MaxBotPerZone { get; set; }

        [JsonProperty("OpenZones", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("OpenZones")]
        public string? OpenZones { get; set; }

        [JsonProperty("OcculsionCullingEnabled", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("OcculsionCullingEnabled")]
        public bool? OcculsionCullingEnabled { get; set; }

        [JsonProperty("GlobalLootChanceModifier", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("GlobalLootChanceModifier")]
        public double? GlobalLootChanceModifier { get; set; }

        [JsonProperty("OldSpawn", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("OldSpawn")]
        public bool? OldSpawn { get; set; }

        [JsonProperty("NewSpawn", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("NewSpawn")]
        public bool? NewSpawn { get; set; }

        [JsonProperty("BotMax", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotMax")]
        public int? BotMax { get; set; }

        [JsonProperty("BotStart", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotStart")]
        public int? BotStart { get; set; }

        [JsonProperty("BotStop", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotStop")]
        public int? BotStop { get; set; }

        [JsonProperty("BotMaxTimePlayer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotMaxTimePlayer")]
        public int? BotMaxTimePlayer { get; set; }

        [JsonProperty("BotSpawnTimeOnMin", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotSpawnTimeOnMin")]
        public int? BotSpawnTimeOnMin { get; set; }

        [JsonProperty("BotSpawnTimeOnMax", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotSpawnTimeOnMax")]
        public int? BotSpawnTimeOnMax { get; set; }

        [JsonProperty("BotSpawnTimeOffMin", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotSpawnTimeOffMin")]
        public int? BotSpawnTimeOffMin { get; set; }

        [JsonProperty("BotSpawnTimeOffMax", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotSpawnTimeOffMax")]
        public int? BotSpawnTimeOffMax { get; set; }

        [JsonProperty("BotMaxPlayer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotMaxPlayer")]
        public int? BotMaxPlayer { get; set; }

        [JsonProperty("BotEasy", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotEasy")]
        public int? BotEasy { get; set; }

        [JsonProperty("BotNormal", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotNormal")]
        public int? BotNormal { get; set; }

        [JsonProperty("BotHard", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotHard")]
        public int? BotHard { get; set; }

        [JsonProperty("BotImpossible", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotImpossible")]
        public int? BotImpossible { get; set; }

        [JsonProperty("BotAssault", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotAssault")]
        public int? BotAssault { get; set; }

        [JsonProperty("BotMarksman", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotMarksman")]
        public int? BotMarksman { get; set; }

        [JsonProperty("DisabledScavExits", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DisabledScavExits")]
        public string? DisabledScavExits { get; set; }

        [JsonProperty("AccessKeys", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AccessKeys")]
        public List<object>? AccessKeys { get; set; }

        [JsonProperty("UnixDateTime", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("UnixDateTime")]
        public int? UnixDateTime { get; set; }

        [JsonProperty("users_gather_seconds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("users_gather_seconds")]
        public int? UsersGatherSeconds { get; set; }

        [JsonProperty("users_spawn_seconds_n", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("users_spawn_seconds_n")]
        public int? UsersSpawnSecondsN { get; set; }

        [JsonProperty("users_spawn_seconds_n2", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("users_spawn_seconds_n2")]
        public int? UsersSpawnSecondsN2 { get; set; }

        [JsonProperty("users_summon_seconds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("users_summon_seconds")]
        public int? UsersSummonSeconds { get; set; }

        [JsonProperty("sav_summon_seconds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("sav_summon_seconds")]
        public int? SavSummonSeconds { get; set; }

        [JsonProperty("matching_min_seconds", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("matching_min_seconds")]
        public int? MatchingMinSeconds { get; set; }

        [JsonProperty("GenerateLocalLootCache", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("GenerateLocalLootCache")]
        public bool? GenerateLocalLootCache { get; set; }

        [JsonProperty("MinMaxBots", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("MinMaxBots")]
        public List<object>? MinMaxBots { get; set; }

        [JsonProperty("BotLocationModifier", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BotLocationModifier")]
        public BotLocationModifier? BotLocationModifier { get; set; }

        [JsonProperty("exits", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("exits")]
        public List<Exit>? Exits { get; set; }

        [JsonProperty("DisabledForScav", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("DisabledForScav")]
        public bool? DisabledForScav { get; set; }

        [JsonProperty("ExitZones", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("ExitZones")]
        public ExitZones? ExitZones { get; set; }

        [JsonProperty("SpawnPointParams", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("SpawnPointParams")]
        public List<SpawnPointParam>? SpawnPointParams { get; set; }

        [JsonProperty("AirdropParameters", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("AirdropParameters")]
        public List<AirdropParameter>? AirdropParameters { get; set; }

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonProperty("_Id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("_Id")]
        public string? _Id { get; set; }

        [JsonProperty("Loot", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Loot")]
        public List<Template>? Loot { get; set; }

        [JsonProperty("Banners", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Banners")]
        public List<Banner>? Banners { get; set; }

        [JsonProperty("BossLocationSpawn", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("BossLocationSpawn")]
        public List<object>? BossLocationSpawn { get; set; }
    }
}