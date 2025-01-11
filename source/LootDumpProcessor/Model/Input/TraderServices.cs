namespace LootDumpProcessor.Model.Input;

public readonly record struct TraderServices(
    TraderService? ExUsecLoyalty, TraderService? ZryachiyAid, TraderService? CultistsAid, TraderService? PlayerTaxi,
    TraderService? BtrItemsDelivery, TraderService? BtrBotCover, TraderService? TransitItemsDelivery
);