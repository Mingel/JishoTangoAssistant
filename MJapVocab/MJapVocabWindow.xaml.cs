
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MJapVocab
{
    /// <summary>
    /// Interaction logic for MJapVocabWindow.xaml
    /// </summary>
    public partial class MJapVocabWindow : Window
    {
        MJapVocabViewModel viewModel;

        public MJapVocabWindow()
        {
            viewModel = new MJapVocabViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "MJV Files (*.mjv)|*.mjv";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    var loadedVocabularyItems = VocabularyItemHandler.JsonToList(fileContent);
                    CurrentSession.addedVocabularyItems.Clear();
                    CurrentSession.addedVocabularyItems.AddRange(loadedVocabularyItems);
                    CurrentSession.userMadeChanges = false;
                    //UpdateVocabularyItemsGridView();
                    //vocabularyItemsGridView.ClearSelection();
                    vocabularyItemsDataGrid.Items.Refresh();
                }
            }*/
            //vocabularyItemsDataGrid.Items.Refresh();
        }
    }
}
