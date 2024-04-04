using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.Interfaces;

public interface IVocabularyListService
{
    ReadOnlyObservableVocabularyList GetList();
    Task AddAsync(VocabularyItem item);
    bool ContainsWord(string word);
    bool Contains(VocabularyItem item);
    int Count();
    Task UndoAsync();
    Task ClearAsync(bool resetAutoIncrementId = true);
    Task AddRangeAsync(IEnumerable<VocabularyItem> items);
    Task RemoveAtAsync(int index);
    Task SwapAsync(int firstIndex, int secondIndex);
}