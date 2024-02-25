using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Model;

namespace JishoTangoAssistant.Services;

public interface IVocabularyListPersistanceService
{
    IEnumerable<VocabularyItem> GetVocabularyItems();
    Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true);
}