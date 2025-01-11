namespace LootDumpProcessor.Model.Input;

public class TraderService
{
    public string? TraderID { get; set; }


    public string? TraderServiceType { get; set; }


    public Requirements? Requirements { get; set; }


    public object? ServiceItemCost { get; set; }


    public List<object>? UniqueItems { get; set; }
}