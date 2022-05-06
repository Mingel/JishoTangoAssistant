using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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


        private void vocabularyItemsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (vocabularyItemsDataGrid.Columns.Count <= 0)
                return;

            if (1 < vocabularyItemsDataGrid.Columns.Count)
                vocabularyItemsDataGrid.Columns[1].Visibility = Visibility.Hidden;

            vocabularyItemsDataGrid.Columns[0].MinWidth = 100;
            vocabularyItemsDataGrid.Columns[2].MinWidth = 100;
            vocabularyItemsDataGrid.Columns[0].Width = 100;
            vocabularyItemsDataGrid.Columns[2].Width = 100;
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
