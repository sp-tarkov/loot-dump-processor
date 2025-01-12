using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Utils;


namespace LootDumpProcessor.Model;

public class Template(
    string internalId, string? id, bool isContainer, bool? useGravity, bool? randomRotation,
    Vector3? position, Vector3? rotation, bool? isGroupPosition, List<GroupPosition>? groupPositions,
    bool? isAlwaysSpawn, string? root, List<Item> items
)
    : IKeyable, ICloneable
{
    [JsonIgnore] public string InternalId { get; } = internalId;
    [JsonPropertyName("Id")] public string? Id { get; set; } = id;
    [JsonPropertyName("IsContainer")] public bool IsContainer { get; set; } = isContainer;
    public bool? UseGravity { get; set; } = useGravity;
    public bool? RandomRotation { get; set; } = randomRotation;
    [JsonPropertyName("Position")] public Vector3? Position { get; set; } = position;
    [JsonPropertyName("Rotation")] public Vector3? Rotation { get; set; } = rotation;
    [JsonPropertyName("IsGroupPosition")] public bool? IsGroupPosition { get; set; } = isGroupPosition;
    [JsonPropertyName("GroupPositions")] public List<GroupPosition>? GroupPositions { get; set; } = groupPositions;
    [JsonPropertyName("IsAlwaysSpawn")] public bool? IsAlwaysSpawn { get; set; } = isAlwaysSpawn;
    [JsonPropertyName("Root")] public string? Root { get; set; } = root;
    [JsonPropertyName("Items")] public List<Item> Items { get; set; } = items;

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