namespace LootDumpProcessor.Model;

public class GroupPosition : ICloneable
{
    public string Name { get; set; }
    public int Weight { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }

    public object Clone() => new GroupPosition
    {
        Name = Name,
        Weight = Weight,
        Position = Position,
        Rotation = Rotation
    };
}