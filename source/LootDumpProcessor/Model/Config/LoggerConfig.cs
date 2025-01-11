using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class LoggerConfig
{
    [JsonProperty("logLevel")]
    [JsonPropertyName("logLevel")]
    public LogLevel LogLevel { get; set; }
    
    [JsonProperty("queueLoggerPoolingTimeoutMs")]
    [JsonPropertyName("queueLoggerPoolingTimeoutMs")]
    public int QueueLoggerPoolingTimeoutMs { get; set; } = 1000;
}