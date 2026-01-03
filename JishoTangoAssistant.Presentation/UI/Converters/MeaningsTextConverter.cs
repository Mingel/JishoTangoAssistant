using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace JishoTangoAssistant.Presentation.UI.Converters;

public class MeaningsTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not List<IEnumerable<string>> meaningGroups)
            return string.Empty;
        
        var similarMeaningsGroupText = meaningGroups.Select(m => string.Join("; ", m));
        return string.Join(Environment.NewLine, similarMeaningsGroupText);

    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string meaningsText) 
            return new List<IEnumerable<string>>();
        
        var similarMeaningsGroups = meaningsText.Split(Environment.NewLine);
        return similarMeaningsGroups.Select(g => g.Split("; ")).ToList();

    }
}