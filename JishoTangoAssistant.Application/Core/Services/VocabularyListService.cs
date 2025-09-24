using Avalonia.Threading;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Domain.Core.Collections;
using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Domain.Models.Core.Collections;
using JishoTangoAssistant.Repositories;
using JishoTangoAssistant.Shared.Constants;

namespace JishoTangoAssistant.Application.Core.Services;

public class VocabularyListService : IVocabularyListService
{
    private readonly IVocabularyListRepository repository;
    private readonly ReadOnlyObservableVocabularyList readOnlyVocabularyList;
    private readonly ObservableVocabularyList vocabularyList;
    private readonly DispatcherTimer dataPersistenceTimer;

    public VocabularyListService(IVocabularyListRepository repository)
    {
        this.repository = repository;
        var items = repository.GetVocabularyItems();
        vocabularyList = new ObservableVocabularyList(items);
        readOnlyVocabularyList = new ReadOnlyObservableVocabularyList(vocabularyList);
        dataPersistenceTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(Constants.DataPersistenceTimerInterval)
        };
        dataPersistenceTimer.Tick += ReplaceVocabularyListHandlerAsync;
    }

    public ReadOnlyObservableVocabularyList GetList() => readOnlyVocabularyList;

    public async Task AddAsync(VocabularyItem item)
    {
        SetAnkiGuid(item);
        vocabularyList.Add(item);
        await ReplaceVocabularyList();
    }

    public bool ContainsWord(string word) => vocabularyList.ContainsWord(word);

    public bool Contains(VocabularyItem item) => vocabularyList.Contains(item);

    public bool SequenceEqual(IEnumerable<VocabularyItem> items) => vocabularyList.SequenceEqual(items);

    public int Count() => vocabularyList.Count;

    public async Task UndoAsync()
    {
        vocabularyList.Undo();
        await ReplaceVocabularyList();
    }

    public async Task ClearAsync(bool resetAutoIncrementId = true)
    {
        vocabularyList.Clear();
        // TODO respect timer
        await repository.ReplaceVocabularyListAsync(vocabularyList, resetAutoIncrementId);
    }

    public async Task AddRangeAsync(IEnumerable<VocabularyItem> items, bool removeInfoAboutLastAddedItem = false)
    {
        var vocabularyItems = items.ToList();
        foreach (var item in vocabularyItems)
        {
            SetAnkiGuid(item);
        }
        vocabularyList.AddRange(vocabularyItems, removeInfoAboutLastAddedItem);
        await ReplaceVocabularyList();
    }

    public async Task RemoveAtAsync(int index)
    {
        vocabularyList.RemoveAt(index);
        await ReplaceVocabularyList();
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
        await ReplaceVocabularyList();
    }

    private async void ReplaceVocabularyListHandlerAsync(object? sender, EventArgs e)
    {
        dataPersistenceTimer.Stop();
        await repository.ReplaceVocabularyListAsync(vocabularyList);
    }

    private ValueTask ReplaceVocabularyList()
    {
        dataPersistenceTimer.Stop();
        dataPersistenceTimer.Start();
        return ValueTask.CompletedTask;
    }

    private static void SetAnkiGuid(VocabularyItem item) => item.AnkiGuid ??= Guid.NewGuid().ToString("N");
}