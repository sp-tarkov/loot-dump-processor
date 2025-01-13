using Reqnroll;

namespace LootDumpProcessor.Tests.Unit.Processors.Ammo.Tables;

public static class CalibersCountTable
{
    public static IReadOnlyList<Row> GetCalibersCount(this Table table) => table.CreateSet<Row>().ToArray();

    public sealed class Row
    {
        public string Caliber { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}