using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Common.Utils;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Messages;
using JishoTangoAssistant.UI.Utils;

namespace JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels;

public partial class PreEnteredInputViewModel : JishoTangoAssistantViewModelBase
{
    [ObservableProperty]
    private string preEnteredInputRawList = string.Empty;
    
    [RelayCommand]
    private async Task LoadPreEnteredInputListFromFile()
    {
        var filePickerTitle = "Open file to load search queries";

        var loadedFileInfo = await FilePicker.LoadAsync(filePickerTitle);
        var loadedFile = loadedFileInfo?.Content;

        // this case can occur if user cancels file dialog
        if (loadedFile == null)
            return;

        var queryList = loadedFile.Split(Environment.NewLine);
        var filteredQueryList = loadedFile.Split(Environment.NewLine).Where(s => s.Length <= 150).ToArray();
        
        if (filteredQueryList.Length < queryList.Length)
            await MessageBoxUtil.CreateAndShowAsync("Error", $"The file contains {queryList.Length - filteredQueryList.Length} queries will not be used because they are too long!", MessageBoxButtons.Ok);

        if (filteredQueryList.Length == 0)
            return;
        
        // Filter out strings that are too long
        PreEnteredInputRawList = string.Join(Environment.NewLine, filteredQueryList);
        
        HidePreEnteredInputList();
    }
    
    [RelayCommand]
    private void HidePreEnteredInputList()
    {
        if (!string.IsNullOrWhiteSpace(PreEnteredInputRawList))
        {
            var preEnteredInputs = Enumerable.Where<string>(PreEnteredInputRawList.Split(Environment.NewLine), s => !string.IsNullOrWhiteSpace(s)).ToList();
            PreEnteredInputRawList = string.Join(Environment.NewLine, preEnteredInputs.Select(s => s.Trim()));
            WeakReferenceMessenger.Default.Send(new PreEnteredInputsUpdatedMessage(preEnteredInputs));
        }
        WeakReferenceMessenger.Default.Send(new ChangePreEnteredInputViewVisibilityMessage(false));
    }
}