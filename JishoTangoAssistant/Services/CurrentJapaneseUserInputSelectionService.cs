using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.Models.Jisho;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using JishoTangoAssistant.Utils;

namespace JishoTangoAssistant.Services;

public class CurrentJapaneseUserInputSelectionService
{
    private const string JishoTagUsuallyInKanaAlone = "Usually written using kana alone";
    private readonly CurrentJapaneseUserInputSelection selection;
    private readonly JishoWebService jishoWebService;

    public CurrentJapaneseUserInputSelectionService()
    {
        selection = new CurrentJapaneseUserInputSelection(); // TODO DI
        jishoWebService = new JishoWebService(); // TODO DI
    }
    
#region selection-attributes
    public ObservableCollection<string> GetWords() => selection.Words;

    public ObservableCollection<string> GetOtherForms() => selection.OtherForms;

    public ObservableRangeCollection<SimilarMeaningsGroup> GetMeanings() => selection.Meanings;
    
    public int GetSelectedWordsIndex() => selection.SelectedWordsIndex;
    public void SetSelectedWordsIndex(int value) => selection.SelectedWordsIndex = value;
    
    public int GetSelectedOtherFormsIndex() => selection.SelectedOtherFormsIndex;
    public void SetSelectedOtherFormsIndex(int value) => selection.SelectedOtherFormsIndex = value;
    
    public string GetReadingOutput() => selection.ReadingOutput;
    
    public string GetAdditionalComments() => selection.AdditionalComments;
    public void SetAdditionalComments(string value) => selection.AdditionalComments = value;
    
    public bool GetWriteInKana() => selection.WriteInKana;
    public void SetWriteInKana(bool value) => selection.WriteInKana = value;
    
    public bool GetItemAdditionPossible() => selection.ItemAdditionPossible;
#endregion

    public async Task UpdateSelectionAsync(string preprocessedInput)
    {
        var allResults = await jishoWebService.GetResultJsonAsync(preprocessedInput);
        if (allResults == null || !allResults.Any()) // TODO move out of this service
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

        if (WritingSystemChecker.ContainsKanji(preprocessedInput))
            GetIndicesOfInputInResult(preprocessedInput, allResults, ref resultIndex, ref entryIndex);

        var result = allResults[resultIndex];
        var japaneseEntry = result.Japanese[entryIndex];

        foreach (var res in allResults)
        {
            var firstJapaneseResult = res.Japanese.First();
            var word = !string.IsNullOrEmpty(firstJapaneseResult.Word) ? firstJapaneseResult.Word : firstJapaneseResult.Reading;
            selection.Words.Add(word);
        }

        if (selection.Words.Any())
        {
            selection.SelectedWordsIndex = resultIndex;
            if (selection.OtherForms.Any())
                selection.SelectedOtherFormsIndex = entryIndex;
        }

        selection.ReadingOutput = japaneseEntry.Reading;

        selection.WriteInKana = result.Senses.First().Tags.Contains(JishoTagUsuallyInKanaAlone)
                                || string.IsNullOrEmpty(japaneseEntry.Word);
        selection.ItemAdditionPossible = true;
    }
    
    private void ClearUserInputResults()
    {
        selection.Meanings.Clear();
        selection.ReadingOutput = string.Empty;
        selection.Words.Clear();
        selection.OtherForms.Clear();

        selection.SelectedWordsIndex = -1;
        selection.SelectedOtherFormsIndex = -1;

        selection.AdditionalComments = string.Empty;
        selection.WriteInKana = false;

        selection.ItemAdditionPossible = false;
    }
    
    public VocabularyItem? CreateVocabularyItem()
    {
        if (selection.SelectedOtherFormsIndex < 0) // nothing has been searched (or no search results found)
            return null;
        var outputText = string.Empty;
        var meaningsString = string.Join("; ", selection.Meanings.Select(g => g.SimilarMeanings)
            .SelectMany(g => g)
            .Where(m => m.IsEnabled)
            .Select(m => m.Value)); // TODO optimize
        outputText += meaningsString;
        if (!string.IsNullOrWhiteSpace(selection.AdditionalComments) 
            && !string.IsNullOrWhiteSpace(meaningsString))
            outputText += Environment.NewLine;
        outputText += selection.AdditionalComments;
        var showReading = !selection.WriteInKana;
        var word = showReading ? selection.OtherForms.ElementAt(selection.SelectedOtherFormsIndex) : selection.ReadingOutput;
        return new VocabularyItem(word, showReading, selection.ReadingOutput, outputText);
    }

    private void StoreMeanings(JishoDatum datum)
    {
        var groups = new List<SimilarMeaningsGroup>();
        
        var i = 0;
        foreach (var sense in datum.Senses)
        {
            var meanings = new TrulyObservableCollection<Meaning>();
            foreach (var definition in sense.EnglishDefinitions)
            {
                meanings.Add(new Meaning(definition, i));
                i++;
            }
            groups.Add(new SimilarMeaningsGroup(meanings));
        }
        
        selection.Meanings.ReplaceAll(groups);
    }

    public void UpdateOtherForms()
    {
        selection.OtherForms.Clear();

        var latestResult = CurrentSession.lastRetrievedResults;
        if (latestResult == null)
            return;
        var selectedDatum = latestResult[selection.SelectedWordsIndex];
        foreach (var japItem in selectedDatum.Japanese)
        {
            var otherForm = !string.IsNullOrEmpty(japItem.Word) ? japItem.Word : japItem.Reading;
            selection.OtherForms.Add(otherForm);
        }
        if (selection.OtherForms.Any())
            selection.SelectedOtherFormsIndex = 0;
        else
            selection.SelectedOtherFormsIndex = -1;

        selection.ReadingOutput = selectedDatum.Japanese.First().Reading;

        StoreMeanings(selectedDatum);

        selection.WriteInKana = selectedDatum.Senses.First().Tags.Contains(JishoTagUsuallyInKanaAlone)
                                || string.IsNullOrEmpty(selectedDatum.Japanese.First().Word);
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

    public bool OnlyOneMeaningInSelection()
    {
        return selection.Meanings.Count == 1 && selection.Meanings.First().SimilarMeanings.Count == 1;
    }

    public void ChangeIsEnabledForAllMeanings(bool isEnabled)
    {
        foreach (var meaningsGroup in selection.Meanings)
        {
            foreach (var meaning in meaningsGroup.SimilarMeanings)
            {
                meaning.IsEnabled = isEnabled;
            }
        }
    }
}