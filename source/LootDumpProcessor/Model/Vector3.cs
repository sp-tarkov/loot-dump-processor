namespace LootDumpProcessor.Model;

public class Vector3 : ICloneable
{
    public float? X { get; set; }


    public float? Y { get; set; }


    public float? Z { get; set; }

    public object Clone() => new Vector3
    {
        X = X,
        Y = Y,
        Z = Z
    };
}