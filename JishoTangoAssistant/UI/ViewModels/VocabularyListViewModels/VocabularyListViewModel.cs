using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Services;
using JishoTangoAssistant.UI.Services;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListViewModel : JishoTangoAssistantViewModelBase
{
    private VocabularyListDetailsViewModel VocabularyListDetailsViewModel { get; }
    private VocabularyListLoadViewModel VocabularyListLoadViewModel { get; }
    private VocabularyListSaveViewModel VocabularyListSaveViewModel { get; }
    private VocabularyListExportViewModel VocabularyListExportViewModel { get; }
    
    public VocabularyListViewModel(
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService,
        WindowManipulatorService windowManipulatorService,
        SaveListService saveListService,
        LoadListService loadListService)
    {
        VocabularyListDetailsViewModel = new VocabularyListDetailsViewModel(vocabularyListService,
            currentSessionService,
            windowManipulatorService);
        VocabularyListLoadViewModel = new VocabularyListLoadViewModel(loadListService);
        VocabularyListSaveViewModel = new VocabularyListSaveViewModel(saveListService);
        VocabularyListExportViewModel = new VocabularyListExportViewModel(vocabularyListService,
            currentSessionService);
    }
}
