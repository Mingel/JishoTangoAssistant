using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace JishoTangoAssistant.Domain.Models.Common.Collections;

public sealed class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
{
    public TrulyObservableCollection()
    {
        CollectionChanged += TrulyObservableCollectionCollectionChanged;
    }

    public event PropertyChangedEventHandler? CollectionItemChanged;
    
    private void TrulyObservableCollectionCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (INotifyPropertyChanged item in e.NewItems)
            {
                item.PropertyChanged += ItemPropertyChanged;
            }
        }
        if (e.OldItems != null)
        {
            foreach (INotifyPropertyChanged item in e.OldItems)
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }
        }
    }

    private void ItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {            
        CollectionItemChanged?.Invoke(sender, e);
    }
}