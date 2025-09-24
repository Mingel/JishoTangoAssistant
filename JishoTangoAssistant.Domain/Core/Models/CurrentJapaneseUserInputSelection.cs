using System.Collections.ObjectModel;
using JishoTangoAssistant.Domain.Models.Common.Collections;

namespace JishoTangoAssistant.Domain.Models.Core.Models;

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