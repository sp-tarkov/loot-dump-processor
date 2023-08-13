﻿namespace LootDumpProcessor.Process;

public interface IWriter
{
    void WriteAll(Dictionary<OutputFileType, object> dumpData);

    void Write(OutputFileType type, object data);
}