using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.Interfaces;

public interface IVocabularyListRepository
{
    IEnumerable<VocabularyItem> GetVocabularyItems();
    Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true);
}