using System.Windows;
using System.Windows.Controls;

namespace JishoTangoAssistant
{
    /// <summary>
    /// Interaktionslogik für VocabularyListView.xaml
    /// </summary>
    public partial class VocabularyListView : UserControl
    {
        private VocabularyListViewModel _vocabularyListViewModel;

        public VocabularyListViewModel VocabularyListViewModel { get; set; }
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
