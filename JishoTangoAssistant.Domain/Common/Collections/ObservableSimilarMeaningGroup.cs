using JishoTangoAssistant.Domain.Models.Common.Data;

namespace JishoTangoAssistant.Domain.Models.Common.Collections;

public class ObservableSimilarMeaningGroup(TrulyObservableCollection<AvailableMeaning> similarMeanings)
{
    public TrulyObservableCollection<AvailableMeaning> SimilarMeanings { get; init; } = similarMeanings;
}