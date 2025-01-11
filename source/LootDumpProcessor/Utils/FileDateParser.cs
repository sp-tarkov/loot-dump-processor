using System.Text.RegularExpressions;

namespace LootDumpProcessor.Utils;

public static partial class FileDateParser
{
    private static readonly Regex FileDateRegex = GetRegex();

    public static bool TryParseFileDate(string fileName, out DateTime? date)
    {
        date = null;
        if (!FileDateRegex.IsMatch(fileName)) return false;

        var match = FileDateRegex.Match(fileName);
        var year = match.Groups[1].Value;
        var month = match.Groups[2].Value;
        var day = match.Groups[3].Value;
        var hour = match.Groups[4].Value;
        var minutes = match.Groups[5].Value;
        var seconds = match.Groups[6].Value;
        date = new DateTime(
            int.Parse(year),
            int.Parse(month),
            int.Parse(day),
            int.Parse(hour),
            int.Parse(minutes),
            int.Parse(seconds)
        );
        return true;
    }

    [GeneratedRegex(".*([0-9]{4})[-]([0-9]{2})[-]([0-9]{2})[_]([0-9]{2})[-]([0-9]{2})[-]([0-9]{2}).*")]
    private static partial Regex GetRegex();
}