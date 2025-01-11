namespace LootDumpProcessor.Model.Input;

public class BotLocationModifier
{
    public float? AccuracySpeed { get; set; }
    public float? Scattering { get; set; }
    public float? GainSight { get; set; }
    public float? MarksmanAccuratyCoef { get; set; }
    public float? VisibleDistance { get; set; }
    public float? DistToPersueAxemanCoef { get; set; }
    public int? KhorovodChance { get; set; }
    public float? MinExfiltrationTime { get; set; }
    public float? MaxExfiltrationTime { get; set; }
    public float? DistToActivatePvE { get; set; }
    public float? DistToSleepPvE { get; set; }
    public float? DistToActivate { get; set; }
    public float? DistToSleep { get; set; }
    public List<AdditionalHostilitySetting>? AdditionalHostilitySettings { get; set; }
}