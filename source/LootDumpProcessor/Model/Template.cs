using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Utils;


namespace LootDumpProcessor.Model;

public class Template : IKeyable, ICloneable
{
    [JsonIgnore] public string InternalId { get; }
    [JsonPropertyName("Id")] public string Id { get; set; }
    [JsonPropertyName("IsContainer")] public bool IsContainer { get; set; }
    public bool UseGravity { get; set; }
    public bool RandomRotation { get; set; }
    [JsonPropertyName("Position")] public Vector3 Position { get; set; }
    [JsonPropertyName("Rotation")] public Vector3 Rotation { get; set; }
    [JsonPropertyName("IsGroupPosition")] public bool IsGroupPosition { get; set; }
    [JsonPropertyName("GroupPositions")] public List<GroupPosition> GroupPositions { get; set; }
    [JsonPropertyName("IsAlwaysSpawn")] public bool IsAlwaysSpawn { get; set; }
    [JsonPropertyName("Root")] public string Root { get; set; }
    [JsonPropertyName("Items")] public List<Item> Items { get; set; }

    public Template(string internalId, string id, bool isContainer, bool useGravity, bool randomRotation,
        Vector3 position, Vector3 rotation, bool isGroupPosition, List<GroupPosition> groupPositions,
        bool isAlwaysSpawn, string root, List<Item> items)
    {
        this.InternalId = internalId;
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

    public IKey GetKey() => new FlatUniqueKey([InternalId]);

    public object Clone() => new Template
    (
        InternalId,
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