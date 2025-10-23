using JishoTangoAssistant.Domain.Core.Models;

namespace JishoTangoAssistant.Application.Core.Interfaces;

public interface IFileService
{
    Task PerformLoad(IEnumerable<VocabularyItem> loadedVocabularyItems, bool performOverwriting, string filePath);
    Task<IEnumerable<VocabularyItem>?> LoadFromFile(string absoluteFilePath);
    Task PerformSave(IEnumerable<VocabularyItem> list, string loadedFilePath);
    Task PerformExport(string content, string loadedFilePath);
}