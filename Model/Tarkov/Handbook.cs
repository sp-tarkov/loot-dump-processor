namespace LootDumpProcessor.Model.Tarkov;

public class Category
{
    public string Id { get; set; }
    public string ParentId { get; set; }
    public string Icon { get; set; }
    public string Color { get; set; }
    public string Order { get; set; }
}

public class HandbookItem
{
    public string Id { get; set; }
    public string ParentId { get; set; }
    public int Price { get; set; }
}

public class HandbookRoot
{
    public List<Category> Categories { get; set; }
    public List<HandbookItem> Items { get; set; }
}

public class StaticContainerRoot
{
    public decimal probability { get; set; }
    public StaticContainerTemplate template { get; set; }
}

public class StaticContainerTemplate
{
    public string Id { get; set; }
    public decimal SpawnChance { get; set; }
    public bool IsAlwaysSpawn { get; set; }
}