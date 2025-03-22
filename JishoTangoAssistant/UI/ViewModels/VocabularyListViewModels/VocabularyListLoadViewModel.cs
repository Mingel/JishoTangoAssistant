using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Common.Utils;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Utils;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListLoadViewModel : JishoTangoAssistantViewModelBase
{
    private readonly IVocabularyListService vocabularyListService;
    private readonly ICurrentSessionService currentSessionService;
    private readonly IWindowManipulatorService windowManipulatorService;

    public VocabularyListLoadViewModel(
        IVocabularyListService vocabularyListService,
        ICurrentSessionService currentSessionService,
        IWindowManipulatorService windowManipulatorService)
    {
        this.vocabularyListService = vocabularyListService;
        this.currentSessionService = currentSessionService;
        this.windowManipulatorService = windowManipulatorService;
    }

    [RelayCommand]
    private async Task LoadList()
    {
        bool? performOverwriting = null;
        if (vocabularyListService.Count() > 0)
        {
            var msgBoxResult = await MessageBoxUtil.CreateAndShowAsync("Warning", "Your vocabulary list is not empty." + Environment.NewLine + "Do you want to overwrite or merge into your current vocabulary list?",
                MessageBoxButtons.MergeOverwriteCancel);

            if (msgBoxResult == MessageBoxResult.Cancel)
                return;
            performOverwriting = msgBoxResult == MessageBoxResult.Overwrite;
        }

        var filePickerTitle = performOverwriting switch
        {
            true => "Open file to load vocabulary list (Overwrite)",
            false => "Open file to load vocabulary list (Merge)",
            _ => "Open file to load vocabulary list"
        };

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };

        var startLocationPath = Path.GetDirectoryName(currentSessionService.GetLoadedFilePath());
        var loadedFileInfo = await FilePicker.LoadAsync<VocabularyItem>(filePickerTitle, filePickerFilter, startLocationPath);
        var loadedVocabularyItems = loadedFileInfo?.Content;
        
        // this case can occur if user cancels file dialog
        if (loadedVocabularyItems == null)
            return;

        if (performOverwriting == true)
            await vocabularyListService.ClearAsync();
        await vocabularyListService.AddRangeAsync(loadedVocabularyItems, true);
        currentSessionService.SetLoadedFilePath(loadedFileInfo?.FilePath);
        currentSessionService.SetUserMadeChanges(false);
        windowManipulatorService.UpdateTitle();
    }
}