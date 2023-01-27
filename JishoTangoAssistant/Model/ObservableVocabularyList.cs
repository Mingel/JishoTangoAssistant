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

        private bool _suppressNotification = false;

        private const string CountString = "Count";
        private const string IndexerName = "Item[]";

        #region methods-interfaces
        public int Count => _vocabularyList.Count;

        public bool IsReadOnly => ((ICollection<VocabularyItem>)_vocabularyList).IsReadOnly;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
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

        public ObservableVocabularyList()
        {
            _vocabularyList = new List<VocabularyItem>();
            _vocabularyDictionary = new Dictionary<string, List<VocabularyItem>>();
        }

        public VocabularyItem this[int index]
        {
            get
            {
                return _vocabularyList[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException("index is out of range");
                var oldItem = _vocabularyList[index];
                _vocabularyList[index] = value;
                OnPropertyChanged(CountString);
                OnPropertyChanged(IndexerName);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
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
            _vocabularyList.Insert(index, item);

            _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
            _vocabularyDictionary[item.Word].Add(item);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("index is out of range");
            var oldItem = _vocabularyList[index];
            _vocabularyList.RemoveAt(index);

            List<VocabularyItem> wordList;
            bool containedInDictionary = _vocabularyDictionary.TryGetValue(oldItem.Word, out wordList);
            if (containedInDictionary)
                wordList.Remove(oldItem);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
        }

        public void Add(VocabularyItem item)
        {
            _vocabularyList.Add(item);

            _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
            _vocabularyDictionary[item.Word].Add(item);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            _vocabularyList.Clear();

            _vocabularyDictionary.Clear();

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(VocabularyItem item)
        {
            bool containsItem = _vocabularyDictionary.ContainsKey(item.Word) && _vocabularyDictionary[item.Word].Contains(item);
            Debug.Assert(_vocabularyList.Contains(item) == containsItem);
            return _vocabularyDictionary.ContainsKey(item.Word) && _vocabularyDictionary[item.Word].Contains(item);
        }

        public void CopyTo(VocabularyItem[] array, int arrayIndex)
        {
            _vocabularyList.CopyTo(array, arrayIndex);

            for (int i = arrayIndex; i < array.Length; i++)
            {
                var item = array[i];
                _vocabularyDictionary.TryAdd(item.Word, new List<VocabularyItem>());
                _vocabularyDictionary[item.Word].Add(item);
            }
        }

        public bool Remove(VocabularyItem item)
        {
            var wasInList = _vocabularyList.Remove(item);

            List<VocabularyItem> wordList;
            bool containedInDictionary = _vocabularyDictionary.TryGetValue(item.Word, out wordList);
            if (containedInDictionary)
                wordList.Remove(item);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return wasInList;
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
