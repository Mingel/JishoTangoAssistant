using System.Threading.Tasks;
using JishoTangoAssistant.Core.Constants;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.Infrastructure.Entities;
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
        var customFontSizeEntity = await dbContext.CurrentSession
            .AsNoTracking().FirstOrDefaultAsync(property => property.Name == Constants.CurrentSessionExportSettingsCustomFontSizePropertyName);
        var loadedFilePathEntity = await dbContext.CurrentSession
            .AsNoTracking().FirstOrDefaultAsync(property => property.Name == Constants.CurrentSessionLoadedFilePathPropertyName);
        
        int exportSettingsFontSize =
            int.TryParse(customFontSizeEntity?.Value, out exportSettingsFontSize) ? exportSettingsFontSize : Constants.DefaultFontSize;
        var loadedFilePath = loadedFilePathEntity?.Value;
        var currentSession = CreateInitialCurrentSession() with
        {
            ExportSettings = new ExportSettings { FontSize = exportSettingsFontSize },
            LoadedFilePath = loadedFilePath,
        };

        var addedToDatabase = false;
        if (customFontSizeEntity == null)
        {
            await dbContext.CurrentSession.AddAsync(new CurrentSessionPropertyEntity
            {
                Name = Constants.CurrentSessionExportSettingsCustomFontSizePropertyName,
                Value = exportSettingsFontSize.ToString()
            });
            addedToDatabase = true;
        }
        if (loadedFilePathEntity == null)
        {
            await dbContext.CurrentSession.AddAsync(new CurrentSessionPropertyEntity
            {
                Name = Constants.CurrentSessionLoadedFilePathPropertyName,
                Value = loadedFilePath ?? string.Empty
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
            UserMadeChanges = false,
            ExportSettings = new ExportSettings(),
            LoadedFilePath = string.Empty,
        };
    }
    
    public async Task UpdatExportSettingsPropertyAsync(ExportSettings value)
    {
        var exportSettingsFontSizeEntity = await dbContext.CurrentSession
            .FirstOrDefaultAsync(property => property.Name == "ExportSettings:CustomFontSize");
        if (exportSettingsFontSizeEntity != null) 
            exportSettingsFontSizeEntity.Value = value.FontSize.ToString();
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateLoadedFilePathPropertyAsync(string value)
    {
        var loadedFilePathEntity = await dbContext.CurrentSession
            .FirstOrDefaultAsync(property => property.Name == "LoadedFilePath");
        if (loadedFilePathEntity != null) 
            loadedFilePathEntity.Value = value;
        await dbContext.SaveChangesAsync();
    }
}