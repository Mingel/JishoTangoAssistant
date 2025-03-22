using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Core.Collections;
using JishoTangoAssistant.Core.Interfaces;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListDetailsViewModel : JishoTangoAssistantViewModelBase
{
    private readonly IVocabularyListService vocabularyListService;
    private readonly ICurrentSessionService currentSessionService;
    private readonly IWindowManipulatorService windowManipulatorService;

    [ObservableProperty] 
    private int selectedVocabItemIndex;

    public VocabularyListDetailsViewModel(
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService,
        IWindowManipulatorService windowManipulatorService)
    {
        this.vocabularyListService = vocabularyListService;
        this.currentSessionService = currentSessionService;
        this.windowManipulatorService = windowManipulatorService;
    }
    
    public ReadOnlyObservableVocabularyList VocabularyList
    {
        get => vocabularyListService.GetList();
        set
        {
            var vocabularyList = vocabularyListService.GetList();
            SetProperty(ref vocabularyList, value);
        }
    }
    
    
    [RelayCommand]
    private async Task DeleteFromList()
    {
        if (0 <= SelectedVocabItemIndex && SelectedVocabItemIndex < VocabularyList.Count)
            await vocabularyListService.RemoveAtAsync(SelectedVocabItemIndex);
        
        currentSessionService.SetUserMadeChanges(true);
        windowManipulatorService.UpdateTitle();
    }
    
    [RelayCommand]
    private async Task DeleteAllFromList()
    {
        currentSessionService.SetLoadedFilePath(null);
        await vocabularyListService.ClearAsync();
        
        currentSessionService.SetUserMadeChanges(true);
        windowManipulatorService.UpdateTitle();
    }

    [RelayCommand]
    private async Task GoUp()
    {
        if (SelectedVocabItemIndex <= 0)
            return;
        await vocabularyListService.SwapAsync(SelectedVocabItemIndex - 1, SelectedVocabItemIndex);
        
        currentSessionService.SetUserMadeChanges(true);
        windowManipulatorService.UpdateTitle();
    }

    [RelayCommand]
    private async Task GoDown()
    {
        if (SelectedVocabItemIndex <= -1 || SelectedVocabItemIndex >= vocabularyListService.Count() - 1) 
            return;
        await vocabularyListService.SwapAsync(SelectedVocabItemIndex, SelectedVocabItemIndex + 1);
        SelectedVocabItemIndex++;
        
        currentSessionService.SetUserMadeChanges(true);
        windowManipulatorService.UpdateTitle();
    }

    [RelayCommand]
    private async Task UndoOperationOnVocabularyList()
    {
        await vocabularyListService.UndoAsync();
        currentSessionService.SetUserMadeChanges(true);
        windowManipulatorService.UpdateTitle();
    }
}