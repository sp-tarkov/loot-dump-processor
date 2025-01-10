using System.Globalization;
using System.Text.RegularExpressions;
using LootDumpProcessor.Logger;

namespace LootDumpProcessor.Process.Reader.Filters;

public class JsonDumpFileFilter : IFileFilter
{
    private static readonly Regex _fileNameDateRegex = new("([0-9]{4}(-[0-9]{2}){2}_((-){0,1}[0-9]{2}){3})");
    private static readonly DateTime _parsedThresholdDate;

    static JsonDumpFileFilter()
    {
        // Calculate parsed date from config threshold
        if (string.IsNullOrEmpty(LootDumpProcessorContext.GetConfig().ReaderConfig.ThresholdDate))
        {
            if (LoggerFactory.GetInstance().CanBeLogged(LogLevel.Warning))
                LoggerFactory.GetInstance()
                    .Log($"ThresholdDate is null or empty in configs, defaulting to current day minus 30 days",
                        LogLevel.Warning);
            _parsedThresholdDate = (DateTime.Now - TimeSpan.FromDays(30));
        }
        else
        {
            _parsedThresholdDate = DateTime.ParseExact(
                LootDumpProcessorContext.GetConfig().ReaderConfig.ThresholdDate,
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