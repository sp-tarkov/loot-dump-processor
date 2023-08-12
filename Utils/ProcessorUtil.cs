using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using LootDumpProcessor.Model;
using LootDumpProcessor.Serializers;
using LootDumpProcessor.Serializers.Json;

namespace LootDumpProcessor.Process.Processor;

public static class ProcessorUtil
{
    private static readonly ISerializer Serializer = JsonSerializerFactory.GetInstance();

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
        if (obj == null)
            return null;
        return obj.Select(o => o.Clone()).Cast<T>().ToList();
    }

    public static string HashFile(string text)
    {
        var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(text)));
    }
}