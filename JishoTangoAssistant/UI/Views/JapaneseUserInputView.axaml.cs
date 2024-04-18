using Avalonia.Controls;
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
    }
    
    private void LoadedHandler(object? sender, RoutedEventArgs? e)
    {
        InputTextBox.Focus();
    }
}