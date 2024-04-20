using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.Interfaces;

public interface ICurrentJapaneseUserInputSelectionService
{
    ObservableCollection<string> GetWords();
    ObservableCollection<string> GetOtherForms();
    ObservableRangeCollection<SimilarMeaningGroup> GetMeaningGroups();
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
    bool OnlyOneMeaningInSelection();
    void ChangeIsEnabledForAllMeanings(bool isEnabled);
}