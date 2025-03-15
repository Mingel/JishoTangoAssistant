using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Infrastructure.Persistence.Repositories;

public interface IVocabularyListRepository
{
    IEnumerable<VocabularyItem> GetVocabularyItems();
    Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true);
}