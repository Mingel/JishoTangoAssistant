using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.Views;

/// <summary>
/// Interaction Logic for JapaneseUserInputView.xaml
/// </summary>
public partial class JapaneseUserInputView : UserControl
{
    public JapaneseUserInputView()
    {
        InitializeComponent();
        Loaded += LoadedHandler;
        KeyUp += KeyUpHandler;
    }
    
    private void LoadedHandler(object? sender, RoutedEventArgs? e)
    {
        InputTextBox.Focus();
    }

    private void AddToVocabularyListClickHandler(object? sender, RoutedEventArgs e)
    {
        FocusInputTextBox();
    }
    
    private void KeyUpHandler(object? sender, KeyEventArgs e)
    {
        if (AddButton.IsEnabled &&
            AddButton.HotKey != null &&
            AddButton.HotKey.KeyModifiers == e.KeyModifiers &&
            AddButton.HotKey.Key == e.Key)
        {
            FocusInputTextBox();
        }
    }

    private void FocusInputTextBox()
    {
        InputTextBox.Focus();
    }
}