using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Utils;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.Models.Jisho;
using JishoTangoAssistant.Models.Messages;
using JishoTangoAssistant.Services;

namespace JishoTangoAssistant.UI.ViewModel;

public partial class JapaneseUserInputViewModel : JishoTangoAssistantViewModelBase,
    IRecipient<ProcessInputMessage>
{
    public WordSearchViewModel WordSearchViewModel { get; } = new();

    #region attributes
    [ObservableProperty]
    private bool writeInKana;

    [ObservableProperty]
    private bool japaneseToEnglishDirection = true;

    [ObservableProperty]
    private bool showFrontSide = true;

    [ObservableProperty]
    private ObservableCollection<string> meanings = [];

    [ObservableProperty]
    private ObservableCollection<int> selectedIndicesOfMeanings = [];
    
    [ObservableProperty]
    private string additionalComments = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> words = [];

    [ObservableProperty]
    private int selectedIndexOfWords = -1;

    [ObservableProperty]
    private ObservableCollection<string> otherForms = [];

    [ObservableProperty]
    private int selectedIndexOfOtherForms = -1;

    [ObservableProperty]
    private string readingOutput = string.Empty;

    [ObservableProperty]
    private int selectedVocabItemIndex = -1;

    [ObservableProperty]
    private bool itemAdditionPossible;

    #endregion
    public delegate void UpdateCheckBoxesEventHandler(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings);
    public event UpdateCheckBoxesEventHandler? UpdateCheckBoxesEvent;

    public delegate void ClearCheckBoxesEventHandler();
    public event ClearCheckBoxesEventHandler? ClearCheckBoxesEvent;

    private const string JishoTagUsuallyInKanaAlone = "Usually written using kana alone";

    private readonly IJishoWebService jishoWebService;

    public JapaneseUserInputViewModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);

        SelectedIndicesOfMeanings.CollectionChanged += (_, _) => ChangeReadingOutput();
        CurrentSession.VocabularyListService.GetList().CollectionChanged += (_, _) => SendUpdateTextInputBackgroundMessage();

        jishoWebService = new JishoWebService(); // TODO DI
    }

    public async void Receive(ProcessInputMessage message)
    {
        await ProcessInput(message.Value);
    }

    private void SendUpdateTextInputBackgroundMessage()
    {
        var item = CreateVocabularyItemFromCurrentUserInput();
        WeakReferenceMessenger.Default.Send(new UpdateTextInputBackgroundMessage(item));
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


    partial void OnJapaneseToEnglishDirectionChanged(bool oldValue, bool newValue)
    {
        if (oldValue != newValue)
            UpdateOutputText();
    }

    private bool EnglishToJapaneseDirection => !JapaneseToEnglishDirection;

    partial void OnShowFrontSideChanged(bool oldValue, bool newValue)
    {
        if (oldValue != newValue)
            UpdateOutputText();
    }

    private bool ShowBackSide => !ShowFrontSide;

    partial void OnReadingOutputChanged(string? oldValue, string newValue)
    {
        if (oldValue != newValue)
            SendUpdateTextInputBackgroundMessage();
    }

    partial void OnAdditionalCommentsChanged(string? oldValue, string newValue)
    {
        if (oldValue == newValue) 
            return;
        
        UpdateOutputText();
        SendUpdateTextInputBackgroundMessage();
    }

    partial void OnWriteInKanaChanged(bool oldValue, bool newValue)
    {
        if (oldValue == newValue) 
            return;
        
        UpdateOutputText();
        SendUpdateTextInputBackgroundMessage();
    }

    partial void OnSelectedIndexOfWordsChanged(int oldValue, int newValue)
    {
        if (newValue < 0 || oldValue == newValue)
            return;
        
        ChangeOtherForms();
        SendUpdateTextInputBackgroundMessage();
    }

    partial void OnSelectedIndexOfOtherFormsChanged(int oldValue, int newValue)
    {
        if (newValue < 0 || oldValue == newValue)
            return;
        
        ChangeOtherForms();
        SendUpdateTextInputBackgroundMessage();
    }

    partial void OnSelectedIndicesOfMeaningsChanged(ObservableCollection<int>? oldValue, ObservableCollection<int> newValue)
    {
        if (oldValue == null || !oldValue.SequenceEqual(newValue))
            SendUpdateTextInputBackgroundMessage();
    }

    #endregion

    [RelayCommand]
    private async Task AddToList()
    {
        if (CurrentSession.lastRetrievedResults == null)
            return;

        var addedItem = CreateVocabularyItemFromCurrentUserInput();

        if (addedItem == null)
            return;

        await CurrentSession.VocabularyListService.AddAsync(addedItem);
        CurrentSession.userMadeChanges = true;
    }

    [RelayCommand]
    private async Task ProcessInput(string input)
    {
        if (CurrentSession.running)
            return;
        
        CurrentSession.running = true;

        var allResults = await jishoWebService.GetResultJsonAsync(input);
        if (allResults == null || !allResults.Any())
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

        if (WritingSystemChecker.ContainsKanji(input))
            GetIndicesOfInputInResult(input, allResults, ref resultIndex, ref entryIndex);

        var result = allResults[resultIndex];
        var japaneseEntry = result.Japanese[entryIndex];

        foreach (var res in allResults)
        {
            var firstJapaneseResult = res.Japanese.First();
            var word = !string.IsNullOrEmpty(firstJapaneseResult.Word) ? firstJapaneseResult.Word : firstJapaneseResult.Reading;
            Words.Add(word);
        }

        if (Words.Any())
        {
            SelectedIndexOfWords = resultIndex;
            if (OtherForms.Any())
                SelectedIndexOfOtherForms = entryIndex;
        }

        ReadingOutput = japaneseEntry.Reading;

        WriteInKana = result.Senses.First().Tags.Contains(JishoTagUsuallyInKanaAlone)
            || string.IsNullOrEmpty(japaneseEntry.Word);
        ItemAdditionPossible = true;
        UpdateOutputText();
        CurrentSession.running = false;
    }


    private static void GetIndicesOfInputInResult(string input, IList<JishoDatum> result, ref int resultIndex, ref int entryIndex)
    {
        ArgumentNullException.ThrowIfNull(result);
        ArgumentOutOfRangeException.ThrowIfNegative(resultIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(entryIndex);

        for (var i = 0; i < result.Count; i++)
        {
            var res = result[i];
            for (var j = 0; j < res.Japanese.Length; j++)
            {
                var entry = res.Japanese[j].Word;
                
                if (input != entry) continue;
                
                resultIndex = i;
                entryIndex = j;

                if (entryIndex == 0) // take result over entry of a prev result
                    return;

                break; // only take the first entry in the result
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
        if (OtherForms.Any())
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

        if (latestResult == null 
            || 0 <= SelectedIndexOfWords && SelectedIndexOfWords < latestResult.Count 
            || 0 <= SelectedIndexOfOtherForms && SelectedIndexOfOtherForms < latestResult[SelectedIndexOfWords].Japanese.Length)
            return;

        ReadingOutput = latestResult[SelectedIndexOfWords].Japanese[SelectedIndexOfOtherForms].Reading;
    }

    private void StoreMeanings(JishoDatum datum)
    {
        Meanings.Clear();
        var englishDefinitions = datum.Senses.SelectMany(sense => sense.EnglishDefinitions);
        foreach (var meaning in englishDefinitions)
        {
            Meanings.Add(meaning);
        }

        UpdateCheckBoxesEvent?.Invoke(datum.Senses.Length, 
            datum.Senses.Select(x => x.EnglishDefinitions.Length).ToList(), 
            Meanings);
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
        if (!string.IsNullOrWhiteSpace(AdditionalComments) 
            && !string.IsNullOrWhiteSpace(meaningsString))
            outputText += Environment.NewLine;
        outputText += AdditionalComments;
        var showReading = !WriteInKana;
        var word = showReading ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput;
        return new VocabularyItem(word, showReading, ReadingOutput, outputText);
    }
}
