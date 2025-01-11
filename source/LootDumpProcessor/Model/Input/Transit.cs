namespace LootDumpProcessor.Model.Input;

public class Transit
{
    public int? Id { get; set; }


    public bool? Active { get; set; }


    public string? Name { get; set; }


    public string? Location { get; set; }


    public string? Description { get; set; }


    public int? ActivateAfterSec { get; set; }


    public string? Target { get; set; }


    public int? Time { get; set; }


    public string? Conditions { get; set; }
}