using System.Windows;

namespace JishoTangoAssistant
{
    public class JishoTangoAssistantViewModel : JishoTangoAssistantViewModelBase
    {
        public bool OnClosingWindow()
        {
            if (CurrentSession.userMadeChanges)
            {
                var messageBox = MessageBox.Show("You have made unsaved changes. Do you really want to close the application?",
                                                    "Warning",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning);
                if (messageBox.Equals(MessageBoxResult.No))
                    return false;
            }
            return true;
        }
    }
}
