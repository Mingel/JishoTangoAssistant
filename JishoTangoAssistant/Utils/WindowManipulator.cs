namespace JishoTangoAssistant.Utils;

public static class WindowManipulator
{
    public static void ChangeLoadedFilenameInWindowTitle(string? filePath)
    {
        var mainWindow = App.GetMainWindow();

        if (mainWindow == null)
            return;

        if (filePath == null)
            mainWindow.Title = "JishoTangoAssistant";
        else
            mainWindow.Title = "JishoTangoAssistant - " + filePath;
    }
}