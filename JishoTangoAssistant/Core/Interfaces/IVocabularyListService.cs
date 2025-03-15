using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Common.Collections;
using JishoTangoAssistant.Core.Collections;
using JishoTangoAssistant.Core.Models;


namespace JishoTangoAssistant.Core.Interfaces;

public interface IVocabularyListService
{
    ReadOnlyObservableVocabularyList GetList();
    Task AddAsync(VocabularyItem item);
    bool ContainsWord(string word);
    bool Contains(VocabularyItem item);
    int Count();
    Task UndoAsync();
    Task ClearAsync(bool resetAutoIncrementId = true);
    Task AddRangeAsync(IEnumerable<VocabularyItem> items, bool removeInfoAboutLastAddedItem = false);
    Task RemoveAtAsync(int index);
    Task SwapAsync(int firstIndex, int secondIndex);
}