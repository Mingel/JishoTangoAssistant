using JishoTangoAssistant.Model.ListOperation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace JishoTangoAssistant.Model;

public partial class ObservableVocabularyList : IList<VocabularyItem>, INotifyCollectionChanged, INotifyPropertyChanged
{
    private List<VocabularyItem> vocabularyList;
    private Dictionary<string, List<VocabularyItem>> vocabularyDictionary; // word -> vocab item
    private Stack<ListOperation<VocabularyItem>> undoOperationStack;

    private bool suppressNotification = false;

    private const string CountString = "Count";
    private const string IndexerName = "Item[]";

    public ObservableVocabularyList()
    {
        vocabularyList = new List<VocabularyItem>();
        vocabularyDictionary = new Dictionary<string, List<VocabularyItem>>();
        undoOperationStack = new Stack<ListOperation<VocabularyItem>>();
    }

    #region methods-interfaces
    public int Count => vocabularyList.Count;

    public bool IsReadOnly => ((ICollection<VocabularyItem>)vocabularyList).IsReadOnly;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    protected virtual event PropertyChangedEventHandler? PropertyChanged;

    event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
    {
        add
        {
            PropertyChanged += value;
        }
        remove
        {
            PropertyChanged -= value;
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, e);
        }
    }

    public VocabularyItem this[int index]
    {
        get
        {
            return vocabularyList[index];
        }
        set
        {
            Replace(value, index, true);
        }
    }

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        var handler = CollectionChanged;
        if (handler != null && !suppressNotification)
            handler(this, args);
    }

    public int IndexOf(VocabularyItem item)
    {
        return vocabularyList.IndexOf(item);
    }

    public void Insert(int index, VocabularyItem item)
    {
        Insert(index, item, true);
    }

    public void RemoveAt(int index)
    {
        RemoveAt(index, true);
    }

    public void Add(VocabularyItem item)
    {
        Add(item, true);
    }

    public void Clear()
    {
        Clear(false);
    }

    public bool Contains(VocabularyItem item)
    {
        bool containsItem = vocabularyDictionary.ContainsKey(item.Word) && vocabularyDictionary[item.Word].Contains(item);
        Debug.Assert(vocabularyList.Contains(item) == containsItem);
        return vocabularyDictionary.ContainsKey(item.Word) && vocabularyDictionary[item.Word].Contains(item);
    }

    public void CopyTo(VocabularyItem[] array, int arrayIndex)
    {
        CopyTo(array, arrayIndex, true);
    }

    public bool Remove(VocabularyItem item)
    {
        return Remove(item, true);
    }

    public IEnumerator<VocabularyItem> GetEnumerator()
    {
        return vocabularyList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region list-operations

    private bool Remove(VocabularyItem item, bool pushListOperationToUndoStack)
    {
        int index = vocabularyList.FindIndex(x => x.Equals(item));
        bool wasInList = index > -1;
        if (wasInList)
        {
            if (pushListOperationToUndoStack)
                undoOperationStack.Push(new RemoveListOperation<VocabularyItem>(item, index));
            vocabularyList.RemoveAt(index);
        }

        List<VocabularyItem>? wordList;
        bool containedInDictionary = vocabularyDictionary.TryGetValue(item.Word, out wordList);
        if (wordList != null && containedInDictionary)
            wordList.Remove(item);

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        return wasInList;
    }

    private void CopyTo(VocabularyItem[] array, int arrayIndex, bool pushListOperationToUndoStack)
    {
        var replacedItems = vocabularyList.GetRange(arrayIndex, array.Length);
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new CopyToOperation<VocabularyItem>(replacedItems, arrayIndex));
        vocabularyList.CopyTo(array, arrayIndex);

        for (int i = arrayIndex; i < array.Length; i++)
        {
            var item = array[i];
            vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
            vocabularyDictionary[item.Word].Add(item);
        }
    }

    private void Clear(bool pushListOperationToUndoStack = false)
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
            undoOperationStack.Push(new AddListOperation<VocabularyItem>());
        vocabularyList.Add(item);

        vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
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

        List<VocabularyItem>? wordList = null;
        bool containedInDictionary = vocabularyDictionary.TryGetValue(oldItem.Word, out wordList);
        if (wordList != null && containedInDictionary)
            wordList.Remove(oldItem);

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
    }

    private void Insert(int index, VocabularyItem item, bool pushListOperationToUndoStack)
    {
        if (pushListOperationToUndoStack)
            undoOperationStack.Push(new InsertListOperation<VocabularyItem>(index));
        vocabularyList.Insert(index, item);

        vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
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
        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, toReplace, oldItem, index));
    }

    #endregion

    #region undo-actions

    public void Undo()
    {
        if (undoOperationStack.Count > 0)
        {
            var operation = undoOperationStack.Pop();
            if (operation == null)
                return;

            switch (operation)
            {
                case AssignmentListOperation<VocabularyItem> op:
                    UndoAssignment(op.index, op.replacedItem);
                    break;
                case InsertListOperation<VocabularyItem> op:
                    UndoInsert(op.index);
                    break;
                case RemoveAtListOperation<VocabularyItem> op:
                    UndoRemoveAt(op.index, op.removedItem);
                    break;
                case AddListOperation<VocabularyItem> _:
                    UndoAdd();
                    break;
                case ClearListOperation<VocabularyItem> op:
                    UndoClear(op.copy);
                    break;
                case CopyToOperation<VocabularyItem> op:
                    UndoCopyTo(op.replacedItems, op.arrayIndex);
                    break;
                case RemoveListOperation<VocabularyItem> op:
                    UndoRemove(op.removedItem, op.index);
                    break;
            }
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

    private void UndoClear(IList<VocabularyItem> copy)
    {
        VocabularyItem[] copyArray = new VocabularyItem[copy.Count];
        copy.CopyTo(copyArray, 0);
        
        CopyTo(copyArray, 0, false);
        vocabularyList.Clear();
        vocabularyDictionary.Clear();
    }

    private void UndoCopyTo(IList<VocabularyItem> replacedItems, int arrayIndex)
    {
        VocabularyItem[] replacedItemsArray = new VocabularyItem[replacedItems.Count];
        CopyTo(replacedItemsArray, arrayIndex, false);
    }

    private void UndoRemove(VocabularyItem removedItem, int index)
    {
        Insert(index, removedItem, false);
    }

    #endregion

    public bool ContainsWord(string word)
    {
        return vocabularyDictionary.ContainsKey(word) && vocabularyDictionary[word].Count > 0;
    }

    public void AddRange(IEnumerable<VocabularyItem> items)
    {
        if (items == null)
            throw new ArgumentNullException($"{nameof(items)} is null");

        suppressNotification = true;

        foreach (var item in items)
        {
            vocabularyList.Add(item);
            vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
            vocabularyDictionary[item.Word].Add(item);
        }

        suppressNotification = false;

        OnPropertyChanged(CountString);
        OnPropertyChanged(IndexerName);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
