using JishoTangoAssistant.Domain.Core.Models;

namespace JishoTangoAssistant.Application.Core.Interfaces;

public interface IFileService
{
    Task PerformLoad(JishoTangoAssistantProfile profile, bool performOverwriting, string filePath);
    Task<JishoTangoAssistantProfile?> LoadFromFile(string absoluteFilePath);
    Task PerformSave(JishoTangoAssistantProfile profile, string loadedFilePath);
    Task PerformExport(string content, string loadedFilePath);
}