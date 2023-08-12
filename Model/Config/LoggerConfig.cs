using System.Text.Json.Serialization;
using LootDumpProcessor.Logger;
using Newtonsoft.Json;

namespace LootDumpProcessor.Model.Config;

public class LoggerConfig
{
    [JsonProperty("logLevel")]
    [JsonPropertyName("logLevel")]
    public LogLevel LogLevel { get; set; } = LogLevel.Info;
    
    [JsonProperty("queueLoggerPoolingTimeoutMs")]
    [JsonPropertyName("queueLoggerPoolingTimeoutMs")]
    public int QueueLoggerPoolingTimeoutMs { get; set; } = 1000;
}