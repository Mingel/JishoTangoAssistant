using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace JishoTangoAssistant
{
    public class JapaneseUserInputViewModel : JishoTangoAssistantViewModelBase
    {
        #region attributes
        private string _input = String.Empty;
        private bool _writeInKana = false;
        private bool _japaneseToEnglishDirection = true;
        private bool _showFrontSide = true;
        private ObservableCollection<string> _englishDefinitions = new ObservableCollection<string>();
        private ObservableCollection<int> _selectedIndicesOfEnglishDefinitions = new ObservableCollection<int>();
        private string _additionalComments = String.Empty;
        private ObservableCollection<string> _words = new ObservableCollection<string>();
        private int _selectedIndexOfWords = -1;
        private ObservableCollection<string> _otherForms = new ObservableCollection<string>();
        private int _selectedIndexOfOtherForms = -1;
        private string _readingOutput = String.Empty;
        private int _selectedVocabItemIndex = -1;

        private Color _textInputBackground = (Color)ColorConverter.ConvertFromString("White");

        private readonly DelegateCommand _addToListCommand;
        private readonly DelegateCommand _processInputCommand;

        public ICommand AddToListCommand => _addToListCommand;
        public ICommand ProcessInputCommand => _processInputCommand;

        #endregion
        public delegate void UpdateCheckBoxesEventHandler(int dataLength, IList<int> englishDefinitionsLengths, IList<string> flattenedEnglishDefinitions);
        public event UpdateCheckBoxesEventHandler UpdateCheckBoxesEvent;

        public delegate void ClearCheckBoxesEventHandler();
        public event ClearCheckBoxesEventHandler ClearCheckBoxesEvent;

        private const string InputTextColorNoDuplicate = "White";
        private const string InputTextColorDifferentMeaning = "LightGoldenrodYellow";
        private const string InputTextColorSameMeaning = "DarkSalmon";

        private const string JishoTagUsuallyInKanaAlone = "Usually written using kana alone";

        public JapaneseUserInputViewModel()
        {
            _addToListCommand = new DelegateCommand(OnAddToList, _ => true);
            _processInputCommand = new DelegateCommand(ProcessInput, _ => true);

            SelectedIndicesOfEnglishDefinitions.CollectionChanged += (_, _) => ChangeReadingOutput();
        }

        #region auto-properties

        public string OutputText
        {
            get
            {
                var outputText = String.Empty;

                if (JapaneseToEnglishDirection && ShowFrontSide)
                {
                    if (SelectedIndexOfOtherForms >= 0)
                        outputText += !WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
                }
                else if (JapaneseToEnglishDirection && ShowBackSide)
                {
                    var englishDefinitionsString = String.Join("; ", EnglishDefinitions.Where((x, i) => SelectedIndicesOfEnglishDefinitions.Contains(i))); // TODO optimize
                    if (!WriteInKana)
                    {
                        outputText += ReadingOutput;
                        if (!string.IsNullOrWhiteSpace(englishDefinitionsString))
                            outputText += Environment.NewLine;
                    }
                    outputText += englishDefinitionsString;
                    if (!string.IsNullOrWhiteSpace(AdditionalComments))
                    {
                        outputText += Environment.NewLine;
                        outputText += AdditionalComments;
                    }
                }
                else if (EnglishToJapaneseDirection && ShowFrontSide)
                {
                    var englishDefinitionsString = String.Join("; ", EnglishDefinitions.Where((x, i) => SelectedIndicesOfEnglishDefinitions.Contains(i))); // TODO optimize
                    outputText += englishDefinitionsString;
                    if (!string.IsNullOrWhiteSpace(AdditionalComments))
                    {
                        outputText += Environment.NewLine;
                        outputText += AdditionalComments;
                    }
                }
                else //  (EnglishToJapaneseDirection && ShowBackSide)
                {
                    if (SelectedIndexOfOtherForms >= 0)
                        outputText += !WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
                    if (!WriteInKana)
                    {
                        outputText += Environment.NewLine;
                        outputText += ReadingOutput;
                    }
                }
                return outputText;
            }
        }

        public bool JapaneseToEnglishDirection
        {
            get => _japaneseToEnglishDirection;
            set
            {
                SetProperty(ref _japaneseToEnglishDirection, value);
                UpdateOutputText();
            }
        }

        public bool EnglishToJapaneseDirection
        {
            get => !_japaneseToEnglishDirection;
            set
            {
                SetProperty(ref _japaneseToEnglishDirection, !value);
                UpdateOutputText();
            }
        }

        public bool ShowFrontSide
        {
            get => _showFrontSide;
            set
            {
                SetProperty(ref _showFrontSide, value);
                UpdateOutputText();
            }
        }

        public bool ShowBackSide
        {
            get => !_showFrontSide;
            set
            {
                SetProperty(ref _showFrontSide, !value);
                UpdateOutputText();
            }
        }

        public int SelectedVocabItemIndex
        {
            get => _selectedVocabItemIndex;
            set { SetProperty(ref _selectedVocabItemIndex, value); UpdateTextInputBackground(); }
        }

        public string Input
        {
            get => _input;
            set { SetProperty(ref _input, value); UpdateTextInputBackground(); }
        }

        public string ReadingOutput
        {
            get => _readingOutput;
            set { SetProperty(ref _readingOutput, value); UpdateTextInputBackground(); }
}

        public ObservableCollection<string> Words
        {
            get => _words;
            set => SetProperty(ref _words, value);
        }

        public ObservableCollection<string> OtherForms
        {
            get => _otherForms;
            set => SetProperty(ref _otherForms, value);
        }

        public string AdditionalComments
        {
            get => _additionalComments;
            set
            {
                SetProperty(ref _additionalComments, value);
                UpdateOutputText();
                UpdateTextInputBackground();
            }
        }

        public bool WriteInKana
        {
            get => _writeInKana;
            set
            {
                SetProperty(ref _writeInKana, value);
                UpdateOutputText();
                UpdateTextInputBackground();
            }
        }

        public ObservableCollection<string> EnglishDefinitions
        {
            get => _englishDefinitions;
            set => SetProperty(ref _englishDefinitions, value);
        }

        public int SelectedIndexOfWords
        {
            get => _selectedIndexOfWords;
            set
            {
                SetProperty(ref _selectedIndexOfWords, value);
                if (SelectedIndexOfWords >= 0)
                {
                    ChangeOtherForms();
                    UpdateTextInputBackground();
                }
            }
        }

        public int SelectedIndexOfOtherForms
        {
            get => _selectedIndexOfOtherForms;
            set
            {
                SetProperty(ref _selectedIndexOfOtherForms, value);
                if (SelectedIndexOfOtherForms >= 0)
                {
                    ChangeReadingOutput();
                    UpdateTextInputBackground();
                }
            }
        }

        public ObservableCollection<int> SelectedIndicesOfEnglishDefinitions
        {
            get => _selectedIndicesOfEnglishDefinitions;
            set { SetProperty(ref _selectedIndicesOfEnglishDefinitions, value); UpdateTextInputBackground(); }
        }

        public Color TextInputBackground { get => _textInputBackground; set => SetProperty(ref _textInputBackground, value); }

        #endregion

        private void OnAddToList(Object commandParameter)
        {
            if (CurrentSession.latestResult == null)
                return;

            VocabularyItem? addedItem = CreateVocabularyItemFromCurrentUserInput();

            if (addedItem == null)
                return;

            CurrentSession.addedVocabularyItems.Add(addedItem);
            CurrentSession.userMadeChanges = true;
            UpdateTextInputBackground();
        }

        private async void ProcessInput(Object commandParameter)
        {
            if (!CurrentSession.running)
            {
                CurrentSession.running = true;

                Input = RomajiKanaConverter.Convert(Input);

                var result = await JishoWebAPIClient.GetResultJsonAsync(Input);
                if (result == null || result.Length == 0)
                {
                    ClearUserInputResults();
                    CurrentSession.running = false;

                    if (result == null) // Application could not retrieve information from Jisho
                    {
                        MessageBox.Show("Information could not be retrieved!",
                                                    "Error",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("No results have been found!",
                                                    "Information",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.None);
                    }
                    return;
                }

                CurrentSession.latestResult = result;

                ClearUserInputResults();

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

                WriteInKana = firstResult.senses[0].tags.Contains(JishoTagUsuallyInKanaAlone)
                    || firstJapaneseEntry.word == null;
                UpdateOutputText();
                CurrentSession.running = false;
            }
        }

        private void ClearUserInputResults()
        {
            EnglishDefinitions.Clear();
            ReadingOutput = String.Empty;
            Words.Clear();
            OtherForms.Clear();

            SelectedIndexOfWords = -1;
            SelectedIndexOfOtherForms = -1;

            AdditionalComments = String.Empty;
            WriteInKana = false;

            ClearCheckBoxesEvent?.Invoke();

            UpdateOutputText();
        }

        private void ChangeOtherForms()
        {
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
                SelectedIndexOfOtherForms = 0;

            ReadingOutput = selectedDatum.japanese[0].reading;

            StoreEnglishDefinitions(selectedDatum);

            WriteInKana = selectedDatum.senses[0].tags.Contains(JishoTagUsuallyInKanaAlone)
                || selectedDatum.japanese[0].word == null;
        }

        private void ChangeReadingOutput()
        {
            var latestResult = CurrentSession.latestResult;

            if (latestResult == null)
                return;

            ReadingOutput = latestResult[SelectedIndexOfWords].japanese[SelectedIndexOfOtherForms].reading;
        }

        private void StoreEnglishDefinitions(JishoDatum datum)
        {
            EnglishDefinitions.Clear();
            foreach (var sense in datum.senses)
            {
                foreach (var englishDefinition in sense.english_definitions)
                {
                    EnglishDefinitions.Add(englishDefinition);
                }
            }

            UpdateCheckBoxesEvent?.Invoke(datum.senses.Length, datum.senses.Select(x => x.english_definitions.Length).ToList(), EnglishDefinitions);
        }

        public void UpdateOutputText()
        {
            InvokePropertyChanged(nameof(OutputText));
        }

        public void ClearSelectedIndicesOfEnglishDefinitions()
        {
            SelectedIndicesOfEnglishDefinitions.Clear();
        }

        public void ChangeSelectedIndicesOfEnglishDefinitions(int i, bool isSelected)
        {
            if (isSelected)
                SelectedIndicesOfEnglishDefinitions.Add(i);
            else
                SelectedIndicesOfEnglishDefinitions.Remove(i);
            UpdateOutputText();
        }

        private VocabularyItem? CreateVocabularyItemFromCurrentUserInput()
        {
            if (SelectedIndexOfOtherForms < 0) // nothing has been searched (or no search results found)
                return null;
            var outputText = String.Empty;
            var englishDefinitionsString = String.Join("; ", EnglishDefinitions.Where((x, i) => SelectedIndicesOfEnglishDefinitions.Contains(i))); // TODO optimize
            outputText += englishDefinitionsString;
            if (!string.IsNullOrWhiteSpace(AdditionalComments) && !string.IsNullOrWhiteSpace(englishDefinitionsString))
                outputText += Environment.NewLine;
            outputText += AdditionalComments;
            bool showReading = !WriteInKana;
            var word = showReading ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
            return new VocabularyItem(word, showReading, ReadingOutput, outputText);
        }

        private void UpdateTextInputBackground()
        {
            var color = InputTextColorNoDuplicate;
            var itemFromCurrentUserInput = CreateVocabularyItemFromCurrentUserInput();
            if (itemFromCurrentUserInput != null && CurrentSession.addedVocabularyItems.ContainsWord(itemFromCurrentUserInput.Word))
            {
                if (CurrentSession.addedVocabularyItems.Contains(itemFromCurrentUserInput))
                    color = InputTextColorSameMeaning;
                else
                    color = InputTextColorDifferentMeaning;
            }
            TextInputBackground = (Color)ColorConverter.ConvertFromString(color);
        }
    }
}
