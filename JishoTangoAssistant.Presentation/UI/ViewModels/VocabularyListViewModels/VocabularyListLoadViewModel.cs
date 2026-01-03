using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Services;
using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Presentation.UI.Enums;
using JishoTangoAssistant.Presentation.UI.Messages;
using JishoTangoAssistant.Presentation.UI.Utils;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListLoadViewModel(ICurrentSessionService currentSessionService, IVocabularyListService vocabularyListService, IFileService fileService) : JishoTangoAssistantViewModelBase
{
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
        var loadedFileInfo = await FilePicker.LoadAsync<JishoTangoAssistantProfile>(filePickerTitle, filePickerFilter, startLocationPath);
        var loadedProfile = loadedFileInfo?.Content;
        
        // this case occurs if user cancels file dialog
        if (loadedProfile == null)
            return;
        
        await fileService.PerformLoad(loadedProfile, performOverwriting ?? false, loadedFileInfo?.FilePath ?? string.Empty);
        WeakReferenceMessenger.Default.Send(new UpdateExportSettingsMessage());
        WeakReferenceMessenger.Default.Send(new UpdateWindowTitleMessage());
    }
}