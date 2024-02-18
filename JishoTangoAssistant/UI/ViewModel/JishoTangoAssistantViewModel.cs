using JishoTangoAssistant.Model;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;

namespace JishoTangoAssistant.UI.ViewModel;

public class JishoTangoAssistantViewModel : JishoTangoAssistantViewModelBase
{
    public JapaneseUserInputViewModel JapaneseUserInputViewModel { get; } = new JapaneseUserInputViewModel();

    public async Task<bool> OnClosingWindowAsync()
    {
        if (CurrentSession.userMadeChanges)
        {
            var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

            if (mainWindow == null)
                return true;

            var msgBoxResult = await MessageBox.Show(mainWindow, "Warning", "You have made unsaved changes. Do you really want to close the application?",
                MessageBoxButtons.YesNo);
            if (msgBoxResult.Equals(MessageBoxResult.No))
                return false;
        }
        return true;
    }
}
