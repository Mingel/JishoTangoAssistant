using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Infrastructure.Persistence.Repositories;

public interface ICurrentSessionRepository
{
    public Task<CurrentSession> GetCurrentSessionAsync();

    public Task UpdateExportSettingsPropertyAsync(ExportSettings value);

    public Task UpdateLoadedFilePathPropertyAsync(string value);
    
    public Task UpdateUserMadeUnsavedChangesPropertyAsync(bool value);
}