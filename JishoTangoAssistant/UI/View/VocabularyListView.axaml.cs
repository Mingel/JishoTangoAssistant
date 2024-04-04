using Avalonia.Controls;
using Avalonia.Interactivity;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View;

public partial class VocabularyListView : UserControl
{
    public VocabularyListView()
    {
        InitializeComponent();
        var vocabularyListViewModel = new VocabularyListViewModel();
        DataContext = vocabularyListViewModel;
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