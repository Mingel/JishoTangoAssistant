using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Utils;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListSaveViewModel : JishoTangoAssistantViewModelBase
{
    private readonly ICurrentSessionService currentSessionService;
    private readonly IWindowManipulatorService windowManipulatorService;
    private readonly IVocabularyListService vocabularyListService;

    public VocabularyListSaveViewModel(
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService,
        IWindowManipulatorService windowManipulatorService)
    {
        this.currentSessionService = currentSessionService;
        this.windowManipulatorService = windowManipulatorService;
        this.vocabularyListService = vocabularyListService;
    }
    
    [RelayCommand]
    private async Task SaveList()
    {
        var list = vocabularyListService.GetList();

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };
        
        var startLocationPath = Path.GetDirectoryName(currentSessionService.GetLoadedFilePath());
        var suggestedFileName = Path.GetFileName(currentSessionService.GetLoadedFilePath());
        var result = await FilePicker.SaveAsync(list, "Save vocabulary list as", filePickerFilter, startLocationPath, suggestedFileName);

        if (result != null)
        {
            currentSessionService.SetLoadedFilePath(result.FilePath);
            currentSessionService.SetUserMadeChanges(false);
            windowManipulatorService.UpdateTitle();
        }
    }
}