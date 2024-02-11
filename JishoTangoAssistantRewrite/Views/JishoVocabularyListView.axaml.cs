using Avalonia.Controls;
using Avalonia.Interactivity;

namespace JishoTangoAssistantRewrite.Views
{
    public partial class JishoVocabularyListView : UserControl
    {
        public JishoVocabularyListView()
        {
            InitializeComponent();
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
