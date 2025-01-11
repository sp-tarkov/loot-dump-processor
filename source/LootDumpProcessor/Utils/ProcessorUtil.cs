using System.Security.Cryptography;
using System.Text;
using LootDumpProcessor.Model;

namespace LootDumpProcessor.Utils;

public static class ProcessorUtil
{
    public static string GetSaneId(this Template x) =>
        $"({x.Position.X}, {x.Position.Y}, {x.Position.Z}, {Math.Round(x.Rotation.X, 3)}," +
        $" {Math.Round(x.Rotation.Y, 3)}, {Math.Round(x.Rotation.Z, 3)}," +
        $" {x.UseGravity}, {x.IsGroupPosition})";

    public static string GetLocationId(this Template x) => $"({x.Position.X}, {x.Position.Y}, {x.Position.Z})";

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