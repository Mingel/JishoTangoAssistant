using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JishoTangoAssistant.Model;

namespace JishoTangoAssistant.Services;

public class VocabularyListService : IVocabularyListService
{
    private readonly IVocabularyListPersistanceService persistanceService;
    private readonly ReadOnlyObservableVocabularyList readOnlyVocabularyList;
    private readonly ObservableVocabularyList vocabularyList;

    public VocabularyListService()
    {
        persistanceService = new VocabularyListPersistanceService(); // TODO DI
        var items = persistanceService.GetVocabularyItems();
        vocabularyList = new ObservableVocabularyList(items);
        readOnlyVocabularyList = new ReadOnlyObservableVocabularyList(vocabularyList);
    }

    public VocabularyListService(IVocabularyListPersistanceService persistanceService)
    {
        this.persistanceService = persistanceService;
        var items = persistanceService.GetVocabularyItems();
        vocabularyList = new ObservableVocabularyList(items);
        readOnlyVocabularyList = new ReadOnlyObservableVocabularyList(vocabularyList);
    }

    public ReadOnlyObservableVocabularyList GetList()
    {
        return readOnlyVocabularyList;
    }

    public async Task AddAsync(VocabularyItem item)
    {
        vocabularyList.Add(item);
        await persistanceService.ReplaceVocabularyListAsync(vocabularyList);
    }

    public bool ContainsWord(string word) => vocabularyList.ContainsWord(word);

    public bool Contains(VocabularyItem item) => vocabularyList.Contains(item);

    public int Count() => vocabularyList.Count;

    public async Task UndoAsync()
    {
        vocabularyList.Undo();
        await persistanceService.ReplaceVocabularyListAsync(vocabularyList);
    }

    public async Task ClearAsync(bool resetAutoIncrementId = true)
    {
        vocabularyList.Clear();
        await persistanceService.ReplaceVocabularyListAsync(vocabularyList, resetAutoIncrementId);
    }

    public async Task AddRangeAsync(IEnumerable<VocabularyItem> items)
    {
        vocabularyList.AddRange(items);
        await persistanceService.ReplaceVocabularyListAsync(vocabularyList);
    }

    public async Task RemoveAtAsync(int index)
    {
        vocabularyList.RemoveAt(index);
        await persistanceService.ReplaceVocabularyListAsync(vocabularyList);
    }
    
    public async Task SwapAsync(int firstIndex, int secondIndex)
    {
        if (firstIndex == secondIndex)
            return;
        if (Math.Min(firstIndex, secondIndex) < 0)
            throw new ArgumentException("indices must be greater than or equal to 0");
        if (Math.Max(firstIndex, secondIndex) >= vocabularyList.Count)
            throw new ArgumentException("indices must be less than the vocabulary list count");
        (vocabularyList[firstIndex], vocabularyList[secondIndex]) = (vocabularyList[secondIndex], vocabularyList[firstIndex]);
        await persistanceService.ReplaceVocabularyListAsync(vocabularyList);
    }
}