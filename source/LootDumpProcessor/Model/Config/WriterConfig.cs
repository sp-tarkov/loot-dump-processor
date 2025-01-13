using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;


namespace LootDumpProcessor.Model.Config;

[UsedImplicitly]
public record WriterConfig([Required] string OutputLocation);