using JishoTangoAssistant.Domain.Models.Core.Models;

namespace JishoTangoAssistant.Application.Core.Interfaces;

public interface ICurrentSessionService
{
    public bool GetUserMadeUnsavedChanges();
    public void SetUserMadeUnsavedChanges(bool value);
    public ExportSettings GetExportSettings();
    public void SetExportSettings(ExportSettings value);
    public string? GetLoadedFilePath();
    public void SetLoadedFilePath(string? value);
}