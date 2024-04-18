using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.Views;

public partial class VocabularyListView : UserControl
{
    public VocabularyListView()
    {
        InitializeComponent();
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