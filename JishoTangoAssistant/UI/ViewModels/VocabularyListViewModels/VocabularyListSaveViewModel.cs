using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.UI.Services;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListSaveViewModel(SaveListService saveListService) : JishoTangoAssistantViewModelBase
{
    [RelayCommand]
    private async Task SaveList() => await saveListService.PerformSave();
    
    [RelayCommand]
    private async Task SaveAs() => await saveListService.PerformSaveAs();
}