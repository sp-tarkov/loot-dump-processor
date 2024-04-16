using System.Security.Cryptography;
using System.Text;
using LootDumpProcessor.Model;

namespace LootDumpProcessor.Utils;

public static class ProcessorUtil
{
    public static string GetSaneId(this Template x)
    {
        return $"({x.Position.X}, {x.Position.Y}, {x.Position.Z}, {Math.Round(x.Rotation.X ?? 0, 3)}," +
               $" {Math.Round(x.Rotation.Y ?? 0, 3)}, {Math.Round(x.Rotation.Z ?? 0, 3)}," +
               $" {x.UseGravity}, {x.IsGroupPosition})";
    }

    public static string GetLocationId(this Template x)
    {
        return $"({x.Position.X}, {x.Position.Y}, {x.Position.Z})";
    }

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

    public static string HashFile(string text)
    {
        var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(text)));
    }
}