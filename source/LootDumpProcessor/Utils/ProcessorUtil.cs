using System.Security.Cryptography;
using System.Text;
using LootDumpProcessor.Model;

namespace LootDumpProcessor.Utils;

public static class ProcessorUtil
{
    public static string GetSaneId(this Template x) =>
        $"({x.Position.GetValueOrDefault().X}, {x.Position.GetValueOrDefault().Y}, {x.Position.GetValueOrDefault().Z}, {Math.Round(x.Rotation.GetValueOrDefault().X, 3)}," +
        $" {Math.Round(x.Rotation.GetValueOrDefault().Y, 3)}, {Math.Round(x.Rotation.GetValueOrDefault().Z, 3)}," +
        $" {x.UseGravity}, {x.IsGroupPosition})";

    public static string GetLocationId(this Template x) =>
        $"({x.Position.GetValueOrDefault().X}, {x.Position.GetValueOrDefault().Y}, {x.Position.GetValueOrDefault().Z})";

    public static T? Copy<T>(T? obj) where T : ICloneable
    {
        if (obj == null)
            return default;
        return (T)obj.Clone();
    }

    public static List<T>? Copy<T>(List<T>? obj) where T : ICloneable
    {
        return obj?.Select(o => o.Clone()).Cast<T>().ToList();
    }

    public static string HashFile(string text) => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(text)));
}