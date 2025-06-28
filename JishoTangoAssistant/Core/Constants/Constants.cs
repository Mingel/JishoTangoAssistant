namespace JishoTangoAssistant.Core.Constants;

public abstract record Constants
{
    public const int DefaultFontSize = 28;

    public const string CurrentSessionExportSettingsCustomFontSizePropertyName = "ExportSettings:CustomFontSize";
    public const string CurrentSessionLoadedFilePathPropertyName = "LoadedFilePath";

    public const string CurrentSessionUserMadeUnsavedChanges = "UserMadeUnsavedChanges";
}