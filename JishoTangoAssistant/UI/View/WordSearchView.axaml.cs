using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.View;

public partial class WordSearchView : UserControl
{
    public WordSearchView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        InputTextBox.Focus();
    }
}