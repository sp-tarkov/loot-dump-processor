using System.Text.Json.Serialization;

namespace LootDumpProcessor.Model.Input;

public record LocationLoot
{
    public LocationLoot()
    {
        
    }
    public LocationLoot(bool Enabled,
        bool EnableCoop,
        bool ForceOnlineRaidInPve,
        bool Locked,
        bool Insurance,
        bool SafeLocation,
        string Name,
        string Description,
        Scene Scene,
        float Area,
        int RequiredPlayerLevelMin,
        int RequiredPlayerLevelMax,
        int PmcMaxPlayersInGroup,
        int ScavMaxPlayersInGroup,
        int MinPlayers,
        int MaxPlayers,
        int MaxCoopGroup,
        int ExitCount,
        int ExitAccessTime,
        int ExitTime,
        Preview Preview,
        int IconX,
        int IconY,
        IReadOnlyList<object> FilterEx,
        IReadOnlyList<object> Waves,
        IReadOnlyList<object> Limits,
        int AveragePlayTime,
        int AveragePlayerLevel,
        int EscapeTimeLimit,
        int EscapeTimeLimitPve,
        int EscapeTimeLimitCoop,
        string Rules,
        bool IsSecret,
        IReadOnlyList<object> Doors, int TmpLocationFieldRemoveMe,
        int MinDistToExitPoint,
        int MaxDistToFreePoint,
        int MinDistToFreePoint,
        int MaxBotPerZone,
        string OpenZones,
        bool OcculsionCullingEnabled,
        float GlobalLootChanceModifier,
        float GlobalLootChanceModifierPvE,
        bool OldSpawn,
        bool OfflineOldSpawn,
        bool NewSpawn,
        bool OfflineNewSpawn,
        int BotMax,
        int BotMaxPvE,
        int BotStart,
        int BotStartPlayer,
        int BotStop,
        int BotMaxTimePlayer,
        int BotSpawnTimeOnMin,
        int BotSpawnTimeOnMax,
        int BotSpawnTimeOffMin,
        int BotSpawnTimeOffMax,
        int BotMaxPlayer,
        int BotEasy,
        int BotNormal,
        int BotHard,
        int BotImpossible,
        int BotAssault,
        int BotMarksman,
        string DisabledScavExits,
        int MinPlayerLvlAccessKeys,
        IReadOnlyList<object> AccessKeys,
        int UnixDateTime, int UsersGatherSeconds, int UsersSpawnSecondsN, int UsersSpawnSecondsN2, int UsersSummonSeconds, int SavSummonSeconds, int MatchingMinSeconds,
        bool GenerateLocalLootCache,
        int PlayersRequestCount,
        NonWaveGroupScenario NonWaveGroupScenario,
        int BotSpawnCountStep,
        int BotSpawnPeriodCheck,
        float GlobalContainerChanceModifier,
        IReadOnlyList<object> MinMaxBots,
        BotLocationModifier BotLocationModifier,
        IReadOnlyList<Exit> Exits,
        bool DisabledForScav,
        IReadOnlyList<object> BossLocationSpawn,
        IReadOnlyList<SpawnPointParam> SpawnPointParams,
        IReadOnlyList<object> MaxItemCountInLocation,
        IReadOnlyList<AirdropParameter> AirdropParameters,
        IReadOnlyList<MatchMakerMinPlayersByWaitTime> MatchMakerMinPlayersByWaitTime,
        IReadOnlyList<Transit> Transits,
        string Id, string Id0,
        IReadOnlyList<Template> Loot,
        IReadOnlyList<Banner> Banners)
    {
        this.Enabled = Enabled;
        this.EnableCoop = EnableCoop;
        this.ForceOnlineRaidInPve = ForceOnlineRaidInPve;
        this.Locked = Locked;
        this.Insurance = Insurance;
        this.SafeLocation = SafeLocation;
        this.Name = Name;
        this.Description = Description;
        this.Scene = Scene;
        this.Area = Area;
        this.RequiredPlayerLevelMin = RequiredPlayerLevelMin;
        this.RequiredPlayerLevelMax = RequiredPlayerLevelMax;
        this.PmcMaxPlayersInGroup = PmcMaxPlayersInGroup;
        this.ScavMaxPlayersInGroup = ScavMaxPlayersInGroup;
        this.MinPlayers = MinPlayers;
        this.MaxPlayers = MaxPlayers;
        this.MaxCoopGroup = MaxCoopGroup;
        this.ExitCount = ExitCount;
        this.ExitAccessTime = ExitAccessTime;
        this.ExitTime = ExitTime;
        this.Preview = Preview;
        this.IconX = IconX;
        this.IconY = IconY;
        this.FilterEx = FilterEx;
        this.Waves = Waves;
        this.Limits = Limits;
        this.AveragePlayTime = AveragePlayTime;
        this.AveragePlayerLevel = AveragePlayerLevel;
        this.EscapeTimeLimit = EscapeTimeLimit;
        this.EscapeTimeLimitPve = EscapeTimeLimitPve;
        this.EscapeTimeLimitCoop = EscapeTimeLimitCoop;
        this.Rules = Rules;
        this.IsSecret = IsSecret;
        this.Doors = Doors;
        this.TmpLocationFieldRemoveMe = TmpLocationFieldRemoveMe;
        this.MinDistToExitPoint = MinDistToExitPoint;
        this.MaxDistToFreePoint = MaxDistToFreePoint;
        this.MinDistToFreePoint = MinDistToFreePoint;
        this.MaxBotPerZone = MaxBotPerZone;
        this.OpenZones = OpenZones;
        this.OcculsionCullingEnabled = OcculsionCullingEnabled;
        this.GlobalLootChanceModifier = GlobalLootChanceModifier;
        this.GlobalLootChanceModifierPvE = GlobalLootChanceModifierPvE;
        this.OldSpawn = OldSpawn;
        this.OfflineOldSpawn = OfflineOldSpawn;
        this.NewSpawn = NewSpawn;
        this.OfflineNewSpawn = OfflineNewSpawn;
        this.BotMax = BotMax;
        this.BotMaxPvE = BotMaxPvE;
        this.BotStart = BotStart;
        this.BotStartPlayer = BotStartPlayer;
        this.BotStop = BotStop;
        this.BotMaxTimePlayer = BotMaxTimePlayer;
        this.BotSpawnTimeOnMin = BotSpawnTimeOnMin;
        this.BotSpawnTimeOnMax = BotSpawnTimeOnMax;
        this.BotSpawnTimeOffMin = BotSpawnTimeOffMin;
        this.BotSpawnTimeOffMax = BotSpawnTimeOffMax;
        this.BotMaxPlayer = BotMaxPlayer;
        this.BotEasy = BotEasy;
        this.BotNormal = BotNormal;
        this.BotHard = BotHard;
        this.BotImpossible = BotImpossible;
        this.BotAssault = BotAssault;
        this.BotMarksman = BotMarksman;
        this.DisabledScavExits = DisabledScavExits;
        this.MinPlayerLvlAccessKeys = MinPlayerLvlAccessKeys;
        this.AccessKeys = AccessKeys;
        this.UnixDateTime = UnixDateTime;
        this.UsersGatherSeconds = UsersGatherSeconds;
        this.UsersSpawnSecondsN = UsersSpawnSecondsN;
        this.UsersSpawnSecondsN2 = UsersSpawnSecondsN2;
        this.UsersSummonSeconds = UsersSummonSeconds;
        this.SavSummonSeconds = SavSummonSeconds;
        this.MatchingMinSeconds = MatchingMinSeconds;
        this.GenerateLocalLootCache = GenerateLocalLootCache;
        this.PlayersRequestCount = PlayersRequestCount;
        this.NonWaveGroupScenario = NonWaveGroupScenario;
        this.BotSpawnCountStep = BotSpawnCountStep;
        this.BotSpawnPeriodCheck = BotSpawnPeriodCheck;
        this.GlobalContainerChanceModifier = GlobalContainerChanceModifier;
        this.MinMaxBots = MinMaxBots;
        this.BotLocationModifier = BotLocationModifier;
        this.Exits = Exits;
        this.DisabledForScav = DisabledForScav;
        this.BossLocationSpawn = BossLocationSpawn;
        this.SpawnPointParams = SpawnPointParams;
        this.MaxItemCountInLocation = MaxItemCountInLocation;
        this.AirdropParameters = AirdropParameters;
        this.MatchMakerMinPlayersByWaitTime = MatchMakerMinPlayersByWaitTime;
        this.Transits = Transits;
        this.Id = Id;
        this.Id0 = Id0;
        this.Loot = Loot;
        this.Banners = Banners;
    }

    public bool Enabled { get; init; }
    public bool EnableCoop { get; init; }
    public bool ForceOnlineRaidInPve { get; init; }
    public bool Locked { get; init; }
    public bool Insurance { get; init; }
    public bool SafeLocation { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public Scene Scene { get; init; }
    public float Area { get; init; }
    public int RequiredPlayerLevelMin { get; init; }
    public int RequiredPlayerLevelMax { get; init; }
    public int PmcMaxPlayersInGroup { get; init; }
    public int ScavMaxPlayersInGroup { get; init; }
    public int MinPlayers { get; init; }
    public int MaxPlayers { get; init; }
    public int MaxCoopGroup { get; init; }
    public int ExitCount { get; init; }
    public int ExitAccessTime { get; init; }
    public int ExitTime { get; init; }
    public Preview Preview { get; init; }
    public int IconX { get; init; }
    public int IconY { get; init; }
    public IReadOnlyList<object> FilterEx { get; init; }
    public IReadOnlyList<object> Waves { get; init; }
    public IReadOnlyList<object> Limits { get; init; }
    public int AveragePlayTime { get; init; }
    public int AveragePlayerLevel { get; init; }
    public int EscapeTimeLimit { get; init; }
    public int EscapeTimeLimitPve { get; init; }
    public int EscapeTimeLimitCoop { get; init; }
    public string Rules { get; init; }
    public bool IsSecret { get; init; }
    public IReadOnlyList<object> Doors { get; init; }
    [JsonPropertyName("tmp_location_field_remove_me")] public int TmpLocationFieldRemoveMe { get; init; }
    public int MinDistToExitPoint { get; init; }
    public int MaxDistToFreePoint { get; init; }
    public int MinDistToFreePoint { get; init; }
    public int MaxBotPerZone { get; init; }
    public string OpenZones { get; init; }
    public bool OcculsionCullingEnabled { get; init; }
    public float GlobalLootChanceModifier { get; init; }
    public float GlobalLootChanceModifierPvE { get; init; }
    public bool OldSpawn { get; init; }
    public bool OfflineOldSpawn { get; init; }
    public bool NewSpawn { get; init; }
    public bool OfflineNewSpawn { get; init; }
    public int BotMax { get; init; }
    public int BotMaxPvE { get; init; }
    public int BotStart { get; init; }
    public int BotStartPlayer { get; init; }
    public int BotStop { get; init; }
    public int BotMaxTimePlayer { get; init; }
    public int BotSpawnTimeOnMin { get; init; }
    public int BotSpawnTimeOnMax { get; init; }
    public int BotSpawnTimeOffMin { get; init; }
    public int BotSpawnTimeOffMax { get; init; }
    public int BotMaxPlayer { get; init; }
    public int BotEasy { get; init; }
    public int BotNormal { get; init; }
    public int BotHard { get; init; }
    public int BotImpossible { get; init; }
    public int BotAssault { get; init; }
    public int BotMarksman { get; init; }
    public string DisabledScavExits { get; init; }
    public int MinPlayerLvlAccessKeys { get; init; }
    public IReadOnlyList<object> AccessKeys { get; init; }
    public int UnixDateTime { get; init; }
    [JsonPropertyName("users_gather_seconds")] public int UsersGatherSeconds { get; init; }
    [JsonPropertyName("users_spawn_seconds_n")] public int UsersSpawnSecondsN { get; init; }
    [JsonPropertyName("users_spawn_seconds_n2")] public int UsersSpawnSecondsN2 { get; init; }
    [JsonPropertyName("users_summon_seconds")] public int UsersSummonSeconds { get; init; }
    [JsonPropertyName("sav_summon_seconds")] public int SavSummonSeconds { get; init; }
    [JsonPropertyName("matching_min_seconds")] public int MatchingMinSeconds { get; init; }
    public bool GenerateLocalLootCache { get; init; }
    public int PlayersRequestCount { get; init; }
    public NonWaveGroupScenario NonWaveGroupScenario { get; init; }
    public int BotSpawnCountStep { get; init; }
    public int BotSpawnPeriodCheck { get; init; }
    public float GlobalContainerChanceModifier { get; init; }
    public IReadOnlyList<object> MinMaxBots { get; init; }
    public BotLocationModifier BotLocationModifier { get; init; }
    public IReadOnlyList<Exit> Exits { get; init; }
    public bool DisabledForScav { get; init; }
    public IReadOnlyList<object> BossLocationSpawn { get; init; }
    public IReadOnlyList<SpawnPointParam> SpawnPointParams { get; init; }
    public IReadOnlyList<object> MaxItemCountInLocation { get; init; }
    public IReadOnlyList<AirdropParameter> AirdropParameters { get; init; }
    public IReadOnlyList<MatchMakerMinPlayersByWaitTime> MatchMakerMinPlayersByWaitTime { get; init; }
    public IReadOnlyList<Transit> Transits { get; init; }
    public string Id { get; init; }
    [JsonPropertyName("_Id")] public string Id0 { get; init; }
    public IReadOnlyList<Template> Loot { get; init; }
    public IReadOnlyList<Banner> Banners { get; init; }

    public void Deconstruct(out bool Enabled, out bool EnableCoop, out bool ForceOnlineRaidInPve, out bool Locked, out bool Insurance, out bool SafeLocation, out string Name, out string Description, out Scene Scene, out float Area, out int RequiredPlayerLevelMin, out int RequiredPlayerLevelMax, out int PmcMaxPlayersInGroup, out int ScavMaxPlayersInGroup, out int MinPlayers, out int MaxPlayers, out int MaxCoopGroup, out int ExitCount, out int ExitAccessTime, out int ExitTime, out Preview Preview, out int IconX, out int IconY, out IReadOnlyList<object> FilterEx, out IReadOnlyList<object> Waves, out IReadOnlyList<object> Limits, out int AveragePlayTime, out int AveragePlayerLevel, out int EscapeTimeLimit, out int EscapeTimeLimitPve, out int EscapeTimeLimitCoop, out string Rules, out bool IsSecret, out IReadOnlyList<object> Doors, out int TmpLocationFieldRemoveMe, out int MinDistToExitPoint, out int MaxDistToFreePoint, out int MinDistToFreePoint, out int MaxBotPerZone, out string OpenZones, out bool OcculsionCullingEnabled, out float GlobalLootChanceModifier, out float GlobalLootChanceModifierPvE, out bool OldSpawn, out bool OfflineOldSpawn, out bool NewSpawn, out bool OfflineNewSpawn, out int BotMax, out int BotMaxPvE, out int BotStart, out int BotStartPlayer, out int BotStop, out int BotMaxTimePlayer, out int BotSpawnTimeOnMin, out int BotSpawnTimeOnMax, out int BotSpawnTimeOffMin, out int BotSpawnTimeOffMax, out int BotMaxPlayer, out int BotEasy, out int BotNormal, out int BotHard, out int BotImpossible, out int BotAssault, out int BotMarksman, out string DisabledScavExits, out int MinPlayerLvlAccessKeys, out IReadOnlyList<object> AccessKeys, out int UnixDateTime, out int UsersGatherSeconds, out int UsersSpawnSecondsN, out int UsersSpawnSecondsN2, out int UsersSummonSeconds, out int SavSummonSeconds, out int MatchingMinSeconds, out bool GenerateLocalLootCache, out int PlayersRequestCount, out NonWaveGroupScenario NonWaveGroupScenario, out int BotSpawnCountStep, out int BotSpawnPeriodCheck, out float GlobalContainerChanceModifier, out IReadOnlyList<object> MinMaxBots, out BotLocationModifier BotLocationModifier, out IReadOnlyList<Exit> Exits, out bool DisabledForScav, out IReadOnlyList<object> BossLocationSpawn, out IReadOnlyList<SpawnPointParam> SpawnPointParams, out IReadOnlyList<object> MaxItemCountInLocation, out IReadOnlyList<AirdropParameter> AirdropParameters, out IReadOnlyList<MatchMakerMinPlayersByWaitTime> MatchMakerMinPlayersByWaitTime, out IReadOnlyList<Transit> Transits, out string Id, out string Id0, out IReadOnlyList<Template> Loot, out IReadOnlyList<Banner> Banners)
    {
        Enabled = this.Enabled;
        EnableCoop = this.EnableCoop;
        ForceOnlineRaidInPve = this.ForceOnlineRaidInPve;
        Locked = this.Locked;
        Insurance = this.Insurance;
        SafeLocation = this.SafeLocation;
        Name = this.Name;
        Description = this.Description;
        Scene = this.Scene;
        Area = this.Area;
        RequiredPlayerLevelMin = this.RequiredPlayerLevelMin;
        RequiredPlayerLevelMax = this.RequiredPlayerLevelMax;
        PmcMaxPlayersInGroup = this.PmcMaxPlayersInGroup;
        ScavMaxPlayersInGroup = this.ScavMaxPlayersInGroup;
        MinPlayers = this.MinPlayers;
        MaxPlayers = this.MaxPlayers;
        MaxCoopGroup = this.MaxCoopGroup;
        ExitCount = this.ExitCount;
        ExitAccessTime = this.ExitAccessTime;
        ExitTime = this.ExitTime;
        Preview = this.Preview;
        IconX = this.IconX;
        IconY = this.IconY;
        FilterEx = this.FilterEx;
        Waves = this.Waves;
        Limits = this.Limits;
        AveragePlayTime = this.AveragePlayTime;
        AveragePlayerLevel = this.AveragePlayerLevel;
        EscapeTimeLimit = this.EscapeTimeLimit;
        EscapeTimeLimitPve = this.EscapeTimeLimitPve;
        EscapeTimeLimitCoop = this.EscapeTimeLimitCoop;
        Rules = this.Rules;
        IsSecret = this.IsSecret;
        Doors = this.Doors;
        TmpLocationFieldRemoveMe = this.TmpLocationFieldRemoveMe;
        MinDistToExitPoint = this.MinDistToExitPoint;
        MaxDistToFreePoint = this.MaxDistToFreePoint;
        MinDistToFreePoint = this.MinDistToFreePoint;
        MaxBotPerZone = this.MaxBotPerZone;
        OpenZones = this.OpenZones;
        OcculsionCullingEnabled = this.OcculsionCullingEnabled;
        GlobalLootChanceModifier = this.GlobalLootChanceModifier;
        GlobalLootChanceModifierPvE = this.GlobalLootChanceModifierPvE;
        OldSpawn = this.OldSpawn;
        OfflineOldSpawn = this.OfflineOldSpawn;
        NewSpawn = this.NewSpawn;
        OfflineNewSpawn = this.OfflineNewSpawn;
        BotMax = this.BotMax;
        BotMaxPvE = this.BotMaxPvE;
        BotStart = this.BotStart;
        BotStartPlayer = this.BotStartPlayer;
        BotStop = this.BotStop;
        BotMaxTimePlayer = this.BotMaxTimePlayer;
        BotSpawnTimeOnMin = this.BotSpawnTimeOnMin;
        BotSpawnTimeOnMax = this.BotSpawnTimeOnMax;
        BotSpawnTimeOffMin = this.BotSpawnTimeOffMin;
        BotSpawnTimeOffMax = this.BotSpawnTimeOffMax;
        BotMaxPlayer = this.BotMaxPlayer;
        BotEasy = this.BotEasy;
        BotNormal = this.BotNormal;
        BotHard = this.BotHard;
        BotImpossible = this.BotImpossible;
        BotAssault = this.BotAssault;
        BotMarksman = this.BotMarksman;
        DisabledScavExits = this.DisabledScavExits;
        MinPlayerLvlAccessKeys = this.MinPlayerLvlAccessKeys;
        AccessKeys = this.AccessKeys;
        UnixDateTime = this.UnixDateTime;
        UsersGatherSeconds = this.UsersGatherSeconds;
        UsersSpawnSecondsN = this.UsersSpawnSecondsN;
        UsersSpawnSecondsN2 = this.UsersSpawnSecondsN2;
        UsersSummonSeconds = this.UsersSummonSeconds;
        SavSummonSeconds = this.SavSummonSeconds;
        MatchingMinSeconds = this.MatchingMinSeconds;
        GenerateLocalLootCache = this.GenerateLocalLootCache;
        PlayersRequestCount = this.PlayersRequestCount;
        NonWaveGroupScenario = this.NonWaveGroupScenario;
        BotSpawnCountStep = this.BotSpawnCountStep;
        BotSpawnPeriodCheck = this.BotSpawnPeriodCheck;
        GlobalContainerChanceModifier = this.GlobalContainerChanceModifier;
        MinMaxBots = this.MinMaxBots;
        BotLocationModifier = this.BotLocationModifier;
        Exits = this.Exits;
        DisabledForScav = this.DisabledForScav;
        BossLocationSpawn = this.BossLocationSpawn;
        SpawnPointParams = this.SpawnPointParams;
        MaxItemCountInLocation = this.MaxItemCountInLocation;
        AirdropParameters = this.AirdropParameters;
        MatchMakerMinPlayersByWaitTime = this.MatchMakerMinPlayersByWaitTime;
        Transits = this.Transits;
        Id = this.Id;
        Id0 = this.Id0;
        Loot = this.Loot;
        Banners = this.Banners;
    }
}