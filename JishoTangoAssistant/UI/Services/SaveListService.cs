using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Utils;
using Newtonsoft.Json;

namespace JishoTangoAssistant.UI.Services;

public class SaveListService(IVocabularyListService vocabularyListService, ICurrentSessionService currentSessionService, WindowManipulatorService windowManipulatorService)
{
    public async Task PerformSaveAs()
    {
        var list = vocabularyListService.GetList();

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };
        
        var startLocationPath = Path.GetDirectoryName(currentSessionService.GetLoadedFilePath());
        var suggestedFileName = Path.GetFileName(currentSessionService.GetLoadedFilePath());
        var result = await FilePicker.SaveAsync(list, "Save vocabulary list as", filePickerFilter, startLocationPath, suggestedFileName);

        if (result != null)
        {
            currentSessionService.SetLoadedFilePath(result.FilePath);
            currentSessionService.SetUserMadeUnsavedChanges(false);
            windowManipulatorService.UpdateTitle();
        }
    }

    public async Task PerformSave()
    {
        var loadedFilePath = currentSessionService.GetLoadedFilePath();
        if (!File.Exists(loadedFilePath))
        {
            Console.WriteLine($"File {loadedFilePath} does not exist, call Save As function");
            await PerformSaveAs();
            return;
        }
        
        var list = vocabularyListService.GetList();
        
        var json = JsonConvert.SerializeObject(list, Formatting.Indented);
        
        var filePath = Uri.UnescapeDataString(loadedFilePath);
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
        await streamWriter.WriteAsync(json);
        
        Console.WriteLine($"File {filePath} saved");
        
        currentSessionService.SetUserMadeUnsavedChanges(false);
        windowManipulatorService.UpdateTitle();
    }
}