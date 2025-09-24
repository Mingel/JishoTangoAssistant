using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Services;
using JishoTangoAssistant.Presentation.UI.Services;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListViewModel : JishoTangoAssistantViewModelBase
{
    private VocabularyListDetailsViewModel VocabularyListDetailsViewModel { get; }
    private VocabularyListLoadViewModel VocabularyListLoadViewModel { get; }
    private VocabularyListSaveViewModel VocabularyListSaveViewModel { get; }
    private VocabularyListExportViewModel VocabularyListExportViewModel { get; }
    
    public VocabularyListViewModel(
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService,
        IFileService fileService,
        SaveListUiService saveListUiService)
    {
        VocabularyListDetailsViewModel = new VocabularyListDetailsViewModel(vocabularyListService,
            currentSessionService);
        VocabularyListLoadViewModel = new VocabularyListLoadViewModel(currentSessionService, vocabularyListService,
            fileService);
        VocabularyListSaveViewModel = new VocabularyListSaveViewModel(saveListUiService);
        VocabularyListExportViewModel = new VocabularyListExportViewModel(vocabularyListService,
            currentSessionService);
    }
}
