using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Input;

namespace JishoTangoAssistant.Presentation.UI.Converters;

public class FlattenedIndexToCheckBoxHotKeyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is int index and >= 0 and < 9 ? new KeyGesture(Key.D1 + index, KeyModifiers.Control) : null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is KeyGesture { KeyModifiers: KeyModifiers.Control, Key: >= Key.D1 and <= Key.D9 } gesture)
            return gesture.Key - Key.D1;

        return null;
    }
}