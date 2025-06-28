using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels;

public partial class VocabularyItemAdditionViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateAllNonCollectionPropertiesMessage>, IRecipient<UpdateVisualRelatedPropertiesMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private readonly IVocabularyListService vocabularyListService;
    private readonly ICurrentSessionService currentSessionService;
    private readonly IWindowManipulatorService windowManipulatorService;
    
    [ObservableProperty]
    private bool itemAdditionPossible;

    public VocabularyItemAdditionViewModel(
        ICurrentJapaneseUserInputSelectionService currentSelectionService,
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService,
        IWindowManipulatorService windowManipulatorService)
    {
        this.currentSelectionService = currentSelectionService;
        this.vocabularyListService = vocabularyListService;
        this.currentSessionService = currentSessionService;
        this.windowManipulatorService = windowManipulatorService;
        WeakReferenceMessenger.Default.Register<UpdateAllNonCollectionPropertiesMessage>(this);
        WeakReferenceMessenger.Default.Register<UpdateVisualRelatedPropertiesMessage>(this);
    }
    
    [RelayCommand]
    private async Task AddToList()
    {
        var addedItem = currentSelectionService.CreateVocabularyItem();

        if (addedItem == null)
            return;

        await vocabularyListService.AddAsync(addedItem);
        currentSessionService.SetUserMadeUnsavedChanges(true);
        windowManipulatorService.UpdateTitle();
    }
    
    private void UpdateItemAdditionPossibleProperty(VocabularyItem? itemFromCurrentUserInput)
    {
        ItemAdditionPossible = currentSelectionService.GetItemAdditionPossible() &&
                               itemFromCurrentUserInput != null &&
                               !vocabularyListService.Contains(itemFromCurrentUserInput);
    }

    public void Receive(UpdateAllNonCollectionPropertiesMessage message)
    {
        ItemAdditionPossible = currentSelectionService.GetItemAdditionPossible();
    }

    public void Receive(UpdateVisualRelatedPropertiesMessage message)
    {
        var itemFromCurrentUserInput = currentSelectionService.CreateVocabularyItem();
        UpdateItemAdditionPossibleProperty(itemFromCurrentUserInput);
    }
}