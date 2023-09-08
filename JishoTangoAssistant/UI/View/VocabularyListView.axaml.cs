using Avalonia.Controls;
using Avalonia.Interactivity;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View
{
    public partial class VocabularyListView : UserControl
    {
        private VocabularyListViewModel _vocabularyListViewModel;

        public VocabularyListView()
        {
            InitializeComponent();
            _vocabularyListViewModel = new VocabularyListViewModel();
            DataContext = _vocabularyListViewModel;
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            vocabularyItemsDataGrid.Focus();
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            vocabularyItemsDataGrid.Focus();
        }
    }
}
