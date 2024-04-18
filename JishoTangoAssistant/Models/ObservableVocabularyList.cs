using JishoTangoAssistant.Models.ListOperation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace JishoTangoAssistant.Models;

public sealed class ObservableVocabularyList : IList<VocabularyItem>, INotifyCollectionChanged, INotifyPropertyChanged
{
    private readonly List<VocabularyItem> vocabularyList = [];
    private readonly Dictionary<string, List<VocabularyItem>> vocabularyDictionary = new(); // word -> vocab item
    private readonly Stack<ListOperation.ListOperation> undoOperationStack = new();

    private const string CountString = "Count";
    private const string IndexerName = "Item[]";

    public ObservableVocabularyList() { }
    
    public ObservableVocabularyList(IEnumerable<VocabularyItem> items)
    {
        AddRange(items);
    }
    
    #region methods-interfaces
    public int Count => vocabularyList.Count;

    public bool IsReadOnly => ((ICollection<VocabularyItem>)vocabularyList).IsReadOnly;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
    {
        add => PropertyChanged += value;
        remove => PropertyChanged -= value;
    }

    private void OnPropertyChanged(string propertyName) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

    private void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

    public VocabularyItem this[int index]
    {
        get => vocabularyList[index];
        set => Replace(value, index, true);
    }

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this, args);
    }

    public int IndexOf(VocabularyItem item) => vocabularyList.IndexOf(item);

    public void Insert(int index, VocabularyItem item) => Insert(index, item, true);

    public void RemoveAt(int index) => RemoveAt(index, true);

    public void Add(VocabularyItem item) => Add(item, true);

    public void Clear() => Clear(true);

    public void AddRange(IEnumerable<VocabularyItem> items) => AddRange(items, true);

    public bool Contains(VocabularyItem item)
    {
        var containsItem = vocabularyDictionary.ContainsKey(item.Word) && vocabularyDictionary[item.Word].Contains(item);
        Debug.Assert(vocabularyList.Contains(item) == containsItem);
        return vocabularyDictionary.ContainsKey(item.Word) && vocabularyDictionary[item.Word].Contains(item);
    }
    
    public bool Remove(VocabularyItem item) => Remove(item, true);

    public IEnumerator<VocabularyItem> GetEnumerator() => vocabularyList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region list-operations

    private bool Remove(VocabularyItem item, bool pushListOperationToUndoStack)
    {
        var index = vocabularyList.FindIndex(x => x.Equals(item));
        var wasInList = index > -1;
        if (wasInList)
        {
            if (pushListOperationToUndoStack)
                undoOperationStack.Push(new RemoveListOperation<VocabularyItem>(item, index));
            vocabularyList.RemoveAt(index);
            DecrementOrderStartingFrom(index);
        }

        var containedInDictionary = vocabularyDictionary.TryGetValue(item.Word, out var wordList);
        if (wordList != null && containedInDictionary)
            wordList.Remove(item);

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        return wasInList;
    }

    public void CopyTo(VocabularyItem[] array, int arrayIndex) => vocabularyList.CopyTo(array, arrayIndex);

    private void Clear(bool pushListOperationToUndoStack)
    {
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new ClearListOperation<VocabularyItem>(new List<VocabularyItem>(this)));
        vocabularyList.Clear();

        vocabularyDictionary.Clear();

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void Add(VocabularyItem item, bool pushListOperationToUndoStack)
    {
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new AddListOperation());
        vocabularyList.Add(item);
        
        item.Order = vocabularyList.Count;

        vocabularyDictionary.TryAdd(item.Word, []);
        vocabularyDictionary[item.Word].Add(item);

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
    }

    private void RemoveAt(int index, bool pushListOperationToUndoStack)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException("index is out of range");
        var oldItem = vocabularyList[index];
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new RemoveAtListOperation<VocabularyItem>(index, oldItem));
        vocabularyList.RemoveAt(index);
        
        DecrementOrderStartingFrom(index);

        var containedInDictionary = vocabularyDictionary.TryGetValue(oldItem.Word, out List<VocabularyItem>? wordList);
        if (wordList != null && containedInDictionary)
            wordList.Remove(oldItem);

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
    }

    private void Insert(int index, VocabularyItem item, bool pushListOperationToUndoStack)
    {
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new InsertListOperation(index));
        vocabularyList.Insert(index, item);
        
        item.Order = index;
        IncrementOrderStartingFrom(index + 1);

        vocabularyDictionary.TryAdd(item.Word, []);
        vocabularyDictionary[item.Word].Add(item);

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
    }

    private void Replace(VocabularyItem toReplace, int index, bool pushListOperationToUndoStack)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException("index is out of range");
        var oldItem = vocabularyList[index];
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new AssignmentListOperation<VocabularyItem>(index, oldItem));
        vocabularyList[index] = toReplace;
        toReplace.Order = index;
        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, toReplace, oldItem, index));
    }

    private void AddRange(IEnumerable<VocabularyItem> items, bool pushListOperationToUndoStack)
    {
        ArgumentNullException.ThrowIfNull(items);

        var vocabularyItems = items as VocabularyItem[] ?? items.ToArray();

        if (vocabularyItems.Length == 0)
            return;
        
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new AddRangeListOperation(vocabularyItems.Length));
        
        foreach (var item in vocabularyItems)
        { 
            vocabularyList.Add(item);
        
            item.Order = vocabularyList.Count;

            vocabularyDictionary.TryAdd(item.Word, []);
            vocabularyDictionary[item.Word].Add(item);
        }

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    #endregion

    #region undo-actions

    public void Undo()
    {
        if (undoOperationStack.Count <= 0) return;
        var operation = undoOperationStack.Pop();

        switch (operation)
        {
            case AssignmentListOperation<VocabularyItem> op:
                UndoAssignment(op.Index, op.ReplacedItem);
                break;
            case InsertListOperation op:
                UndoInsert(op.Index);
                break;
            case RemoveAtListOperation<VocabularyItem> op:
                UndoRemoveAt(op.Index, op.RemovedItem);
                break;
            case AddListOperation _:
                UndoAdd();
                break;
            case ClearListOperation<VocabularyItem> op:
                UndoClear(op.Copy);
                break;
            case RemoveListOperation<VocabularyItem> op:
                UndoRemove(op.RemovedItem, op.Index);
                break;
            case AddRangeListOperation op:
                UndoAddRange(op.ItemsLength);
                break;
        }
    }

    private void UndoAssignment(int index, VocabularyItem replacedItem)
    {
        Replace(replacedItem, index, false);
    }

    private void UndoInsert(int index)
    {
        RemoveAt(index, false);
    }

    private void UndoRemoveAt(int index, VocabularyItem removedItem)
    {
        Insert(index, removedItem, false);
    }

    private void UndoAdd()
    {
        RemoveAt(Count - 1, false);
    }

    private void UndoClear(ICollection<VocabularyItem> copy)
    {
        ArgumentNullException.ThrowIfNull(copy);
        var copyArray = new VocabularyItem[copy.Count];
        copy.CopyTo(copyArray, 0);
        
        CopyTo(copyArray, 0);
        
        vocabularyList.Clear();
        vocabularyDictionary.Clear();

        foreach (var item in copyArray)
        {
            vocabularyList.Add(item);
            item.Order = vocabularyList.Count;
            vocabularyDictionary.TryAdd(item.Word, []);
            vocabularyDictionary[item.Word].Add(item);
        }
        
        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void UndoRemove(VocabularyItem removedItem, int index)
    {
        Insert(index, removedItem, false);
    }
    
    private void UndoAddRange(int itemsLength)
    {
        for (var i = 0; i < itemsLength; i++)
        {
            RemoveAt(Count - 1, false);
        }
    }
    #endregion

    public bool ContainsWord(string word)
    {
        return vocabularyDictionary.ContainsKey(word) && vocabularyDictionary[word].Count > 0;
    }

    private void ChangeOrderStartingFrom(int startIndex, int increaseByValue)
    {
        var filteredItems = vocabularyList.Where(i => i.Order >= startIndex);
        foreach (var item in filteredItems)
        {
            item.Order += increaseByValue;
        }
    }

    private void IncrementOrderStartingFrom(int startIndex) => ChangeOrderStartingFrom(startIndex, 1);

    private void DecrementOrderStartingFrom(int startIndex) => ChangeOrderStartingFrom(startIndex, -1);

    private bool HasOrderIntegrity()
    {
        var orders = vocabularyList.Select(i => i.Order).ToArray();
        return orders.Min() == 1 && orders.Max() == vocabularyList.Count && orders.Distinct().Count() == vocabularyList.Count;
    }

    private void RestoreOrderIntegrity()
    {
        for (var i = 0; i < vocabularyList.Count; i++)
        {
            vocabularyList[i].Order = i;
        }
    }
}
