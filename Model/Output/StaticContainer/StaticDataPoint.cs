using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Output.StaticContainer;

public class StaticDataPoint
{
    [JsonProperty("probability", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("probability")]
    public double? Probability { get; set; }
    
    [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("template")]
    public Template? Template { get; set; }
}