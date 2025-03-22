using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.Views.JapaneseUserInputViews;

public partial class WordSearchView : UserControl
{
    public WordSearchView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<FocusInputTextBoxMessage>(this, (_, _) =>
        {
            InputTextBox.Focus();
        });
    }

    private void RemovePreEnteredInputsButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        InputTextBox.Focus();
    }
}