namespace LootDumpProcessor.Model.Input;

public record RootData
{
    public RootData()
    {
    }

    public RootData(int Err, Data Data, object Errmsg)
    {
        this.Err = Err;
        this.Data = Data;
        this.Errmsg = Errmsg;
    }

    public int Err { get; init; }
    public Data Data { get; init; }
    public object Errmsg { get; init; }

    public void Deconstruct(out int Err, out Data Data, out object Errmsg)
    {
        Err = this.Err;
        Data = this.Data;
        Errmsg = this.Errmsg;
    }
}