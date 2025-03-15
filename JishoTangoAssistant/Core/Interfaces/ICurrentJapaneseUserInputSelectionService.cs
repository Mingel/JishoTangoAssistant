using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JishoTangoAssistant.Common.Collections;
using JishoTangoAssistant.Core.Models;


namespace JishoTangoAssistant.Core.Interfaces;

public interface ICurrentJapaneseUserInputSelectionService
{
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
    Task UpdateSelectionAsync(string preprocessedInput);
    VocabularyItem? CreateVocabularyItem();
    void UpdateOtherForms();
    void UpdateReading();
    bool OnlyOneMeaningInSelection();
    void ChangeIsEnabledForAllMeanings(bool isEnabled);
}