using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.Infrastructure.Persistence.Repositories;

namespace JishoTangoAssistant.Core.Services;

public class CurrentSessionService : ICurrentSessionService
{
    private readonly ICurrentSessionRepository currentSessionRepository;
    private readonly CurrentSession currentSession;

    public CurrentSessionService(ICurrentSessionRepository currentSessionRepository)
    {
        this.currentSessionRepository = currentSessionRepository;
        currentSession = currentSessionRepository.GetCurrentSessionAsync().Result;
    }
    
    public bool GetUserMadeUnsavedChanges()
    {
        return currentSession.UserMadeUnsavedChanges;
    }

    public void SetUserMadeUnsavedChanges(bool value)
    {
        currentSession.UserMadeUnsavedChanges = value;
        currentSessionRepository.UpdateUserMadeUnsavedChangesPropertyAsync(value);
    }
    
    public ExportSettings GetExportSettings()
    {
        return currentSession.ExportSettings;
    }
    
    public void SetExportSettings(ExportSettings value)
    {
        currentSession.ExportSettings = value;
        currentSessionRepository.UpdateExportSettingsPropertyAsync(value);
    }

    public string? GetLoadedFilePath()
    {
        return currentSession.LoadedFilePath;
    }
    
    public void SetLoadedFilePath(string? value)
    {
        currentSession.LoadedFilePath = value;
        currentSessionRepository.UpdateLoadedFilePathPropertyAsync(value ?? string.Empty);
    }
}