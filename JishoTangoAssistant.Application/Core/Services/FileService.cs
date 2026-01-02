using System.Text;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Domain.Common.Data;
using JishoTangoAssistant.Domain.Core.Models;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Application.Core.Services;

public class FileService(IVocabularyListService vocabularyListService, ICurrentSessionService currentSessionService) : IFileService
{
    public async Task PerformLoad(JishoTangoAssistantProfile loadedProfile, bool performOverwriting, string filePath)
    {
        if (performOverwriting)
            await vocabularyListService.ClearAsync();
        await vocabularyListService.AddRangeAsync(loadedProfile.VocabularyItems, true);
        currentSessionService.SetLoadedFilePath(filePath);
        currentSessionService.SetUserMadeUnsavedChanges(false);
    }

    public async Task<JishoTangoAssistantProfile?> LoadFromFile(string absoluteFilePath)
    {
        var filePath = Uri.UnescapeDataString(absoluteFilePath);

        if (!File.Exists(filePath))
            return null;

        await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var streamReader = new StreamReader(stream, Encoding.UTF8);
        var fileTextContent = await streamReader.ReadToEndAsync();
        
        var loadedFileTextInfo = new FileInfo<string>(fileTextContent, filePath);
        
        var fileContent = loadedFileTextInfo.Content;
        var content = JsonConvert.DeserializeObject<JishoTangoAssistantProfile>(fileContent);

        return content;
    }
    
    public async Task PerformSave(JishoTangoAssistantProfile profile, string loadedFilePath)
    {
        var content = JsonConvert.SerializeObject(profile, Formatting.Indented);
        ArgumentNullException.ThrowIfNull(content);
        var filePath = Uri.UnescapeDataString(loadedFilePath);
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
        await streamWriter.WriteAsync(content);
        
        Console.WriteLine($"File {filePath} saved");

        currentSessionService.SetLoadedFilePath(filePath);
        currentSessionService.SetUserMadeUnsavedChanges(false);
    }
    
    public async Task PerformExport(string content, string loadedFilePath)
    {
        ArgumentNullException.ThrowIfNull(content);
        var filePath = Uri.UnescapeDataString(loadedFilePath);
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
        await streamWriter.WriteAsync(content);
        
        Console.WriteLine($"Export to {filePath} successful");
    }
}