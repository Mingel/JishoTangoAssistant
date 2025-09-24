using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JishoTangoAssistant.Domain.Models.Common.Collections;

public class ObservableRangeCollection<T> : ObservableCollection<T>
{
    public void ReplaceAll(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        var oldItems = new List<T>(Items);
        Items.Clear();
        var itemList = items.ToList();
        foreach (var item in itemList)
        {
            Items.Add(item);
        }
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, itemList, oldItems, 0));
    }
}