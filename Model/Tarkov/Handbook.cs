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