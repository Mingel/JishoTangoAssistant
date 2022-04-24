using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MJapVocab
{
    public class MJapVocabViewModel : MJapVocabViewModelBase
    {
        private readonly DelegateCommand loadListCommand;
        private readonly DelegateCommand saveListCommand;
        private readonly DelegateCommand exportCsvJapToEngCommand;
        private readonly DelegateCommand exportCsvEngToJapCommand;

        public ICommand SaveListCommand => saveListCommand;
        public ICommand LoadListCommand => loadListCommand;
        public ICommand ExportCsvJapToEngCommand => exportCsvJapToEngCommand;
        public ICommand ExportCsvEngToJapCommand => exportCsvEngToJapCommand;

        public MJapVocabViewModel() {

            loadListCommand = new DelegateCommand(OnLoadList, _ => true);
            saveListCommand = new DelegateCommand(OnSaveList, _ => true);
            exportCsvJapToEngCommand = new DelegateCommand(OnExportCsvJapToEng, _ => true);
            exportCsvEngToJapCommand = new DelegateCommand(OnExportCsvEngToJap, _ => true);
        }

        private void OnLoadList(Object commandParameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

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
                    foreach (var item in loadedVocabularyItems) // TODO optimize
                    {
                        CurrentSession.addedVocabularyItems.Add(item);
                    }
                    //CurrentSession.addedVocabularyItems.AddRange(loadedVocabularyItems);

                    CurrentSession.userMadeChanges = false;
                    //UpdateVocabularyItemsGridView();
                    //vocabularyItemsGridView.ClearSelection();
                    
                    //vocabularyItemsDataGrid.Items.Refresh();
                }
            }
        }

        private void OnSaveList(Object commandParameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "MJV Files (*.mjv)|*.mjv";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(VocabularyItemHandler.ListToJson(CurrentSession.addedVocabularyItems.ToArray()));
                    CurrentSession.userMadeChanges = false;
                }
            }
        }

        private void OnExportCsvJapToEng(Object commandParameter)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            exportFileDialog.RestoreDirectory = true;

            if (exportFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(exportFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(VocabularyItem.ListToJapEng(CurrentSession.addedVocabularyItems.ToArray()));
                }
            }
        }

        private void OnExportCsvEngToJap(Object commandParameter)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            exportFileDialog.RestoreDirectory = true;

            if (exportFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(exportFileDialog.FileName, false, Encoding.UTF8))
                {
                    var vocabItems = CurrentSession.addedVocabularyItems.ToArray();
                    sw.Write(VocabularyItem.ListToJapEng(vocabItems));
                }
            }
        }

        public ObservableCollection<VocabularyItem> VocabList
        {
            get => CurrentSession.addedVocabularyItems;
            set {
                SetProperty(ref CurrentSession.addedVocabularyItems, value);
            }
        }
    }
}
