using JishoTangoAssistant.Domain.Core.Models;

namespace JishoTangoAssistant.Repositories;

public interface ICurrentSessionRepository
{
    public Task<CurrentSession> GetCurrentSessionAsync();

    public Task UpdateExportSettingsPropertyAsync(ExportSettings value);

    public Task UpdateLoadedFilePathPropertyAsync(string value);
    
    public Task UpdateUserMadeUnsavedChangesPropertyAsync(bool value);
}