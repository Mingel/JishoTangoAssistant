using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Views;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace JishoTangoAssistant.UI.ViewModels;

public partial class JishoTangoAssistantWindowViewModel(
    JapaneseUserInputViewModel japaneseUserInputViewModel,
    VocabularyListViewModel vocabularyListViewModel)
    : JishoTangoAssistantViewModelBase
{
    [ObservableProperty]
    private JapaneseUserInputViewModel japaneseUserInputViewModel = japaneseUserInputViewModel;
    
    [ObservableProperty]
    private VocabularyListViewModel vocabularyListViewModel = vocabularyListViewModel;
    
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
