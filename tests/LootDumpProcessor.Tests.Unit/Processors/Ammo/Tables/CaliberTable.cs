using Reqnroll;

namespace LootDumpProcessor.Tests.Unit.Processors.Ammo.Tables;

public static class CaliberTable
{
    public static IReadOnlyList<Row> GetCalibers(this Table calibersTable) => calibersTable.CreateSet<Row>().ToArray();

    public sealed class Row
    {
        public string Caliber { get; set; } = string.Empty;
    }
}