using System.Linq;

namespace JishoTangoAssistant.Models;

public class ObservableSimilarMeaningGroup(TrulyObservableCollection<AvailableMeaning> similarMeanings)
{
    public TrulyObservableCollection<AvailableMeaning> SimilarMeanings { get; init; } = similarMeanings;
}