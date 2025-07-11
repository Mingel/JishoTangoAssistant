using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Services;
using JishoTangoAssistant.UI.Utils;

namespace JishoTangoAssistant.UI.Services;

public class SaveListService(IVocabularyListService vocabularyListService, ICurrentSessionService currentSessionService, WindowManipulatorService windowManipulatorService)
{
    public async Task PerformSave()
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
}