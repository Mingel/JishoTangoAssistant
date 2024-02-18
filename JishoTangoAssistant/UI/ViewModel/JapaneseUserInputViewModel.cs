using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.Services;
using JishoTangoAssistant.Services.Jisho;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace JishoTangoAssistant.UI.ViewModel;

public partial class JapaneseUserInputViewModel : JishoTangoAssistantViewModelBase
{
    #region attributes
    [ObservableProperty]
    private string input = string.Empty;

    [ObservableProperty]
    private bool writeInKana;

    [ObservableProperty]
    private bool japaneseToEnglishDirection = true;

    [ObservableProperty]
    private bool showFrontSide = true;

    [ObservableProperty]
    private ObservableCollection<string> meanings = new ObservableCollection<string>();

    [ObservableProperty]
    private ObservableCollection<int> selectedIndicesOfMeanings = new ObservableCollection<int>();
    
    [ObservableProperty]
    private string additionalComments = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> words = new ObservableCollection<string>();

    [ObservableProperty]
    private int selectedIndexOfWords = -1;

    [ObservableProperty]
    private ObservableCollection<string> otherForms = new ObservableCollection<string>();

    [ObservableProperty]
    private int selectedIndexOfOtherForms = -1;

    [ObservableProperty]
    private string readingOutput = string.Empty;

    [ObservableProperty]
    private int selectedVocabItemIndex = -1;

    [ObservableProperty]
    private bool itemAdditionPossible;
    
    [ObservableProperty]
    private Color textInputBackground = App.UsesDarkMode() ? Color.Parse("#66000000") : Color.Parse("#66ffffff");

    #endregion
    public delegate void UpdateCheckBoxesEventHandler(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings);
    public event UpdateCheckBoxesEventHandler? UpdateCheckBoxesEvent;

    public delegate void ClearCheckBoxesEventHandler();
    public event ClearCheckBoxesEventHandler? ClearCheckBoxesEvent;

    private const string JishoTagUsuallyInKanaAlone = "Usually written using kana alone";

    public JapaneseUserInputViewModel()
    {
        SelectedIndicesOfMeanings.CollectionChanged += (_, _) => ChangeReadingOutput();
        CurrentSession.addedVocabularyItems.CollectionChanged += (_, _) => UpdateTextInputBackground();
    }

    #region auto-properties

    public string OutputText
    {
        get
        {
            var outputText = string.Empty;

            if (JapaneseToEnglishDirection && ShowFrontSide)
            {
                if (SelectedIndexOfOtherForms >= 0)
                    outputText += !WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
            }
            else if (JapaneseToEnglishDirection && ShowBackSide)
            {
                var meaningsString = string.Join("; ", Meanings.Where((_, i) => SelectedIndicesOfMeanings.Contains(i))); // TODO optimize
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
                var meaningsString = string.Join("; ", Meanings.Where((_, i) => SelectedIndicesOfMeanings.Contains(i))); // TODO optimize
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


    partial void OnJapaneseToEnglishDirectionChanged(bool value) => UpdateOutputText();

    private bool EnglishToJapaneseDirection => !JapaneseToEnglishDirection;

    partial void OnShowFrontSideChanged(bool value) => UpdateOutputText();

    private bool ShowBackSide => !ShowFrontSide;

    partial void OnInputChanged(string value) => UpdateTextInputBackground();

    partial void OnReadingOutputChanged(string value) => UpdateTextInputBackground();

    partial void OnAdditionalCommentsChanged(string value) 
    {
        UpdateOutputText();
        UpdateTextInputBackground();
    }

    partial void OnWriteInKanaChanged(bool value) 
    {
        UpdateOutputText();
        UpdateTextInputBackground();
    }

    partial void OnSelectedIndexOfWordsChanged(int value) 
    {
        if (value >= 0)
        {
            ChangeOtherForms();
            UpdateTextInputBackground();
        }
    }

    partial void OnSelectedIndexOfOtherFormsChanged(int value) 
    {
        if (value >= 0)
        {
            ChangeOtherForms();
            UpdateTextInputBackground();
        }
    }

    partial void OnSelectedIndicesOfMeaningsChanged(ObservableCollection<int> value) => UpdateTextInputBackground();
    #endregion

    [RelayCommand]
    private void AddToList()
    {
        if (CurrentSession.lastRetrievedResults == null)
            return;

        VocabularyItem? addedItem = CreateVocabularyItemFromCurrentUserInput();

        if (addedItem == null)
            return;

        CurrentSession.addedVocabularyItems.Add(addedItem);
        CurrentSession.userMadeChanges = true;
    }

    [RelayCommand]
    private async Task ProcessInput()
    {
        if (!CurrentSession.running)
        {
            CurrentSession.running = true;

            Input = RomajiKanaConverter.Convert(Input.Trim());

            var allResults = await JishoWebApiClient.GetResultJsonAsync(Input);
            if (allResults == null || allResults.Length == 0)
            {
                ClearUserInputResults();
                CurrentSession.running = false;

                var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

                if (mainWindow != null && allResults == null) // Application could not retrieve information from Jisho
                {
                    await MessageBox.Show(mainWindow, "Error", "Information could not be retrieved!", MessageBoxButtons.Ok);
                }
                else if (mainWindow != null)
                {
                    await MessageBox.Show(mainWindow, "Information", "No results were found!", MessageBoxButtons.Ok);
                }
                return;
            }

            CurrentSession.lastRetrievedResults = allResults;

            ClearUserInputResults();

            var resultIndex = 0;
            var entryIndex = 0;

            if (CheckWritingSystem.ContainsKanji(Input))
                GetIndicesOfInputInResult(allResults, ref resultIndex, ref entryIndex);

            var result = allResults[resultIndex];
            var japaneseEntry = result.Japanese[entryIndex];

            foreach (var res in allResults)
            {
                var firstJapaneseResult = res.Japanese.First();
                var word = !string.IsNullOrEmpty(firstJapaneseResult.Word) ? firstJapaneseResult.Word : firstJapaneseResult.Reading;
                Words.Add(word);
            }

            if (Words.Count > 0)
            {
                SelectedIndexOfWords = resultIndex;
                if (OtherForms.Count > 0)
                    SelectedIndexOfOtherForms = entryIndex;
            }

            ReadingOutput = japaneseEntry.Reading;

            WriteInKana = result.Senses.First().Tags.Contains(JishoTagUsuallyInKanaAlone)
                || string.IsNullOrEmpty(japaneseEntry.Word);
            ItemAdditionPossible = true;
            UpdateOutputText();
            CurrentSession.running = false;
        }
    }


    private void GetIndicesOfInputInResult(JishoDatum[]? result, ref int resultIndex, ref int entryIndex)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(resultIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(entryIndex);
        resultIndex = -1;
        entryIndex = -1;

        if (result != null)
        {
            for (var i = 0; i < result.Length; i++)
            {
                var res = result[i];
                for (var j = 0; j < res.Japanese.Length; j++)
                {
                    var entry = res.Japanese[j].Word;
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
        }

        // default to index 0 if no result was found
        resultIndex = Math.Max(resultIndex, 0);
        entryIndex = Math.Max(entryIndex, 0);
    }

    private void ClearUserInputResults()
    {
        Meanings.Clear();
        ReadingOutput = string.Empty;
        Words.Clear();
        OtherForms.Clear();

        SelectedIndexOfWords = -1;
        SelectedIndexOfOtherForms = -1;

        AdditionalComments = string.Empty;
        WriteInKana = false;

        ClearCheckBoxesEvent?.Invoke();

        ItemAdditionPossible = false;

        UpdateOutputText();
    }

    private void ChangeOtherForms()
    {
        OtherForms.Clear();

        var latestResult = CurrentSession.lastRetrievedResults;
        if (latestResult == null)
            return;
        var selectedDatum = latestResult[SelectedIndexOfWords];
        foreach (var japItem in selectedDatum.Japanese)
        {
            var otherForm = !string.IsNullOrEmpty(japItem.Word) ? japItem.Word : japItem.Reading;
            OtherForms.Add(otherForm);
        }
        if (OtherForms.Count > 0)
            SelectedIndexOfOtherForms = 0;
        else
            SelectedIndexOfOtherForms = -1;

        ReadingOutput = selectedDatum.Japanese.First().Reading;

        StoreMeanings(selectedDatum);

        WriteInKana = selectedDatum.Senses.First().Tags.Contains(JishoTagUsuallyInKanaAlone)
            || string.IsNullOrEmpty(selectedDatum.Japanese.First().Word);
    }

    private void ChangeReadingOutput()
    {
        var latestResult = CurrentSession.lastRetrievedResults;

        if (latestResult == null)
            return;

        ReadingOutput = latestResult[SelectedIndexOfWords].Japanese[SelectedIndexOfOtherForms].Reading;
    }

    private void StoreMeanings(JishoDatum datum)
    {
        Meanings.Clear();
        foreach (var sense in datum.Senses)
        {
            foreach (var meaning in sense.EnglishDefinitions)
            {
                Meanings.Add(meaning);
            }
        }

        UpdateCheckBoxesEvent?.Invoke(datum.Senses.Length, datum.Senses.Select(x => x.EnglishDefinitions.Length).ToList(), Meanings);
    }

    public void UpdateOutputText()
    {
        OnPropertyChanged(nameof(OutputText));
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
        var outputText = string.Empty;
        var meaningsString = string.Join("; ", Meanings.Where((_, i) => SelectedIndicesOfMeanings.Contains(i))); // TODO optimize
        outputText += meaningsString;
        if (!string.IsNullOrWhiteSpace(AdditionalComments) && !string.IsNullOrWhiteSpace(meaningsString))
            outputText += Environment.NewLine;
        outputText += AdditionalComments;
        var showReading = !WriteInKana;
        var word = showReading ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
        return new VocabularyItem(word, showReading, ReadingOutput, outputText);
    }

    private void UpdateTextInputBackground()
    {
        var color = InputTextColorNoDuplicate();
        var itemFromCurrentUserInput = CreateVocabularyItemFromCurrentUserInput();
        if (itemFromCurrentUserInput != null && CurrentSession.addedVocabularyItems.ContainsWord(itemFromCurrentUserInput.Word))
        {
            color = CurrentSession.addedVocabularyItems.Contains(itemFromCurrentUserInput) ? InputTextColorSameMeaning() : InputTextColorDifferentMeaning();
        }
        TextInputBackground = Color.Parse(color);
    }
    private string InputTextColorNoDuplicate()
    {
        return App.UsesDarkMode() ? "#66000000" : "#66FFFFFF";
    }

    private string InputTextColorDifferentMeaning()
    {
        return App.UsesDarkMode() ? "#667D7D69" : "#66FAFAD2";
    }
    private string InputTextColorSameMeaning()
    {
        return App.UsesDarkMode() ? "#66744B3D" : "#66E9967A";
    }
}
