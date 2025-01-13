namespace LootDumpProcessor.Tests.Unit.Processors.Ammo.Tables;

public static class AmmoTable
{
    public static IReadOnlyList<Row> GetAmmo(this Table table) => table.CreateSet<Row>().ToArray();

    public sealed class Row
    {
        public string Tpl { get; set; } = string.Empty;
        public string Caliber { get; set; } = string.Empty;
    }
}