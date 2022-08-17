using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.Model;
using System.Threading.Tasks;

namespace JishoTangoAssistant.UI.ViewModel
{
    public class JishoTangoAssistantViewModel : JishoTangoAssistantViewModelBase
    {
        public async Task<bool> OnClosingWindowAsync()
        {
            if (CurrentSession.userMadeChanges)
            {
                var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime).MainWindow;
                var msgBoxResult = await View.MessageBox.Show(mainWindow, "Warning", "You have made unsaved changes. Do you really want to close the application?",
                    View.MessageBox.MessageBoxButtons.YesNo);
                if (msgBoxResult.Equals(View.MessageBox.MessageBoxResult.No))
                    return false;
            }
            return true;
        }
    }
}
