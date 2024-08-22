using System.Text.Json.Serialization;
using LootDumpProcessor.Storage;
using LootDumpProcessor.Utils;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model
{
    public class Template : IKeyable, ICloneable
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string __ID { get; } = KeyGenerator.GetNextKey();

        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonProperty("IsContainer", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("IsContainer")]
        public bool? IsContainer { get; set; }

        [JsonProperty("useGravity", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("useGravity")]
        public bool? UseGravity { get; set; }

        [JsonProperty("randomRotation", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("randomRotation")]
        public bool? RandomRotation { get; set; }

        [JsonProperty("Position", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Position")]
        public Vector3? Position { get; set; }

        [JsonProperty("Rotation", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("Rotation")]
        public Vector3? Rotation { get; set; }

        [JsonProperty("IsGroupPosition", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("IsGroupPosition")]
        public bool? IsGroupPosition { get; set; }

        [JsonProperty("GroupPositions", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("GroupPositions")]
        public List<GroupPosition>? GroupPositions { get; set; }

        [JsonProperty("IsAlwaysSpawn", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("IsAlwaysSpawn")]
        public bool? IsAlwaysSpawn { get; set; }

        [JsonProperty("Root")]
        [JsonPropertyName("Root")]
        public string? Root { get; set; }

        [JsonProperty("Items")]
        [JsonPropertyName("Items")]
        public required List<Item> Items { get; set; }

        protected bool Equals(Template other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Template)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public IKey GetKey()
        {
            return new FlatUniqueKey(new[] { __ID });
        }

        public object Clone()
        {
            return new Template
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
    }
}