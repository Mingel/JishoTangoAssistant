﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace JishoTangoAssistant
{
    public class FontSizeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value < 0 ? String.Empty : value.ToString();
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret = 0;
            return int.TryParse((string)value, out ret) ? ret : -1;
        }
    }
}
