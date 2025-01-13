using Reqnroll.Assist.Attributes;

namespace LootDumpProcessor.Tests.Unit.Processors.Ammo.Tables;

public static class ProbabilitiesTable
{
    public static IReadOnlyList<Row> GetProbabilities(this Table table) => table.CreateSet<Row>().ToArray();

    public sealed class Row
    {
        public string Caliber { get; set; } = string.Empty;
        [TableAliases("Relative probability")]
        public int Count { get; set; }
    }
}