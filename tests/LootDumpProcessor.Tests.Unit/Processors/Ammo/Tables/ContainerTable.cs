namespace LootDumpProcessor.Tests.Unit.Processors.Ammo.Tables;

public static class ContainerTable
{
    public static IReadOnlyList<Row> GetContainerItems(this Table table) => table.CreateSet<Row>().ToArray();

    public sealed record Row
    {
        public string Tpl { get; set; } = string.Empty;
    }
}