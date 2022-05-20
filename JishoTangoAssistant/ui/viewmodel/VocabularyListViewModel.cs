using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace JishoTangoAssistant
{
    public class VocabularyListViewModel : JishoTangoAssistantViewModelBase
    {
        private int _selectedVocabItemIndex = -1;

        private readonly DelegateCommand _loadListCommand;
        private readonly DelegateCommand _saveListCommand;
        private readonly DelegateCommand _exportCsvJapToEngCommand;
        private readonly DelegateCommand _exportCsvEngToJapCommand;
        private readonly DelegateCommand _deleteFromListCommand;
        private readonly DelegateCommand _goUpCommand;
        private readonly DelegateCommand _goDownCommand;

        public ICommand SaveListCommand => _saveListCommand;
        public ICommand LoadListCommand => _loadListCommand;
        public ICommand ExportCsvJapToEngCommand => _exportCsvJapToEngCommand;
        public ICommand ExportCsvEngToJapCommand => _exportCsvEngToJapCommand;
        public ICommand DeleteFromListCommand => _deleteFromListCommand;
        public ICommand GoUpCommand => _goUpCommand;
        public ICommand GoDownCommand => _goDownCommand;

        public ObservableVocabularyList VocabularyList
        {
            get => CurrentSession.addedVocabularyItems;
            set
            {
                SetProperty(ref CurrentSession.addedVocabularyItems, value);
            }
        }

        public int SelectedVocabItemIndex
        {
            get => _selectedVocabItemIndex;
            set
            {
                SetProperty(ref _selectedVocabItemIndex, value);
            }
        }

        public int FontSize
        {
            get => CurrentSession.customFontSize; 
            set => SetProperty(ref CurrentSession.customFontSize, value);
        }

        public VocabularyListViewModel()
        {
            _loadListCommand = new DelegateCommand(OnLoadList, _ => true);
            _saveListCommand = new DelegateCommand(OnSaveList, _ => true);
            _exportCsvJapToEngCommand = new DelegateCommand(OnExportCsvJapeneseToEnglish, _ => true);
            _exportCsvEngToJapCommand = new DelegateCommand(OnExportCsvEnglishToJapanese, _ => true);
            _deleteFromListCommand = new DelegateCommand(OnDeleteFromList, _ => true);
            _goUpCommand = new DelegateCommand(OnGoUp, _ => true);
            _goDownCommand = new DelegateCommand(OnGoDown, _ => true);
        }

        private void OnLoadList(Object commandParameter)
        {
            if (CurrentSession.userMadeChanges)
            {
                var messageBox = MessageBox.Show("You have made unsaved changes. Do you really want to overwrite your current vocabulary list?",
                                                    "Warning",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning);
                if (messageBox.Equals(MessageBoxResult.No))
                    return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "MJV Files (*.mjv)|*.mjv";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    var loadedVocabularyItems = JsonConvert.DeserializeObject<VocabularyItem[]>(fileContent);

                    if (loadedVocabularyItems == null)
                        throw new ArgumentNullException($"{nameof(loadedVocabularyItems)} is null");

                    VocabularyList.Clear();
                    VocabularyList.AddRange(loadedVocabularyItems);

                    CurrentSession.userMadeChanges = false;
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
                    var json = JsonConvert.SerializeObject(VocabularyList.ToArray(), Formatting.Indented);
                    sw.Write(json);
                    CurrentSession.userMadeChanges = false;
                }
            }
        }

        private void OnExportCsvJapeneseToEnglish(Object commandParameter)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            exportFileDialog.RestoreDirectory = true;

            if (exportFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(exportFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(VocabularyListExporter.JapaneseToEnglish(VocabularyList));
                    ShowHtmlMessageBox();
                }
            }
        }

        private void OnExportCsvEnglishToJapanese(Object commandParameter)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            exportFileDialog.RestoreDirectory = true;

            if (exportFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(exportFileDialog.FileName, false, Encoding.UTF8))
                {
                    var vocabItems = VocabularyList.ToArray();
                    sw.Write(VocabularyListExporter.EnglishToJapanese(VocabularyList));
                    ShowHtmlMessageBox();
                }
            }
        }

        private void OnDeleteFromList(object commandParameter)
        {
            if (0 <= SelectedVocabItemIndex && SelectedVocabItemIndex < VocabularyList.Count)
                VocabularyList.RemoveAt(SelectedVocabItemIndex);
        }

        private void OnGoUp(object commandParameter)
        {
            if (SelectedVocabItemIndex > 0)
            {
                var tmpIndex = SelectedVocabItemIndex;
                var tmpItem = VocabularyList[SelectedVocabItemIndex - 1];
                VocabularyList[SelectedVocabItemIndex - 1] = VocabularyList[SelectedVocabItemIndex];
                VocabularyList[SelectedVocabItemIndex] = tmpItem; // this line makes tmpIndex necessary because this line resets SelectedVocabItemIndex to -1
                SelectedVocabItemIndex = tmpIndex - 1;
            }
        }

        private void OnGoDown(object commandParameter)
        {
            if (SelectedVocabItemIndex < CurrentSession.addedVocabularyItems.Count - 1)
            {
                var tmpIndex = SelectedVocabItemIndex;
                var tmpItem = VocabularyList[SelectedVocabItemIndex + 1];
                VocabularyList[SelectedVocabItemIndex + 1] = VocabularyList[SelectedVocabItemIndex];
                VocabularyList[SelectedVocabItemIndex] = tmpItem; // this line makes tmpIndex necessary because this line resets SelectedVocabItemIndex to -1
                SelectedVocabItemIndex = tmpIndex + 1;
            }
        }

        private void ShowHtmlMessageBox()
        {
            MessageBox.Show("Make sure to ENABLE \"Allow HTML in fields\" when importing the exported file into Anki!",
                            "Information",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}
