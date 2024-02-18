using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using System.Threading.Tasks;

namespace JishoTangoAssistant.UI.ViewModel;

public class JishoTangoAssistantViewModel : JishoTangoAssistantViewModelBase
{
    public async Task<bool> OnClosingWindowAsync()
    {
        if (!CurrentSession.userMadeChanges) return true;
        
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

        if (mainWindow == null)
            return true;

        var msgBoxResult = await MessageBox.Show(mainWindow, "Warning", "You have made unsaved changes. Do you really want to close the application?",
            MessageBoxButtons.YesNo);
        return !msgBoxResult.Equals(MessageBoxResult.No);
    }
}
