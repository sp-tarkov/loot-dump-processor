namespace LootDumpProcessor.Model.Input;

public class RootData
{
    public int? Err { get; set; }


    public required Data Data { get; set; }


    public object? Errmsg { get; set; }
}