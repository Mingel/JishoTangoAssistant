using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MJapVocab
{
    public class MJapVocabViewModel : MJapVocabViewModelBase
    {
        private string _input = String.Empty;
        private bool _writeInKana = false;
        private ObservableCollection<string> _englishDefinitions = new ObservableCollection<string>();
        private ObservableCollection<int> _selectedIndicesOfEnglishDefinitions = new ObservableCollection<int>();
        private string _additionalComments = String.Empty;
        private ObservableCollection<string> _words = new ObservableCollection<string>();
        private int _selectedIndexOfWords = -1;
        private ObservableCollection<string> _otherForms = new ObservableCollection<string>();
        private int _selectedIndexOfOtherForms = -1;
        private string _readingOutput = String.Empty;

        private readonly DelegateCommand _loadListCommand;
        private readonly DelegateCommand _saveListCommand;
        private readonly DelegateCommand _exportCsvJapToEngCommand;
        private readonly DelegateCommand _exportCsvEngToJapCommand;
        private readonly DelegateCommand _addToListCommand;
        private readonly DelegateCommand _copyToClipboardCommand;
        private readonly DelegateCommand _processInputCommand;

        public ICommand SaveListCommand => _saveListCommand;
        public ICommand LoadListCommand => _loadListCommand;
        public ICommand ExportCsvJapToEngCommand => _exportCsvJapToEngCommand;
        public ICommand ExportCsvEngToJapCommand => _exportCsvEngToJapCommand;
        public ICommand AddToListCommand => _addToListCommand;
        public ICommand CopyToClipboardCommand => _copyToClipboardCommand;
        public ICommand ProcessInputCommand => _processInputCommand;

        //public event EventHandler CheckBoxEvent;

        public delegate void CheckBoxEventHandler(int dataLength, List<int> englishDefinitionsLengths, List<string> flattenedEnglishDefinitions);
        public event CheckBoxEventHandler CheckBoxEvent;

        public MJapVocabViewModel() {

            _loadListCommand = new DelegateCommand(OnLoadList, _ => true);
            _saveListCommand = new DelegateCommand(OnSaveList, _ => true);
            _exportCsvJapToEngCommand = new DelegateCommand(OnExportCsvJapToEng, _ => true);
            _exportCsvEngToJapCommand = new DelegateCommand(OnExportCsvEngToJap, _ => true);
            _addToListCommand = new DelegateCommand(OnAddToList, _ => true);
            _copyToClipboardCommand = new DelegateCommand(OnCopyToClipboard, _ => true);
            _processInputCommand = new DelegateCommand(ProcessInput, _ => true);

            SelectedIndicesOfEnglishDefinitions.CollectionChanged += (_, _) => ChangeReadingOutput();
        }

        public string OutputText
        {
            get
            {
                var outputText = String.Empty;
                var englishDefinitionsString = String.Join("; ", _englishDefinitions.Where((x, i) => SelectedIndicesOfEnglishDefinitions.Contains(i))); // TODO optimize
                if (!WriteInKana)
                {
                    outputText += ReadingOutput;
                    if (!string.IsNullOrWhiteSpace(englishDefinitionsString))
                    {
                        outputText += Environment.NewLine;
                    }
                }
                outputText += englishDefinitionsString;
                if (!string.IsNullOrWhiteSpace(AdditionalComments))
                {
                    outputText += Environment.NewLine;
                    outputText += AdditionalComments;
                }
                return outputText;
            }
        }

        public string Input
        {
            get => _input;
            set
            {
                SetProperty(ref _input, value);
            }
        }

        public string ReadingOutput
        {
            get => _readingOutput;
            set
            {
                SetProperty(ref _readingOutput, value);
            }
        }

        public ObservableCollection<string> Words
        {
            get => _words; 
            set
            {
                SetProperty(ref _words, value);
            }
        }

        public ObservableCollection<string> OtherForms
        {
            get => _otherForms;
            set
            {
                SetProperty(ref _otherForms, value);
            }
        }

        public string AdditionalComments
        {
            get => _additionalComments;
            set
            {
                SetProperty(ref _additionalComments, value);
                UpdateOutputText();
            }
        }

        public bool WriteInKana
        {
            get => _writeInKana;
            set
            {
                SetProperty(ref _writeInKana, value);
            }
        }

        public int SelectedIndexOfWords
        {
            get => _selectedIndexOfWords;
            set
            {
                SetProperty(ref _selectedIndexOfWords, value);
                if (SelectedIndexOfWords >= 0)
                    ChangeOtherForms();
            }
        }

        public int SelectedIndexOfOtherForms
        {
            get => _selectedIndexOfOtherForms;
            set
            {
                SetProperty(ref _selectedIndexOfOtherForms, value);
                if (SelectedIndexOfOtherForms >= 0)
                    ChangeReadingOutput();
            }
        }

        public ObservableCollection<int> SelectedIndicesOfEnglishDefinitions
        {
            get => _selectedIndicesOfEnglishDefinitions;
            set
            {
                SetProperty(ref _selectedIndicesOfEnglishDefinitions, value);
            }
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

                    SelectedIndexOfWords = -1;
                    SelectedIndexOfOtherForms = -1;

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

        private void OnAddToList(Object commandParameter)
        {
            if (CurrentSession.latestResult == null)
                return;
            var outputText = String.Empty;
            var englishDefinitionsString = String.Join("; ", _englishDefinitions.Where((x, i) => SelectedIndicesOfEnglishDefinitions.Contains(i))); // TODO optimize
            outputText += englishDefinitionsString;
            if (!string.IsNullOrWhiteSpace(AdditionalComments) && !string.IsNullOrWhiteSpace(englishDefinitionsString))
            {
                outputText += Environment.NewLine;
            }
            outputText += AdditionalComments;
            bool showReading = !WriteInKana;
            var word = showReading ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
            VocabularyItem item = new VocabularyItem(word, showReading, ReadingOutput, outputText);
            CurrentSession.addedVocabularyItems.Add(item);
            CurrentSession.userMadeChanges = true;
            //UpdateVocabularyItemsGridView();
            //vocabularyItemsGridView.Rows[vocabularyItemsGridView.Rows.Count - 1].Selected = true;
        }

        private void OnCopyToClipboard(Object commandParameter)
        {
            if (CurrentSession.running) // in this case: outputText.Box.Text != null
                Clipboard.SetText(OutputText);
        }

        public ObservableCollection<VocabularyItem> VocabList
        {
            get => CurrentSession.addedVocabularyItems;
            set {
                SetProperty(ref CurrentSession.addedVocabularyItems, value);
            }
        }

        private async void ProcessInput(Object commandParameter)
        {
            if (!CurrentSession.running)
            {
                CurrentSession.running = true;

                var result = await JishoWebAPIClient.GetResultJsonAsync(Input);
                if (result == null || result.Length == 0)
                {
                    ReadingOutput = String.Empty;
                    Words.Clear();
                    //wordComboBox.Enabled = false;
                    OtherForms.Clear();

                    SelectedIndexOfWords = -1;
                    SelectedIndexOfOtherForms = -1;

                    //OtherFormsComboBox.Enabled = false;
                    AdditionalComments = String.Empty;
                    //DisposeOutputCheckBoxes(true);
                    //outputTextBox.Text = String.Empty;
                    WriteInKana = false;
                    CurrentSession.running = false;

                    if (result == null) // Application could not retrieve information from Jisho
                    {
                        var messageBoxResult = MessageBox.Show("Information could not be retrieved!",
                                                    "Error",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error);
                    }
                    return;
                }

                CurrentSession.latestResult = result;

                //wordComboBox.Enabled = true;
                //OtherFormsComboBox.Enabled = true;

                Words.Clear();

                SelectedIndexOfWords = -1;
                SelectedIndexOfOtherForms = -1;

                AdditionalComments = String.Empty;

                var firstResult = result[0];
                var firstJapaneseEntry = firstResult.japanese[0];
                foreach (var res in result)
                {
                    if (res.japanese[0].word != null)
                        Words.Add(res.japanese[0].word);
                    else
                        Words.Add(res.japanese[0].reading);
                }
                if (Words.Count > 0)
                    SelectedIndexOfWords = 0;

                ReadingOutput = firstJapaneseEntry.reading;

                //WriteInKanaCheckbox.Enabled = true;
                WriteInKana = firstResult.senses.Where(x => x.tags.Contains("Usually written using kana alone")).Any()
                    || firstJapaneseEntry.word == null;
                //UpdateOutputTextbox();
                //CreateEnglishDefinitionsCheckBoxes(firstResult);
                CurrentSession.running = false;
            }
        }

        private void ChangeOtherForms()
        {
            //writeInKanaCheckbox.Enabled = true;

            OtherForms.Clear();

            SelectedIndexOfOtherForms = -1;

            var latestResult = CurrentSession.latestResult;
            if (latestResult == null)
                return;
            var selectedDatum = latestResult[SelectedIndexOfWords];
            foreach (var japItem in selectedDatum.japanese)
            {
                if (japItem.word != null)
                    OtherForms.Add(japItem.word);
                else
                    OtherForms.Add(japItem.reading);
            }
            if (OtherForms.Count > 0)
            {
                SelectedIndexOfOtherForms = 0;
            }

            ReadingOutput = selectedDatum.japanese[0].reading;

            CreateEnglishDefinitionsCheckBoxes(selectedDatum);

            //writeInKanaCheckbox.Enabled = selectedDatum.japanese[0].word != null;
            WriteInKana = selectedDatum.senses.Where(x => x.tags.Contains("Usually written using kana alone")).Any()
                || selectedDatum.japanese[0].word == null;

            //UpdateOutputTextbox();
        }

        private void ChangeReadingOutput()
        {
            var latestResult = CurrentSession.latestResult;
            if (latestResult == null)
                return;
            ReadingOutput = latestResult[SelectedIndexOfWords].japanese[SelectedIndexOfOtherForms].reading;
        }

        private void CreateEnglishDefinitionsCheckBoxes(JishoDatum datum)
        {
            List<string> englishDefinitions = new List<string>();

            foreach (var sense in datum.senses)
            {
                foreach (var englishDefinition in sense.english_definitions)
                {
                    englishDefinitions.Add(englishDefinition);
                }
            }

            CheckBoxEvent?.Invoke(datum.senses.Length, datum.senses.Select(x => x.english_definitions.Length).ToList(), englishDefinitions);
        }

        public void UpdateOutputText()
        {
            InvokePropertyChanged("OutputText");
        }

        public void ClearSelectedIndicesOfEnglishDefinitions()
        {
            SelectedIndicesOfEnglishDefinitions.Clear();
        }

        public void AddSelectedIndicesOfEnglishDefinitions(int i)
        {
            SelectedIndicesOfEnglishDefinitions.Add(i);
        }
    }
}
