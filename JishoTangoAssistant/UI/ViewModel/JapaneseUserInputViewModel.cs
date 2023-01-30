using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.Services;
using JishoTangoAssistant.Services.Commands;
using JishoTangoAssistant.Services.Jisho;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;

namespace JishoTangoAssistant.UI.ViewModel
{
    public class JapaneseUserInputViewModel : JishoTangoAssistantViewModelBase
    {
        #region attributes
        private string _input = String.Empty;
        private bool _writeInKana = false;
        private bool _japaneseToEnglishDirection = true;
        private bool _showFrontSide = true;
        private ObservableCollection<string> _meanings = new ObservableCollection<string>();
        private ObservableCollection<int> _selectedIndicesOfMeanings = new ObservableCollection<int>();
        private string _additionalComments = String.Empty;
        private ObservableCollection<string> _words = new ObservableCollection<string>();
        private int _selectedIndexOfWords = -1;
        private ObservableCollection<string> _otherForms = new ObservableCollection<string>();
        private int _selectedIndexOfOtherForms = -1;
        private string _readingOutput = String.Empty;
        private int _selectedVocabItemIndex = -1;
        private bool _itemAdditionPossible = false;

        private Color _textInputBackground = App.UsesDarkMode() ? Color.Parse("#66000000") : Color.Parse("#66ffffff");
        private readonly DelegateCommand _addToListCommand;
        private readonly DelegateCommand _processInputCommand;

        public ICommand AddToListCommand => _addToListCommand;
        public ICommand ProcessInputCommand => _processInputCommand;

        #endregion
        public delegate void UpdateCheckBoxesEventHandler(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings);
        public event UpdateCheckBoxesEventHandler UpdateCheckBoxesEvent;

        public delegate void ClearCheckBoxesEventHandler();
        public event ClearCheckBoxesEventHandler ClearCheckBoxesEvent;

        private const string JishoTagUsuallyInKanaAlone = "Usually written using kana alone";

        public JapaneseUserInputViewModel()
        {
            _addToListCommand = new DelegateCommand(OnAddToList, _ => true);
            _processInputCommand = new DelegateCommand(ProcessInput, _ => true);

            SelectedIndicesOfMeanings.CollectionChanged += (_, _) => ChangeReadingOutput();
            CurrentSession.addedVocabularyItems.CollectionChanged += (_, _) => UpdateTextInputBackground();
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
                    var meaningsString = String.Join("; ", Meanings.Where((x, i) => SelectedIndicesOfMeanings.Contains(i))); // TODO optimize
                    if (!WriteInKana)
                    {
                        outputText += ReadingOutput;
                        if (!string.IsNullOrWhiteSpace(meaningsString))
                            outputText += Environment.NewLine;
                    }
                    outputText += meaningsString;
                    if (!string.IsNullOrWhiteSpace(AdditionalComments))
                    {
                        outputText += Environment.NewLine;
                        outputText += AdditionalComments;
                    }
                }
                else if (EnglishToJapaneseDirection && ShowFrontSide)
                {
                    var meaningsString = String.Join("; ", Meanings.Where((x, i) => SelectedIndicesOfMeanings.Contains(i))); // TODO optimize
                    outputText += meaningsString;
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

        public bool ItemAdditionPossible
        {
            get => _itemAdditionPossible;
            set
            {
                SetProperty(ref _itemAdditionPossible, value);
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

        public ObservableCollection<string> Meanings
        {
            get => _meanings;
            set => SetProperty(ref _meanings, value);
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

        public ObservableCollection<int> SelectedIndicesOfMeanings
        {
            get => _selectedIndicesOfMeanings;
            set { SetProperty(ref _selectedIndicesOfMeanings, value); UpdateTextInputBackground(); }
        }

        public Color TextInputBackground { get => _textInputBackground; set => SetProperty(ref _textInputBackground, value); }

        #endregion

        private void OnAddToList(Object commandParameter)
        {
            if (CurrentSession.lastRetrievedResults == null)
                return;

            VocabularyItem? addedItem = CreateVocabularyItemFromCurrentUserInput();

            if (addedItem == null)
                return;

            CurrentSession.addedVocabularyItems.Add(addedItem);
            CurrentSession.userMadeChanges = true;
        }

        private async void ProcessInput(Object commandParameter)
        {
            if (!CurrentSession.running)
            {
                CurrentSession.running = true;

                Input = RomajiKanaConverter.Convert(Input.Trim());

                var allResults = await JishoWebAPIClient.GetResultJsonAsync(Input);
                if (allResults == null || allResults.Length == 0)
                {
                    ClearUserInputResults();
                    CurrentSession.running = false;

                    if (allResults == null) // Application could not retrieve information from Jisho
                    {
                        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime).MainWindow;
                        await MessageBox.Show(mainWindow, "Error", "Information could not be retrieved!", MessageBoxButtons.Ok);
                    }
                    else
                    {
                        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime).MainWindow;
                        await MessageBox.Show(mainWindow, "Information", "No results were found!", MessageBoxButtons.Ok);
                    }
                    return;
                }

                CurrentSession.lastRetrievedResults = allResults;

                ClearUserInputResults();

                int resultIndex = 0;
                int entryIndex = 0;

                if (CheckWritingSystem.ContainsKanji(Input))
                    GetIndicesOfInputInResult(allResults, ref resultIndex, ref entryIndex);

                var result = allResults[resultIndex];
                var japaneseEntry = result.japanese[entryIndex];

                foreach (var res in allResults)
                {
                    if (res.japanese[0].word != null)
                        Words.Add(res.japanese[0].word);
                    else
                        Words.Add(res.japanese[0].reading);
                }

                if (Words.Count > 0)
                {
                    SelectedIndexOfWords = resultIndex;
                    if (OtherForms.Count > 0)
                        SelectedIndexOfOtherForms = entryIndex;
                }

                ReadingOutput = japaneseEntry.reading;

                WriteInKana = result.senses[0].tags.Contains(JishoTagUsuallyInKanaAlone)
                    || japaneseEntry.word == null;
                ItemAdditionPossible = true;
                UpdateOutputText();
                CurrentSession.running = false;
            }
        }


        private void GetIndicesOfInputInResult(JishoDatum[]? result, ref int resultIndex, ref int entryIndex)
        {
            resultIndex = -1;
            entryIndex = -1;

            for (int i = 0; i < result.Length; i++)
            {
                var res = result[i];
                for (int j = 0; j < res.japanese.Length; j++)
                {
                    var entry = res.japanese[j].word;
                    if (Input.Equals(entry))
                    {
                        resultIndex = i;
                        entryIndex = j;

                        if (entryIndex == 0) // take result over entry of a prev result
                            return;

                        break; // only take the first entry in the result
                    }
                }
            }

            // default to index 0 if no result was found
            resultIndex = Math.Max(resultIndex, 0);
            entryIndex = Math.Max(entryIndex, 0);
        }

        private void ClearUserInputResults()
        {
            Meanings.Clear();
            ReadingOutput = String.Empty;
            Words.Clear();
            OtherForms.Clear();

            SelectedIndexOfWords = -1;
            SelectedIndexOfOtherForms = -1;

            AdditionalComments = String.Empty;
            WriteInKana = false;

            ClearCheckBoxesEvent?.Invoke();

            ItemAdditionPossible = false;

            UpdateOutputText();
        }

        private void ChangeOtherForms()
        {
            OtherForms.Clear();

            SelectedIndexOfOtherForms = -1;

            var latestResult = CurrentSession.lastRetrievedResults;
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

            StoreMeanings(selectedDatum);

            WriteInKana = selectedDatum.senses[0].tags.Contains(JishoTagUsuallyInKanaAlone)
                || selectedDatum.japanese[0].word == null;
        }

        private void ChangeReadingOutput()
        {
            var latestResult = CurrentSession.lastRetrievedResults;

            if (latestResult == null)
                return;

            ReadingOutput = latestResult[SelectedIndexOfWords].japanese[SelectedIndexOfOtherForms].reading;
        }

        private void StoreMeanings(JishoDatum datum)
        {
            Meanings.Clear();
            foreach (var sense in datum.senses)
            {
                foreach (var meaning in sense.english_definitions)
                {
                    Meanings.Add(meaning);
                }
            }

            UpdateCheckBoxesEvent?.Invoke(datum.senses.Length, datum.senses.Select(x => x.english_definitions.Length).ToList(), Meanings);
        }

        public void UpdateOutputText()
        {
            InvokePropertyChanged(nameof(OutputText));
        }

        public void ClearSelectedIndicesOfMeanings()
        {
            SelectedIndicesOfMeanings.Clear();
        }

        public void ChangeSelectedIndicesOfMeanings(int i, bool isSelected)
        {
            if (isSelected)
                SelectedIndicesOfMeanings.Add(i);
            else
                SelectedIndicesOfMeanings.Remove(i);
            UpdateOutputText();
        }

        private VocabularyItem? CreateVocabularyItemFromCurrentUserInput()
        {
            if (SelectedIndexOfOtherForms < 0) // nothing has been searched (or no search results found)
                return null;
            var outputText = String.Empty;
            var meaningsString = String.Join("; ", Meanings.Where((x, i) => SelectedIndicesOfMeanings.Contains(i))); // TODO optimize
            outputText += meaningsString;
            if (!string.IsNullOrWhiteSpace(AdditionalComments) && !string.IsNullOrWhiteSpace(meaningsString))
                outputText += Environment.NewLine;
            outputText += AdditionalComments;
            bool showReading = !WriteInKana;
            var word = showReading ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
            return new VocabularyItem(word, showReading, ReadingOutput, outputText);
        }

        private void UpdateTextInputBackground()
        {
            var color = InputTextColorNoDuplicate();
            var itemFromCurrentUserInput = CreateVocabularyItemFromCurrentUserInput();
            if (itemFromCurrentUserInput != null && CurrentSession.addedVocabularyItems.ContainsWord(itemFromCurrentUserInput.Word))
            {
                if (CurrentSession.addedVocabularyItems.Contains(itemFromCurrentUserInput))
                    color = InputTextColorSameMeaning();
                else
                    color = InputTextColorDifferentMeaning();
            }
            TextInputBackground = Color.Parse(color);
        }
        private string InputTextColorNoDuplicate()
        {
            if (App.UsesDarkMode())
                return "#66000000";
            else
                return "#66FFFFFF";
        }

        private string InputTextColorDifferentMeaning()
        {
            if (App.UsesDarkMode())
                return "#667D7D69";
            else
                return "#66FAFAD2";
        }
        private string InputTextColorSameMeaning()
        {
            if (App.UsesDarkMode())
                return "#66744B3D";
            else
                return "#66E9967A";
        }
    }
}
