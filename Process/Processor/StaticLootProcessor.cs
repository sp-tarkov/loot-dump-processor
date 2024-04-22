using LootDumpProcessor.Model;
using LootDumpProcessor.Model.Input;
using LootDumpProcessor.Model.Output;
using LootDumpProcessor.Model.Output.StaticContainer;
using LootDumpProcessor.Model.Processing;
using LootDumpProcessor.Utils;

namespace LootDumpProcessor.Process.Processor;

public static class StaticLootProcessor
{
    public static List<PreProcessedStaticLoot> PreProcessStaticLoot(List<Template> staticloot)
    {
        var containers = new List<PreProcessedStaticLoot>();
        foreach (var lootSpawnPosition in staticloot)
        {
            var tpl = lootSpawnPosition.Items[0].Tpl;
            if (!LootDumpProcessorContext.GetStaticWeaponIds().Contains(tpl))
            {
                // Only add non-weapon static containers
                containers.Add(new PreProcessedStaticLoot
                {
                    Type = tpl,
                    ContainerId = lootSpawnPosition.Items[0].Id,
                    Items = lootSpawnPosition.Items.Skip(1).ToList()
                });
            }
        }

        return containers;
    }

    public static Tuple<string, MapStaticLoot> CreateStaticWeaponsAndStaticForcedContainers(RootData rawMapDump)
    {
        var mapName = rawMapDump.Data.Name;
        var staticLootPositions = (from li in rawMapDump.Data.Loot
            where li.IsContainer ?? false
            select li).ToList();
        var staticWeapons = new List<Template>();
        staticLootPositions = staticLootPositions.OrderBy(x => x.Id).ToList();
        foreach (var staticLootPosition in staticLootPositions)
        {
            if (LootDumpProcessorContext.GetStaticWeaponIds().Contains(staticLootPosition.Items[0].Tpl))
            {
                staticWeapons.Add(ProcessorUtil.Copy(staticLootPosition));
            }
        }

        var forcedStaticItems = LootDumpProcessorContext.GetForcedItems().ContainsKey(mapName)
            ? LootDumpProcessorContext.GetForcedItems()[mapName]
            : new List<StaticForced>();

        var mapStaticData = new MapStaticLoot
        {
            StaticWeapons = staticWeapons,
            StaticForced = forcedStaticItems
        };
        return Tuple.Create(mapName, mapStaticData);
    }
    
    public static List<Template> CreateDynamicStaticContainers(RootData rawMapDump)
    {
        var data = (from li in rawMapDump.Data.Loot
            where (li.IsContainer ?? false) && (!LootDumpProcessorContext.GetStaticWeaponIds().Contains(li.Items[0].Tpl))
            select li).ToList();

        foreach (var item in data)
        {
            // remove all but first item from containers items
            item.Items = new List<Item> { item.Items[0] };
        }

        return data;
    }

    public static Dictionary<string, List<AmmoDistribution>> CreateAmmoDistribution(
        List<PreProcessedStaticLoot> container_counts
    )
    {
        var ammo = new List<string>();
        foreach (var ci in container_counts)
        {
            ammo.AddRange(from item in ci.Items
                where LootDumpProcessorContext.GetTarkovItems().IsBaseClass(item.Tpl, BaseClasses.Ammo)
                select item.Tpl);
        }

        var ammo_counts = new List<CaliberTemplateCount>();
        ammo_counts.AddRange(
            ammo.GroupBy(a => a)
                .Select(g => new CaliberTemplateCount
                {
                    Caliber = LootDumpProcessorContext.GetTarkovItems().AmmoCaliber(g.Key),
                    Template = g.Key,
                    Count = g.Count()
                })
        );
        ammo_counts = ammo_counts.OrderBy(x => x.Caliber).ToList();
        var ammo_distribution = new Dictionary<string, List<AmmoDistribution>>();
        foreach (var _tup_3 in ammo_counts.GroupBy(x => x.Caliber))
        {
            var k = _tup_3.Key;
            var g = _tup_3.ToList();
            ammo_distribution[k] = (from gi in g
                select new AmmoDistribution
                {
                    Tpl = gi.Template,
                    RelativeProbability = gi.Count
                }).ToList();
        }

        return ammo_distribution;
    }

    public static Dictionary<string, StaticItemDistribution> CreateStaticLootDistribution(
        List<PreProcessedStaticLoot> container_counts,
        Dictionary<string, MapStaticLoot> staticContainers)
    {
        // Iterate over each map we have containers for
        foreach (var map in staticContainers)
        {
            var mapName = map.Key;
            var mapContainers = map.Value;
        }

        var static_loot_distribution = new Dictionary<string, StaticItemDistribution>();
        var uniqueContainerTypeIds = Enumerable.Distinct((from ci in container_counts
            select ci.Type).ToList());

        foreach (var typeId in uniqueContainerTypeIds)
        {
            var container_counts_selected = (from ci in container_counts
                where ci.Type == typeId
                select ci).ToList();
            var itemscounts = new List<int>();
            foreach (var ci in container_counts_selected)
            {
                itemscounts.Add((from cii in ci.Items
                    where cii.ParentId == ci.ContainerId
                    select cii).ToList().Count);
            }

            static_loot_distribution[typeId] = new StaticItemDistribution();
            static_loot_distribution[typeId].ItemCountDistribution = itemscounts.GroupBy(i => i)
                .Select(g => new ItemCountDistribution
                {
                    Count = g.Key,
                    RelativeProbability = g.Count()
                }).ToList();
            // TODO: Change for different algo that splits items per parent once parentid = containerid, then compose
            // TODO: key and finally create distribution based on composed Id instead
            var itemsHitCounts = new Dictionary<string, int>();
            foreach (var ci in container_counts_selected)
            {
                foreach (var cii in ci.Items.Where(cii => cii.ParentId == ci.ContainerId))
                {
                    if (itemsHitCounts.ContainsKey(cii.Tpl))
                        itemsHitCounts[cii.Tpl] += 1;
                    else
                        itemsHitCounts[cii.Tpl] = 1;
                }
            }

            static_loot_distribution[typeId].ItemDistribution = itemsHitCounts.Select(v => new StaticDistribution
            {
                Tpl = v.Key,
                RelativeProbability = v.Value
            }).ToList();
        }

        return static_loot_distribution;
    }
}