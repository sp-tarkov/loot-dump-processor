using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using LootDumpProcessor.Serializers.Json.Converters;


namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record DumpProcessorConfig(
    [Required] [property: JsonConverter(typeof(NetDateTimeConverter))] DateTime SpawnContainerChanceIncludeAfterDate
);