using JishoTangoAssistant.Model;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;

namespace JishoTangoAssistant.UI.ViewModel
{
    public class JishoTangoAssistantViewModel : JishoTangoAssistantViewModelBase
    {
        public bool OnClosingWindow()
        {
            if (CurrentSession.userMadeChanges)
            {
                var msgBox = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ContentTitle = "Warning",
                    ContentMessage = "You have made unsaved changes. Do you really want to close the application?",
                    Icon = MessageBox.Avalonia.Enums.Icon.Warning,
                    ButtonDefinitions = MessageBox.Avalonia.Enums.ButtonEnum.YesNo
                });
                var userButtonInput = msgBox.Show();
                if (userButtonInput.Equals(MessageBox.Avalonia.Enums.ButtonResult.No))
                    return false;
            }
            return true;
        }
    }
}
