namespace JishoTangoAssistant.Shared.Constants;

public abstract record Constants
{
    public const int DataPersistenceTimerInterval = 300; // in milliseconds

    public const int MinFontSize = 6;
    public const int MaxFontSize = 96;
    public const int DefaultFontSize = 28;

    public const string CurrentSessionExportSettingsAnkiDeckNamePropertyName = "ExportSettings:AnkiDeckName";
    public const string CurrentSessionExportSettingsCustomFontSizePropertyName = "ExportSettings:CustomFontSize";
    public const string CurrentSessionLoadedFilePathPropertyName = "LoadedFilePath";

    public const string CurrentSessionUserMadeUnsavedChanges = "UserMadeUnsavedChanges";
}