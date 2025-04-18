using JishoTangoAssistant.Core.Interfaces;

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
        IWindowManipulatorService windowManipulatorService,
        ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        VocabularyListDetailsViewModel = new VocabularyListDetailsViewModel(vocabularyListService,
            currentSessionService,
            windowManipulatorService,
            currentSelectionService);
        VocabularyListLoadViewModel = new VocabularyListLoadViewModel(vocabularyListService,
            currentSessionService,
            windowManipulatorService);
        VocabularyListSaveViewModel = new VocabularyListSaveViewModel(vocabularyListService,
            currentSessionService,
            windowManipulatorService);
        VocabularyListExportViewModel = new VocabularyListExportViewModel(vocabularyListService,
            currentSessionService);
    }
}
