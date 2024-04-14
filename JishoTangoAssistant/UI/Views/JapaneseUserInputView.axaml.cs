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
        Loaded += OnLoaded;
    }
    
    private void OnLoaded(object? sender, RoutedEventArgs? e)
    {
        InputTextBox.Focus();
    }
}