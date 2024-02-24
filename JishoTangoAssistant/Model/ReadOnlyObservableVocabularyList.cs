using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace JishoTangoAssistant.Model;

public class ReadOnlyObservableVocabularyList : IReadOnlyList<VocabularyItem>, INotifyCollectionChanged, INotifyPropertyChanged
{
    private readonly ObservableVocabularyList vocabularyList;

    public ReadOnlyObservableVocabularyList(ObservableVocabularyList vocabularyList)
    {
        this.vocabularyList = vocabularyList;
        Count = vocabularyList.Count;
        vocabularyList.CollectionChanged += HandleCollectionChanged;
        vocabularyList.PropertyChanged += HandlePropertyChanged;
    }
    
    public int Count { get; }
    
    
    public VocabularyItem this[int index] => vocabularyList[index];
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    private event PropertyChangedEventHandler? PropertyChanged;

    event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
    {
        add => PropertyChanged += value;
        remove => PropertyChanged -= value;
    }

    public IEnumerator<VocabularyItem> GetEnumerator()
    {
        return vocabularyList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void OnPropertyChanged(string propertyName) => OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

    private void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
    
    private void HandleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnCollectionChanged(e);
    }

    private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(e);
    }
    
    private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this, args);
    }
}