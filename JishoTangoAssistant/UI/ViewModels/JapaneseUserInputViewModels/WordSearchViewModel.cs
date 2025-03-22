using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Common.Utils;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels;

public partial class WordSearchViewModel : JishoTangoAssistantViewModelBase, IRecipient<PreEnteredInputsUpdatedMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private readonly List<string> preEnteredInputs = [];
    
    [ObservableProperty]
    private string input = string.Empty;
    
    [ObservableProperty]
    private int preEnteredInputIndex;
 
    [ObservableProperty]
    private bool isInPreEnteredInputsMode;

    public WordSearchViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        this.currentSelectionService = currentSelectionService;
        WeakReferenceMessenger.Default.Register(this);
    }
    
    public bool PreEnteredInputNoPrevPossible => PreEnteredInputIndex > 0;
    
    public bool PreEnteredInputNoNextPossible => PreEnteredInputIndex < preEnteredInputs.Count - 1;
    
    [RelayCommand]
    private async Task PrevPreEnteredInput()
    {
        if (PreEnteredInputIndex > 0)
        {
            PreEnteredInputIndex--;
            Input = preEnteredInputs[PreEnteredInputIndex];
            await ProcessInput();
            OnPropertyChanged(nameof(PreEnteredInputNoPrevPossible));
            OnPropertyChanged(nameof(PreEnteredInputNoNextPossible));
        }
    }
    
    [RelayCommand]
    private async Task NextPreEnteredInput()
    {
        if (PreEnteredInputIndex < preEnteredInputs.Count - 1)
        {
            PreEnteredInputIndex++;
            Input = preEnteredInputs[PreEnteredInputIndex];
            await ProcessInput();
            OnPropertyChanged(nameof(PreEnteredInputNoPrevPossible));
            OnPropertyChanged(nameof(PreEnteredInputNoNextPossible));
        }
    }
    
    [RelayCommand]
    private void RemovePreEnteredInputs()
    {
        PreEnteredInputIndex = 0;
        preEnteredInputs.Clear();
        IsInPreEnteredInputsMode = false;
    }
    

    [RelayCommand]
    private async Task ProcessInput()
    {
        Input = RomajiKanaConverter.Convert(Input.Trim());
        await currentSelectionService.UpdateSelectionAsync(Input);
        WeakReferenceMessenger.Default.Send(new UpdateAllNonCollectionPropertiesMessage(true));
        WeakReferenceMessenger.Default.Send(new UpdateOutputTextMessage());
        WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
        WeakReferenceMessenger.Default.Send(
            new UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage(currentSelectionService
                .IsSelectedWordAndFormIsKanaOnly()));
    }

    [RelayCommand]
    private void ShowPreEnteredInputList()
    {
        WeakReferenceMessenger.Default.Send(new ChangePreEnteredInputViewVisibilityMessage(true));
    }

    public async void Receive(PreEnteredInputsUpdatedMessage message)
    {
        if (message.Value.Count == 0) 
            return;
            
        preEnteredInputs.Clear();
        preEnteredInputs.AddRange(message.Value);
        Input = preEnteredInputs[PreEnteredInputIndex];
        await ProcessInput();
        IsInPreEnteredInputsMode = true;
        OnPropertyChanged(nameof(PreEnteredInputNoPrevPossible));
        OnPropertyChanged(nameof(PreEnteredInputNoNextPossible));
    }
}