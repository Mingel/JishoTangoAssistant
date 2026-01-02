using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Infrastructure.Entities;
using JishoTangoAssistant.Repositories;
using JishoTangoAssistant.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace JishoTangoAssistant.Infrastructure.Persistence.Repositories;

public class CurrentSessionRepository : ICurrentSessionRepository
{
    private readonly DbContext dbContext;

    public CurrentSessionRepository()
    {
        dbContext = new DbContext();
        dbContext.Database.EnsureCreated();
    }

    public async Task<CurrentSession> GetCurrentSessionAsync()
    {
        var customFontSizeProperty = await dbContext.CurrentSession
            .AsNoTracking().FirstOrDefaultAsync(property => property.Name == Constants.CurrentSessionExportSettingsCustomFontSizePropertyName);
        var ankiDeckNameProperty = await dbContext.CurrentSession
            .AsNoTracking().FirstOrDefaultAsync(property => property.Name == Constants.CurrentSessionExportSettingsAnkiDeckNamePropertyName);
        var loadedFilePathProperty = await dbContext.CurrentSession
            .AsNoTracking().FirstOrDefaultAsync(property => property.Name == Constants.CurrentSessionLoadedFilePathPropertyName);
        var userMadeUnsavedChangesProperty = await dbContext.CurrentSession
            .AsNoTracking().FirstOrDefaultAsync(property => property.Name == Constants.CurrentSessionUserMadeUnsavedChanges);
        
        int exportSettingsFontSize =
            int.TryParse(customFontSizeProperty?.Value, out exportSettingsFontSize) ? exportSettingsFontSize : Constants.DefaultFontSize;
        var loadedFilePath = loadedFilePathProperty?.Value;
        bool userMadeUnsavedChanges = bool.TryParse(userMadeUnsavedChangesProperty?.Value, out userMadeUnsavedChanges) && userMadeUnsavedChanges;
        var currentSession = CreateInitialCurrentSession() with
        {
            ExportSettings = new ExportSettings { FontSize = exportSettingsFontSize, AnkiDeckName = ankiDeckNameProperty?.Value ?? string.Empty },
            LoadedFilePath = loadedFilePath,
            UserMadeUnsavedChanges = userMadeUnsavedChanges
        };

        var addedToDatabase = false;
        if (customFontSizeProperty == null)
        {
            await dbContext.CurrentSession.AddAsync(new CurrentSessionPropertyEntity
            {
                Name = Constants.CurrentSessionExportSettingsCustomFontSizePropertyName,
                Value = exportSettingsFontSize.ToString()
            });
            addedToDatabase = true;
        }
        if (ankiDeckNameProperty == null)
        {
            await dbContext.CurrentSession.AddAsync(new CurrentSessionPropertyEntity
            {
                Name = Constants.CurrentSessionExportSettingsAnkiDeckNamePropertyName,
                Value = ankiDeckNameProperty?.Value ?? string.Empty
            });
            addedToDatabase = true;
        }
        if (loadedFilePathProperty == null)
        {
            await dbContext.CurrentSession.AddAsync(new CurrentSessionPropertyEntity
            {
                Name = Constants.CurrentSessionLoadedFilePathPropertyName,
                Value = loadedFilePath ?? string.Empty
            });
            addedToDatabase = true;
        }
        if (userMadeUnsavedChangesProperty == null)
        {
            await dbContext.CurrentSession.AddRangeAsync(new CurrentSessionPropertyEntity
            {
                Name = Constants.CurrentSessionUserMadeUnsavedChanges,
                Value = userMadeUnsavedChanges.ToString()
            });
            addedToDatabase = true;
        }

        if (addedToDatabase)
            await dbContext.SaveChangesAsync();
        
        return currentSession;
    }

    private static CurrentSession CreateInitialCurrentSession()
    {
        return new CurrentSession
        {
            UserMadeUnsavedChanges = false,
            ExportSettings = new ExportSettings(),
            LoadedFilePath = string.Empty,
        };
    }

    public async Task UpdateExportSettingsPropertyAsync(ExportSettings value)
    {
        await UpdateSessionPropertyAsync(Constants.CurrentSessionExportSettingsCustomFontSizePropertyName,
            value.FontSize);
        await UpdateSessionPropertyAsync(Constants.CurrentSessionExportSettingsAnkiDeckNamePropertyName,
            value.AnkiDeckName);
    }

    public async Task UpdateLoadedFilePathPropertyAsync(string value) =>
        await UpdateSessionPropertyAsync(Constants.CurrentSessionLoadedFilePathPropertyName, value);

    public async Task UpdateUserMadeUnsavedChangesPropertyAsync(bool value) =>
        await UpdateSessionPropertyAsync(Constants.CurrentSessionUserMadeUnsavedChanges, value);
    
    private async Task UpdateSessionPropertyAsync<T>(string propertyName, T value)
    {
        var entity = await dbContext.CurrentSession
            .FirstOrDefaultAsync(property => property.Name == propertyName);

        if (entity != null)
        {
            entity.Value = value?.ToString() ?? string.Empty;
            await dbContext.SaveChangesAsync();
        }
    }
}