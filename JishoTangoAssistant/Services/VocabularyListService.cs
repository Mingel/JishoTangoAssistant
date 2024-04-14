using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.Services;

public class VocabularyListService : IVocabularyListService
{
    private readonly IVocabularyListRepository repository;
    private readonly ReadOnlyObservableVocabularyList readOnlyVocabularyList;
    private readonly ObservableVocabularyList vocabularyList;

    public VocabularyListService(IVocabularyListRepository repository)
    {
        this.repository = repository;
        var items = repository.GetVocabularyItems();
        vocabularyList = new ObservableVocabularyList(items);
        readOnlyVocabularyList = new ReadOnlyObservableVocabularyList(vocabularyList);
    }

    public ReadOnlyObservableVocabularyList GetList()
    {
        return readOnlyVocabularyList;
    }

    public async Task AddAsync(VocabularyItem item)
    {
        SetAnkiGuid(item);
        vocabularyList.Add(item);
        await repository.ReplaceVocabularyListAsync(vocabularyList);
    }

    public bool ContainsWord(string word) => vocabularyList.ContainsWord(word);

    public bool Contains(VocabularyItem item) => vocabularyList.Contains(item);

    public int Count() => vocabularyList.Count;

    public async Task UndoAsync()
    {
        vocabularyList.Undo();
        await repository.ReplaceVocabularyListAsync(vocabularyList);
    }

    public async Task ClearAsync(bool resetAutoIncrementId = true)
    {
        vocabularyList.Clear();
        await repository.ReplaceVocabularyListAsync(vocabularyList, resetAutoIncrementId);
    }

    public async Task AddRangeAsync(IEnumerable<VocabularyItem> items)
    {
        var vocabularyItems = items.ToList();
        foreach (var item in vocabularyItems)
        {
            SetAnkiGuid(item);
        }
        vocabularyList.AddRange(vocabularyItems);
        await repository.ReplaceVocabularyListAsync(vocabularyList);
    }

    public async Task RemoveAtAsync(int index)
    {
        vocabularyList.RemoveAt(index);
        await repository.ReplaceVocabularyListAsync(vocabularyList);
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
        await repository.ReplaceVocabularyListAsync(vocabularyList);
    }

    private static void SetAnkiGuid(VocabularyItem item)
    {
        item.AnkiGuid ??= Guid.NewGuid().ToString("N");
    }
}