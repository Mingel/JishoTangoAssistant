using System.ComponentModel;

namespace JishoTangoAssistant.Domain.Models.Common.Data;

public sealed class AvailableMeaning(string value, int flattenedIndex) : INotifyPropertyChanged
{
    public string Value { get; } = value;
    public int FlattenedIndex { get; } = flattenedIndex; // one-based 

    private bool isEnabled;
    
    public bool IsEnabled
    {
        get => isEnabled;
        set
        {
            if (isEnabled == value) 
                return;
            
            isEnabled = value;
            OnPropertyChanged(nameof(IsEnabled));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
    {
        add => PropertyChanged += value;
        remove => PropertyChanged -= value;
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}