using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.Services.Commands;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JishoTangoAssistant.UI.ViewModel
{
    public class JishoTangoAssistantViewModel : JishoTangoAssistantViewModelBase
    {
        private readonly DelegateCommand _undoOperationOnVocabularyList;
        public ICommand UndoOperationOnVocabularyList => _undoOperationOnVocabularyList;

        public JishoTangoAssistantViewModel()
        {
            _undoOperationOnVocabularyList = new DelegateCommand(OnUndoOperationOnVocabularyList, _ => true);
        }

        private void OnUndoOperationOnVocabularyList(Object commandParameter)
        {
            CurrentSession.addedVocabularyItems.Undo();
        }

        public async Task<bool> OnClosingWindowAsync()
        {
            if (CurrentSession.userMadeChanges)
            {
                var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime).MainWindow;
                var msgBoxResult = await MessageBox.Show(mainWindow, "Warning", "You have made unsaved changes. Do you really want to close the application?",
                    MessageBoxButtons.YesNo);
                if (msgBoxResult.Equals(MessageBoxResult.No))
                    return false;
            }
            return true;
        }
    }
}
