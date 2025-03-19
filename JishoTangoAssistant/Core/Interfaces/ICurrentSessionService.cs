using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Core.Interfaces;

public interface ICurrentSessionService
{
    public bool GetUserMadeChanges();
    public void SetUserMadeChanges(bool value);
    public ExportSettings GetExportSettings();
    public void SetExportSettings(ExportSettings value);
    public string? GetLoadedFilePath();
    public void SetLoadedFilePath(string? value);
}