using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.Views;

public partial class VocabularyListView : UserControl
{
    public VocabularyListView()
    {
        InitializeComponent();
    }

    private void upButton_Click(object sender, RoutedEventArgs e)
    {
        VocabularyItemsDataGrid.Focus();
    }

    private void downButton_Click(object sender, RoutedEventArgs e)
    {
        VocabularyItemsDataGrid.Focus();
    }
}