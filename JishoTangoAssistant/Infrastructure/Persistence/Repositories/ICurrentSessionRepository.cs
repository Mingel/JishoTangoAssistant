using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Infrastructure.Persistence.Repositories;

public interface ICurrentSessionRepository
{
    public Task<CurrentSession> GetCurrentSessionAsync();

    public Task UpdatExportSettingsPropertyAsync(ExportSettings value);

    public Task UpdateLoadedFilePathPropertyAsync(string value);
}