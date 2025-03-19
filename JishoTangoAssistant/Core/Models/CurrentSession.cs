namespace JishoTangoAssistant.Core.Models;

public record CurrentSession
{
    public bool UserMadeChanges { get; set; }

    public ExportSettings ExportSettings { get; set; } = new();

    public string? LoadedFilePath { get; set; }
}