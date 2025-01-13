using System.Globalization;
using System.Text.RegularExpressions;
using LootDumpProcessor.Model.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LootDumpProcessor.Process.Reader.Filters;

public class JsonDumpFileFilter : IFileFilter
{
    private readonly ILogger<JsonDumpFileFilter> _logger;
    private readonly Regex _fileNameDateRegex = new("([0-9]{4}(-[0-9]{2}){2}_((-){0,1}[0-9]{2}){3})");
    private readonly DateTime _parsedThresholdDate;
    private readonly Config _config;

    public JsonDumpFileFilter(ILogger<JsonDumpFileFilter> logger, IOptions<Config> config)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _config = (config ?? throw new ArgumentNullException(nameof(config))).Value;
        // Calculate parsed date from config threshold
        if (string.IsNullOrEmpty(_config.ReaderConfig.ThresholdDate))
        {
            _logger.LogWarning("ThresholdDate is null or empty in configs, defaulting to current day minus 30 days");
            _parsedThresholdDate = DateTime.Now - TimeSpan.FromDays(30);
        }
        else
        {
            _parsedThresholdDate = DateTime.ParseExact(
                _config.ReaderConfig.ThresholdDate,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture
            );
        }
    }

    public string GetExtension() => "json";

    public bool Accept(string filename)
    {
        var unparsedDate = _fileNameDateRegex.Match(filename).Groups[1].Value;
        var date = DateTime.ParseExact(unparsedDate, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
        return date > _parsedThresholdDate;
    }
}