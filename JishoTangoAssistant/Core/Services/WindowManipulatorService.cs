using JishoTangoAssistant.Core.Interfaces;

namespace JishoTangoAssistant.Core.Services;

public class WindowManipulatorService(ICurrentSessionService currentSessionService) : IWindowManipulatorService
{
    public void UpdateTitle()
    {
        var mainWindow = App.GetMainWindow();

        if (mainWindow == null)
            return;

        var filePath = currentSessionService.GetLoadedFilePath();
        var changesMade = currentSessionService.GetUserMadeChanges();

        if (string.IsNullOrEmpty(filePath))
            mainWindow.Title = "JishoTangoAssistant";
        else
            mainWindow.Title = "JishoTangoAssistant - " + filePath;
        
        if (changesMade)
            mainWindow.Title += "*";
    }
}