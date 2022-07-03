using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;

namespace JishoTangoAssistant
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
                msgBox.Show();
                if (msgBox.Equals(MessageBox.Avalonia.Enums.ButtonResult.No))
                    return false;
            }
            return true;
        }
    }
}
