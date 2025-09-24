using System.Collections.ObjectModel;
using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Domain.Models.Common.Collections;
using JishoTangoAssistant.Domain.Models.Core.Models;

namespace JishoTangoAssistant.Application.Core.Interfaces;

public interface ICurrentJapaneseUserInputSelectionService
{
    IEnumerable<JishoDatum> LastRetrievedResults { get; set; }
    ObservableCollection<string> GetWords();
    ObservableCollection<string> GetOtherForms();
    ObservableRangeCollection<ObservableSimilarMeaningGroup> GetMeaningGroups();
    int GetSelectedWordsIndex();
    void SetSelectedWordsIndex(int value);
    int GetSelectedOtherFormsIndex();
    void SetSelectedOtherFormsIndex(int value);
    string GetReadingOutput();
    string GetAdditionalComments();
    void SetAdditionalComments(string value);
    bool GetWriteInKana();
    public void SetWriteInKana(bool value);
    public bool GetItemAdditionPossible();
    public bool IsSelectedWordAndFormIsKanaOnly();
    Task<UpdateSelectionResult> UpdateSelectionAsync(string preprocessedInput);
    VocabularyItem? CreateVocabularyItem();
    void UpdateOtherForms();
    void UpdateReading();
    bool OnlyOneMeaningInSelection();
    void ChangeIsEnabledForAllMeanings(bool isEnabled);
}