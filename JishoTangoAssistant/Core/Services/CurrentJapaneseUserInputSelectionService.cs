using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using JishoTangoAssistant.Common.Collections;
using JishoTangoAssistant.Common.Data;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.Infrastructure.ApiClients;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Utils;
using WritingSystemUtil = JishoTangoAssistant.Common.Utils.WritingSystemUtil;

namespace JishoTangoAssistant.Core.Services;

public class CurrentJapaneseUserInputSelectionService(IJishoWebService jishoWebService)
    : ICurrentJapaneseUserInputSelectionService
{
    private const string JishoTagUsuallyInKanaAlone = "Usually written using kana alone";
    private readonly CurrentJapaneseUserInputSelection selection = new();

    #region selection-attributes
    public ObservableCollection<string> GetWords() => selection.Words;

    public ObservableCollection<string> GetOtherForms() => selection.OtherForms;

    public ObservableRangeCollection<ObservableSimilarMeaningGroup> GetMeaningGroups() => selection.Meanings;
    
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
        var allResults = await jishoWebService.GetResultAsync(preprocessedInput);
        if (allResults == null || !allResults.Any()) // TODO move out of this service
        {
            ClearUserInputResults();

            await HandleSearchErrorAsync(allResults);
            return;
        }

        CurrentSession.lastRetrievedResults = allResults;

        ClearUserInputResults();

        var resultIndex = 0;
        var entryIndex = 0;

        if (WritingSystemUtil.ContainsKanji(preprocessedInput))
            GetIndicesOfInputInResult(preprocessedInput, allResults, ref resultIndex, ref entryIndex);

        var result = allResults.ElementAt(resultIndex);
        var japaneseEntry = result.Japanese.ElementAt(entryIndex);

        foreach (var res in allResults)
        {
            var firstJapaneseResult = res.Japanese.FirstOrDefault();

            if (firstJapaneseResult == null)
                continue;

            var word = !string.IsNullOrEmpty(firstJapaneseResult.Word) ? firstJapaneseResult.Word : firstJapaneseResult.Reading;
            selection.Words.Add(word);
        }

        var kanjisOnlyInInput = WritingSystemUtil.FilterKanji(preprocessedInput);
        if (selection.Words.Any())
        {
            selection.SelectedWordsIndex = resultIndex;

            foreach (var japItem in result.Japanese)
            {
                var otherForm = !string.IsNullOrEmpty(japItem.Word) ? japItem.Word : japItem.Reading;
                selection.OtherForms.Add(otherForm);
            }

            if (selection.OtherForms.Any())
            {
                // make selection of form dependent on kanjis present in input
                if (string.IsNullOrEmpty(kanjisOnlyInInput))
                {
                    selection.SelectedOtherFormsIndex = entryIndex;
                }
                else
                {
                    selection.SelectedOtherFormsIndex = selection.OtherForms
                        .Select((f, i) => new { Kanjis = WritingSystemUtil.FilterKanji(f), Index = i })
                        .FirstOrDefault(x => x.Kanjis == kanjisOnlyInInput)?
                        .Index ?? entryIndex;
                }
            }
            else
            {
                selection.SelectedOtherFormsIndex = -1;
            }
        }

        selection.ReadingOutput = GetReadingOutput(result, entryIndex);

        StoreMeanings(result);

        selection.ItemAdditionPossible = true;
    }

    private async Task HandleSearchErrorAsync(IEnumerable<JishoDatum>? allResults)
    {
        if (allResults == null) // Application could not retrieve information from Jisho
            await MessageBoxUtil.CreateAndShowAsync("Error", "Information could not be retrieved!", MessageBoxButtons.Ok);
        else
            await MessageBoxUtil.CreateAndShowAsync("Information", "No results were found!", MessageBoxButtons.Ok);
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
        var meanings = selection.Meanings.Select(g => g.SimilarMeanings.Where(m => m.IsEnabled)
                                                                                                                 .Select(m => m.Value))
                                                              .Where(m => m.Any())
                                                              .ToList();
        var additionalComments = !string.IsNullOrWhiteSpace(selection.AdditionalComments) ? selection.AdditionalComments : null;
        var showReading = !selection.WriteInKana;
        var word = showReading ? selection.OtherForms.ElementAt(selection.SelectedOtherFormsIndex) : selection.ReadingOutput;
        return new VocabularyItem(word, showReading, selection.ReadingOutput, meanings, additionalComments);
    }

    private void StoreMeanings(JishoDatum datum)
    {
        var groups = new List<ObservableSimilarMeaningGroup>();
        
        var i = 0;
        foreach (var sense in datum.Senses)
        {
            var meanings = new TrulyObservableCollection<AvailableMeaning>();
            foreach (var definition in sense.EnglishDefinitions)
            {
                meanings.Add(new AvailableMeaning(definition, i));
                i++;
            }
            groups.Add(new ObservableSimilarMeaningGroup(meanings));
        }
        
        selection.Meanings.ReplaceAll(groups);
    }

    public void UpdateOtherForms()
    {
        selection.OtherForms.Clear();

        var latestResult = CurrentSession.lastRetrievedResults;
        if (latestResult == null)
            return;
        var selectedDatum = latestResult.ElementAt(selection.SelectedWordsIndex);
        foreach (var japItem in selectedDatum.Japanese)
        {
            var otherForm = !string.IsNullOrEmpty(japItem.Word) ? japItem.Word : japItem.Reading;
            selection.OtherForms.Add(otherForm);
        }
        if (selection.OtherForms.Any())
            selection.SelectedOtherFormsIndex = 0;
        else
            selection.SelectedOtherFormsIndex = -1;

        StoreMeanings(selectedDatum);

        selection.ReadingOutput = GetReadingOutput(selectedDatum, 0);
    }

    public void UpdateReading()
    {
        if (selection.SelectedOtherFormsIndex < 0 || selection.SelectedOtherFormsIndex >= selection.OtherForms.Count)
        {
            selection.ReadingOutput = string.Empty;
            return;
        }

        var latestResult = CurrentSession.lastRetrievedResults;
        if (latestResult == null)
            return;
        var selectedDatum = latestResult.ElementAt(selection.SelectedWordsIndex);

        selection.ReadingOutput = GetReadingOutput(selectedDatum, selection.SelectedOtherFormsIndex);
    }

    private string GetReadingOutput(JishoDatum selectedDatum, int japaneseItemsIndex = 0)
    {
        var selectedWord = selectedDatum.Japanese.ElementAtOrDefault(japaneseItemsIndex)?.Word;
        selection.WriteInKana = selectedDatum.Senses.FirstOrDefault()?.Tags.Contains(JishoTagUsuallyInKanaAlone) == true
                                || string.IsNullOrEmpty(selectedWord);
        if (selection.WriteInKana && !string.IsNullOrEmpty(selectedWord) && WritingSystemUtil.OnlyContainsKana(selectedWord))
        {
            return selectedWord;
        }
        else
        {
            return selectedDatum.Japanese.ElementAtOrDefault(japaneseItemsIndex)?.Reading ?? string.Empty;
        }
    }
    
    private static void GetIndicesOfInputInResult(string input, IEnumerable<JishoDatum> result, ref int resultIndex, ref int entryIndex)
    {
        ArgumentNullException.ThrowIfNull(result);

        for (var i = 0; i < result.Count(); i++)
        {
            var res = result.ElementAt(i);
            for (var j = 0; j < res.Japanese.Count(); j++)
            {
                var entry = res.Japanese.ElementAt(j).Word;
                
                if (input != entry)
                    continue;
                
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