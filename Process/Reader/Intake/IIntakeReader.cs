using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process;

public interface IIntakeReader
{
    bool Read(string file, out BasicInfo basicInfo);
}