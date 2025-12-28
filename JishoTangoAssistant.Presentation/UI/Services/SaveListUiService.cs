using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Utils;
using JishoTangoAssistant.Presentation.UI.Messages;
using JishoTangoAssistant.Presentation.UI.Utils;

namespace JishoTangoAssistant.Presentation.UI.Services;

public class SaveListUiService(IVocabularyListService vocabularyListService, ICurrentSessionService currentSessionService, IFileService fileService)
{
    public async Task PerformSaveAs()
    {
        var list = vocabularyListService.GetList();

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };

        var loadedFilePath = currentSessionService.GetLoadedFilePath();
        var startLocationPath = Path.GetDirectoryName(loadedFilePath);
        var suggestedFileName = Path.GetFileNameWithoutExtension(loadedFilePath);
        var filePath = await FilePicker.GetFilePathForSaveAsync("Save vocabulary list as", filePickerFilter, startLocationPath, suggestedFileName);

        if (filePath != null)
        {
            await fileService.PerformSave(list, filePath);            
            WeakReferenceMessenger.Default.Send(new UpdateWindowTitleMessage());
        }
    }

    public async Task PerformSave()
    {
        var loadedFilePath = currentSessionService.GetLoadedFilePath();
        if (!File.Exists(loadedFilePath))
        {
            Console.WriteLine($"File {loadedFilePath} does not exist, call Save As function");
            await PerformSaveAs();
        }
        else
        {
            var list = vocabularyListService.GetList();
            await fileService.PerformSave(list, loadedFilePath);            
            WeakReferenceMessenger.Default.Send(new UpdateWindowTitleMessage());    
        }
    }
    
    public async Task<bool> PerformExportAsCsv(bool toJapanese)
    {
        var list = vocabularyListService.GetList();
        var contentToExport = toJapanese ? VocabularyListExporter.EnglishToJapanese(list, currentSessionService.GetExportSettings()) : VocabularyListExporter.JapaneseToEnglish(list, currentSessionService.GetExportSettings());

        var filePickerFilter = new[] {
            new FilePickerFileType("CSV Files") { Patterns = ["*.csv"] }
        };
        
        var loadedFilePath = currentSessionService.GetLoadedFilePath();
        var startLocationPath = Path.GetDirectoryName(loadedFilePath);
        var suggestedFileName = Path.GetFileNameWithoutExtension(loadedFilePath);
        var filePath = await FilePicker.GetFilePathForSaveAsync("Export vocabulary list as", filePickerFilter, startLocationPath, suggestedFileName);

        if (filePath == null)
            return false;
        
        await fileService.PerformExport(contentToExport, filePath);
        return true;
    }
}