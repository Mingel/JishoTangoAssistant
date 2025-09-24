using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels;

public partial class VocabularyItemAdditionViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateAllNonCollectionPropertiesMessage>, IRecipient<UpdateVisualRelatedPropertiesMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private readonly IVocabularyListService vocabularyListService;
    private readonly ICurrentSessionService currentSessionService;
    
    [ObservableProperty]
    private bool itemAdditionPossible;

    public VocabularyItemAdditionViewModel(
        ICurrentJapaneseUserInputSelectionService currentSelectionService,
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService)
    {
        this.currentSelectionService = currentSelectionService;
        this.vocabularyListService = vocabularyListService;
        this.currentSessionService = currentSessionService;
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
        WeakReferenceMessenger.Default.Send(new UpdateWindowTitleMessage());
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