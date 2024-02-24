using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Model;

namespace JishoTangoAssistant.Services;

public interface IVocabularyListPersistanceService
{
    Task ReplaceVocabularyListAsync(IList<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true);
}