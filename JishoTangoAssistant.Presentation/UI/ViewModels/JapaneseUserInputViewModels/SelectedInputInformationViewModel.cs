using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Utils;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels;

public partial class SelectedInputInformationViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateAllNonCollectionPropertiesMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private bool isProcessingInput;
    
    [ObservableProperty]
    private ObservableCollection<string> words = [];
    
    [ObservableProperty]
    private int selectedIndexOfWords = -1;

    [ObservableProperty]
    private ObservableCollection<string> otherForms = [];

    [ObservableProperty]
    private int selectedIndexOfOtherForms = -1;

    [ObservableProperty]
    private int selectedVocabItemIndex = -1;

    [ObservableProperty]
    private string readingOutput = string.Empty;
    
    public SelectedInputInformationViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        this.currentSelectionService = currentSelectionService;
        
        Words = currentSelectionService.GetWords();
        OtherForms = currentSelectionService.GetOtherForms();
        WeakReferenceMessenger.Default.Register(this);
    }
    
    partial void OnReadingOutputChanged(string value) => WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());

    partial void OnSelectedIndexOfWordsChanged(int value) 
    {
        if (value >= 0)
        {
            currentSelectionService.SetSelectedWordsIndex(value);

            if (!isProcessingInput)
            {
                currentSelectionService.UpdateOtherForms();
                currentSelectionService.SetSelectedOtherFormsIndex(0);

                WeakReferenceMessenger.Default.Send(new UpdateAllNonCollectionPropertiesMessage(false));
                WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
                WeakReferenceMessenger.Default.Send(new UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage(
                    0 <= SelectedIndexOfWords && SelectedIndexOfWords < Words.Count
                                              && 0 <= SelectedIndexOfOtherForms && SelectedIndexOfOtherForms < OtherForms.Count
                                              && WritingSystemUtil.OnlyContainsKana(Words[SelectedIndexOfWords]) && WritingSystemUtil.OnlyContainsKana(OtherForms[SelectedIndexOfOtherForms]))
                );
            }
        }
    }

    partial void OnSelectedIndexOfOtherFormsChanged(int value) 
    {
        if (value >= 0)
        {
            currentSelectionService.SetSelectedOtherFormsIndex(value);

            if (!isProcessingInput)
            {
                currentSelectionService.UpdateReading();

                WeakReferenceMessenger.Default.Send(new UpdateAllNonCollectionPropertiesMessage(false));
                WeakReferenceMessenger.Default.Send(new UpdateOutputTextMessage());
                WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
                WeakReferenceMessenger.Default.Send(new UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage(
                    0 <= SelectedIndexOfWords && SelectedIndexOfWords < Words.Count
                                              && 0 <= SelectedIndexOfOtherForms &&
                                              SelectedIndexOfOtherForms < OtherForms.Count
                                              && WritingSystemUtil.OnlyContainsKana(Words[SelectedIndexOfWords]) &&
                                              WritingSystemUtil.OnlyContainsKana(OtherForms[SelectedIndexOfOtherForms]))
                );
            }
        }
    }

    public void Receive(UpdateAllNonCollectionPropertiesMessage message)
    {
        isProcessingInput = message.Value;
        SelectedIndexOfWords = currentSelectionService.GetSelectedWordsIndex();
        SelectedIndexOfOtherForms = currentSelectionService.GetSelectedOtherFormsIndex();
        ReadingOutput = currentSelectionService.GetReadingOutput();
        isProcessingInput = false;
    }
}