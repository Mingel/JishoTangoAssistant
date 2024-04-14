using Avalonia.Controls;
using Avalonia.Interactivity;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View;

/// <summary>
/// Interaction Logic for JapaneseUserInputView.xaml
/// </summary>
public partial class JapaneseUserInputView : UserControl
{
    public JapaneseUserInputView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        var japaneseUserInputViewModel = new JapaneseUserInputViewModel();
        DataContext = japaneseUserInputViewModel;
    }
    
    private void OnLoaded(object? sender, RoutedEventArgs? e)
    {
        InputTextBox.Focus();
    }
}