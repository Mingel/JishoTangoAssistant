using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.Views;

public partial class VocabularyListView : UserControl
{
    public VocabularyListView()
    {
        InitializeComponent();

        KeyDown += KeyDownHandler;
        KeyUp += KeyUpHandler;
    }
    
    private void KeyDownHandler(object? _, KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
        {
            DeleteSelectionButton.IsVisible = false;
            DeleteAllButton.IsVisible = true;
        }
    }

    private void KeyUpHandler(object? _, KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
        {
            DeleteSelectionButton.IsVisible = true;
            DeleteAllButton.IsVisible = false;
        }
    }

    private void UpButtonClickHandler(object sender, RoutedEventArgs e)
    {
        VocabularyItemsDataGrid.Focus();
    }

    private void DownButtonClickHandler(object sender, RoutedEventArgs e)
    {
        VocabularyItemsDataGrid.Focus();
    }
}