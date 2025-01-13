﻿using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Processing;

namespace LootDumpProcessor.Process.Processor.AmmoProcessor;

public interface IAmmoProcessor
{
    IReadOnlyDictionary<string, List<AmmoDistribution>> CreateAmmoDistribution(List<PreProcessedStaticLoot> containers);
}