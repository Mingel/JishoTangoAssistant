using JishoTangoAssistant.Common.Data;


namespace JishoTangoAssistant.Common.Collections;

public class ObservableSimilarMeaningGroup(TrulyObservableCollection<AvailableMeaning> similarMeanings)
{
    public TrulyObservableCollection<AvailableMeaning> SimilarMeanings { get; init; } = similarMeanings;
}