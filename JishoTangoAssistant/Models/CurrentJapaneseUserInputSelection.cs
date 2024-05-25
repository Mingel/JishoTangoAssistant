using System.Collections.ObjectModel;

namespace JishoTangoAssistant.Models;

public class CurrentJapaneseUserInputSelection
{
    public ObservableCollection<string> Words { get; } = [];
    public ObservableCollection<string> OtherForms { get; } = [];
    public ObservableRangeCollection<ObservableSimilarMeaningGroup> Meanings { get; } = [];
    public string ReadingOutput { get; set; } = string.Empty;
    public bool ItemAdditionPossible { get; set; }
    public int SelectedOtherFormsIndex { get; set; } = -1;
    public int SelectedWordsIndex { get; set; }
    public string AdditionalComments { get; set; } = string.Empty;
    public bool WriteInKana { get; set; }
}