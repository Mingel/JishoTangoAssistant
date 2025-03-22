using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels;

public partial class WriteKanaViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateAllNonCollectionPropertiesMessage>, IRecipient<UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private bool isProcessingInput;
    
    [ObservableProperty]
    private bool writeInKana;
    
    [ObservableProperty]
    private bool selectedWordAndFormIsKanaOnly;

    public WriteKanaViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        this.currentSelectionService = currentSelectionService;
        WeakReferenceMessenger.Default.Register<UpdateAllNonCollectionPropertiesMessage>(this);
        WeakReferenceMessenger.Default.Register<UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage>(this);
    }
    
    partial void OnWriteInKanaChanged(bool value) 
    {
        currentSelectionService.SetWriteInKana(value);
        if (!isProcessingInput)
        {
            WeakReferenceMessenger.Default.Send(new UpdateOutputTextMessage());
            WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
        }
    }

    public void Receive(UpdateAllNonCollectionPropertiesMessage message)
    {
        isProcessingInput = message.Value;
        WriteInKana = currentSelectionService.GetWriteInKana();
        isProcessingInput = false;
    }

    public void Receive(UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage message)
    {
        SelectedWordAndFormIsKanaOnly = message.Value;
    }
}