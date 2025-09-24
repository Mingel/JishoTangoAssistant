using JishoTangoAssistant.Domain.Core.Models;

namespace JishoTangoAssistant.Repositories;

public interface IVocabularyListRepository
{
    IEnumerable<VocabularyItem> GetVocabularyItems();
    Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true);
}