using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.Views.JapaneseUserInputViews;

public partial class WordSearchView : UserControl, IRecipient<FocusInputTextBoxMessage>
{
    public WordSearchView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register(this);
    }

    private void RemovePreEnteredInputsButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        InputTextBox.Focus();
    }

    public void Receive(FocusInputTextBoxMessage message)
    {
        InputTextBox.Focus();
    }
}