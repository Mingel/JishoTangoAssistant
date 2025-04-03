using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels;

public partial class AdditionalCommentsViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateAllNonCollectionPropertiesMessage>, IRecipient<EditVocabularyItemMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private bool isProcessingInput;

    [ObservableProperty]
    private string additionalCommentsJapanese = string.Empty;
    
    public AdditionalCommentsViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        this.currentSelectionService = currentSelectionService;
        WeakReferenceMessenger.Default.Register<UpdateAllNonCollectionPropertiesMessage>(this);
        WeakReferenceMessenger.Default.Register<EditVocabularyItemMessage>(this);
    }
    
    partial void OnAdditionalCommentsJapaneseChanged(string value) 
    {
        currentSelectionService.SetAdditionalComments(value);
        if (!isProcessingInput)
        {
            WeakReferenceMessenger.Default.Send(new UpdateOutputTextMessage());
            WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
        }
    }

    public void Receive(UpdateAllNonCollectionPropertiesMessage message)
    {
        isProcessingInput = message.Value;
        AdditionalCommentsJapanese = currentSelectionService.GetAdditionalComments();
        isProcessingInput = false;
    }

    public void Receive(EditVocabularyItemMessage message)
    {
        AdditionalCommentsJapanese = message.Value.AdditionalCommentsJapanese ?? string.Empty;
    }
}