using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Utils;


namespace LootDumpProcessor.Model;

public class Template : IKeyable, ICloneable
{
    [JsonIgnore] public string internalId { get; }
    public string Id { get; set; }
    public bool IsContainer { get; set; }
    public bool UseGravity { get; set; }
    public bool RandomRotation { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public bool IsGroupPosition { get; set; }
    public List<GroupPosition> GroupPositions { get; set; }
    public bool IsAlwaysSpawn { get; set; }
    public string Root { get; set; }
    public List<Item> Items { get; set; }

    public Template(string internalId, string id, bool isContainer, bool useGravity, bool randomRotation,
        Vector3 position, Vector3 rotation, bool isGroupPosition, List<GroupPosition> groupPositions,
        bool isAlwaysSpawn, string root, List<Item> items)
    {
        this.internalId = internalId;
        Id = id;
        IsContainer = isContainer;
        UseGravity = useGravity;
        RandomRotation = randomRotation;
        Position = position;
        Rotation = rotation;
        IsGroupPosition = isGroupPosition;
        GroupPositions = groupPositions;
        IsAlwaysSpawn = isAlwaysSpawn;
        Root = root;
        Items = items;
    }

    private bool Equals(Template other) => Id == other.Id;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Template)obj);
    }

    public override int GetHashCode() => Id != null ? Id.GetHashCode() : 0;

    public IKey GetKey() => new FlatUniqueKey([internalId]);

    public object Clone() => new Template
    (
        internalId,
        Id,
        IsContainer,
        UseGravity,
        RandomRotation,
        Position,
        Rotation,
        IsGroupPosition,
        ProcessorUtil.Copy(GroupPositions),
        IsAlwaysSpawn,
        Root,
        ProcessorUtil.Copy(Items)
    );
}