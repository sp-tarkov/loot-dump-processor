namespace LootDumpProcessor.Model.Input;

public readonly record struct ServerSettings(
    TraderServerSettings TraderServerSettings, BTRServerSettings BtrServerSettings
);