using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels;

public partial class AdditionalCommentsViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateAllNonCollectionPropertiesMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private bool isProcessingInput;

    [ObservableProperty]
    private string additionalCommentsJapanese = string.Empty;
    
    public AdditionalCommentsViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        this.currentSelectionService = currentSelectionService;
        WeakReferenceMessenger.Default.Register(this);
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
}