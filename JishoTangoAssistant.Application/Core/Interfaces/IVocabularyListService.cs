using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Domain.Models.Core.Collections;

namespace JishoTangoAssistant.Application.Core.Interfaces;

public interface IVocabularyListService
{
    ReadOnlyObservableVocabularyList GetList();
    Task AddAsync(VocabularyItem item);
    bool ContainsWord(string word);
    bool Contains(VocabularyItem item);
    bool SequenceEqual(IEnumerable<VocabularyItem> items);
    int Count();
    Task UndoAsync();
    Task ClearAsync(bool resetAutoIncrementId = true);
    Task AddRangeAsync(IEnumerable<VocabularyItem> items, bool removeInfoAboutLastAddedItem = false);
    Task RemoveAtAsync(int index);
    Task SwapAsync(int firstIndex, int secondIndex);
}