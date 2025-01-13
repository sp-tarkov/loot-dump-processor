using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JetBrains.Annotations;


namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record WriterConfig([Required]string OutputLocation);