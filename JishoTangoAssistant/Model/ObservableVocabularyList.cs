using JishoTangoAssistant.Model.ListOperation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace JishoTangoAssistant.Model
{
    public partial class ObservableVocabularyList : IList<VocabularyItem>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private List<VocabularyItem> _vocabularyList;
        private Dictionary<string, List<VocabularyItem>> _vocabularyDictionary; // word -> vocab item
        private Stack<ListOperation<VocabularyItem>> _undoOperationStack;

        private bool _suppressNotification = false;

        private const string CountString = "Count";
        private const string IndexerName = "Item[]";

        public ObservableVocabularyList()
        {
            _vocabularyList = new List<VocabularyItem>();
            _vocabularyDictionary = new Dictionary<string, List<VocabularyItem>>();
            _undoOperationStack = new Stack<ListOperation<VocabularyItem>>();
        }

        #region methods-interfaces
        public int Count => _vocabularyList.Count;

        public bool IsReadOnly => ((ICollection<VocabularyItem>)_vocabularyList).IsReadOnly;

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
                return _vocabularyList[index];
            }
            set
            {
                Replace(value, index, true);
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            var handler = CollectionChanged;
            if (handler != null && !_suppressNotification)
                handler(this, args);
        }

        public int IndexOf(VocabularyItem item)
        {
            return _vocabularyList.IndexOf(item);
        }

        public void Insert(int index, VocabularyItem item)
        {
            this.Insert(index, item, true);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index, true);
        }

        public void Add(VocabularyItem item)
        {
            this.Add(item, true);
        }

        public void Clear()
        {
            this.Clear(false);
        }

        public bool Contains(VocabularyItem item)
        {
            bool containsItem = _vocabularyDictionary.ContainsKey(item.Word) && _vocabularyDictionary[item.Word].Contains(item);
            Debug.Assert(_vocabularyList.Contains(item) == containsItem);
            return _vocabularyDictionary.ContainsKey(item.Word) && _vocabularyDictionary[item.Word].Contains(item);
        }

        public void CopyTo(VocabularyItem[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex, true);
        }

        public bool Remove(VocabularyItem item)
        {
            return this.Remove(item, true);
        }

        public IEnumerator<VocabularyItem> GetEnumerator()
        {
            return _vocabularyList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region list-operations

        private bool Remove(VocabularyItem item, bool pushListOperationToUndoStack)
        {
            int index = _vocabularyList.FindIndex(x => x.Equals(item));
            bool wasInList = index > -1;
            if (wasInList)
            {
                if (pushListOperationToUndoStack)
                    _undoOperationStack.Push(new RemoveListOperation<VocabularyItem>(item, index));
                _vocabularyList.RemoveAt(index);
            }

            List<VocabularyItem>? wordList;
            bool containedInDictionary = _vocabularyDictionary.TryGetValue(item.Word, out wordList);
            if (wordList != null && containedInDictionary)
                wordList.Remove(item);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return wasInList;
        }

        private void CopyTo(VocabularyItem[] array, int arrayIndex, bool pushListOperationToUndoStack)
        {
            var replacedItems = _vocabularyList.GetRange(arrayIndex, array.Length);
            if (pushListOperationToUndoStack)
                _undoOperationStack.Push(new CopyToOperation<VocabularyItem>(replacedItems, arrayIndex));
            _vocabularyList.CopyTo(array, arrayIndex);

            for (int i = arrayIndex; i < array.Length; i++)
            {
                var item = array[i];
                _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
                _vocabularyDictionary[item.Word].Add(item);
            }
        }

        private void Clear(bool pushListOperationToUndoStack = false)
        {
            if (pushListOperationToUndoStack)
                _undoOperationStack.Push(new ClearListOperation<VocabularyItem>(new List<VocabularyItem>(this)));
            _vocabularyList.Clear();

            _vocabularyDictionary.Clear();

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void Add(VocabularyItem item, bool pushListOperationToUndoStack)
        {
            if (pushListOperationToUndoStack)
                _undoOperationStack.Push(new AddListOperation<VocabularyItem>());
            _vocabularyList.Add(item);

            _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
            _vocabularyDictionary[item.Word].Add(item);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        private void RemoveAt(int index, bool pushListOperationToUndoStack)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("index is out of range");
            var oldItem = _vocabularyList[index];
            if (pushListOperationToUndoStack)
                _undoOperationStack.Push(new RemoveAtListOperation<VocabularyItem>(index, oldItem));
            _vocabularyList.RemoveAt(index);

            List<VocabularyItem>? wordList = null;
            bool containedInDictionary = _vocabularyDictionary.TryGetValue(oldItem.Word, out wordList);
            if (wordList != null && containedInDictionary)
                wordList.Remove(oldItem);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
        }

        private void Insert(int index, VocabularyItem item, bool pushListOperationToUndoStack)
        {
            if (pushListOperationToUndoStack)
                _undoOperationStack.Push(new InsertListOperation<VocabularyItem>(index));
            _vocabularyList.Insert(index, item);

            _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
            _vocabularyDictionary[item.Word].Add(item);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        private void Replace(VocabularyItem toReplace, int index, bool pushListOperationToUndoStack)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("index is out of range");
            var oldItem = _vocabularyList[index];
            if (pushListOperationToUndoStack)
                _undoOperationStack.Push(new AssignmentListOperation<VocabularyItem>(index, oldItem));
            _vocabularyList[index] = toReplace;
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, toReplace, oldItem, index));
        }

        #endregion

        #region undo-actions

        public void Undo()
        {
            if (this._undoOperationStack.Count > 0)
            {
                var operation = this._undoOperationStack.Pop();
                if (operation == null)
                    return;

                if (operation is AssignmentListOperation<VocabularyItem>)
                {
                    var op = (AssignmentListOperation<VocabularyItem>)operation;
                    UndoAssignment(op.index, op.replacedItem);
                }
                else if (operation is InsertListOperation<VocabularyItem>)
                {
                    var op = (InsertListOperation<VocabularyItem>)operation;
                    UndoInsert(op.index);
                }
                else if (operation is RemoveAtListOperation<VocabularyItem>)
                {
                    var op = (RemoveAtListOperation<VocabularyItem>)operation;
                    UndoRemoveAt(op.index, op.removedItem);
                }
                else if (operation is AddListOperation<VocabularyItem>)
                {
                    var op = (AddListOperation<VocabularyItem>)operation;
                    UndoAdd();
                }
                else if (operation is ClearListOperation<VocabularyItem>)
                {
                    var op = (ClearListOperation<VocabularyItem>)operation;
                    UndoClear(op.copy);
                }
                else if (operation is CopyToOperation<VocabularyItem>)
                {
                    var op = (CopyToOperation<VocabularyItem>)operation;
                    UndoCopyTo(op.replacedItems, op.arrayIndex);
                }
                else if (operation is RemoveListOperation<VocabularyItem>)
                {
                    var op = (RemoveListOperation<VocabularyItem>)operation;
                    UndoRemove(op.removedItem, op.index);
                }
            }
        }

        private void UndoAssignment(int index, VocabularyItem replacedItem)
        {
            this.Replace(replacedItem, index, false);
        }

        private void UndoInsert(int index)
        {
            this.RemoveAt(index, false);
        }

        private void UndoRemoveAt(int index, VocabularyItem removedItem)
        {
            this.Insert(index, removedItem, false);
        }

        private void UndoAdd()
        {
            this.RemoveAt(this.Count - 1, false);
        }

        private void UndoClear(IList<VocabularyItem> copy)
        {
            VocabularyItem[] copyArray = new VocabularyItem[copy.Count];
            copy.CopyTo(copyArray, 0);
            
            this.CopyTo(copyArray, 0, false);
            _vocabularyList.Clear();
            _vocabularyDictionary.Clear();
        }

        private void UndoCopyTo(IList<VocabularyItem> replacedItems, int arrayIndex)
        {
            VocabularyItem[] replacedItemsArray = new VocabularyItem[replacedItems.Count];
            this.CopyTo(replacedItemsArray, arrayIndex, false);
        }

        private void UndoRemove(VocabularyItem removedItem, int index)
        {
            this.Insert(index, removedItem, false);
        }

        #endregion

        public bool ContainsWord(string word)
        {
            return _vocabularyDictionary.ContainsKey(word) && _vocabularyDictionary[word].Count > 0;
        }

        public void AddRange(IEnumerable<VocabularyItem> items)
        {
            if (items == null)
                throw new ArgumentNullException($"{nameof(items)} is null");

            _suppressNotification = true;

            foreach (var item in items)
            {
                _vocabularyList.Add(item);
                _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
                _vocabularyDictionary[item.Word].Add(item);
            }

            _suppressNotification = false;

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
