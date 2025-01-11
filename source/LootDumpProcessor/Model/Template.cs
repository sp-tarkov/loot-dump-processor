using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Utils;


namespace LootDumpProcessor.Model;

public class Template : IKeyable, ICloneable
{
    [JsonIgnore] public string __ID { get; } = KeyGenerator.GetNextKey();


    public string? Id { get; set; }


    public bool? IsContainer { get; set; }


    public bool? UseGravity { get; set; }


    public bool? RandomRotation { get; set; }


    public Vector3? Position { get; set; }


    public Vector3? Rotation { get; set; }


    public bool? IsGroupPosition { get; set; }


    public List<GroupPosition>? GroupPositions { get; set; }


    public bool? IsAlwaysSpawn { get; set; }


    public string? Root { get; set; }


    public required List<Item> Items { get; set; }

    protected bool Equals(Template other) => Id == other.Id;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Template)obj);
    }

    public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

    public IKey GetKey()
    {
        return new FlatUniqueKey(new[] { __ID });
    }

    public object Clone() => new Template
    {
        Id = Id,
        IsContainer = IsContainer,
        UseGravity = UseGravity,
        RandomRotation = RandomRotation,
        Position = ProcessorUtil.Copy(Position),
        Rotation = ProcessorUtil.Copy(Rotation),
        IsGroupPosition = IsGroupPosition,
        GroupPositions = ProcessorUtil.Copy(GroupPositions),
        IsAlwaysSpawn = IsAlwaysSpawn,
        Root = Root,
        Items = ProcessorUtil.Copy(Items)
    };
}