using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Presentation.UI.Services;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListSaveViewModel(SaveListUiService saveListUiService) : JishoTangoAssistantViewModelBase
{
    [RelayCommand]
    private async Task SaveList() => await saveListUiService.PerformSave();

    [RelayCommand]
    private async Task SaveAs() => await saveListUiService.PerformSaveAs();
}