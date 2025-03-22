using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.Views.JapaneseUserInputViews;

public partial class PreEnteredInputView : UserControl
{
    public PreEnteredInputView()
    {
        InitializeComponent();
    }
    
    private void BackToUserInputButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        PreEnteredInputListTextBox.Focus();
    }
}