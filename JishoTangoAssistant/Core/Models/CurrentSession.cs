namespace JishoTangoAssistant.Core.Models;

public record CurrentSession
{
    public bool UserMadeUnsavedChanges { get; set; }

    public ExportSettings ExportSettings { get; set; } = new();

    public string? LoadedFilePath { get; set; }
}