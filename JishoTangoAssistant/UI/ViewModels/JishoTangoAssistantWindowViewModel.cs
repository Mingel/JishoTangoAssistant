using System;
using Avalonia.Controls.ApplicationLifetimes;
using JishoTangoAssistant.UI.Elements;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Services;
using JishoTangoAssistant.Utils;

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
    
    [RelayCommand]
    private async Task OpenAbout()
    {
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

        if (mainWindow == null)
            return;

        await MessageBoxUtil.CreateAndShowAsync(mainWindow, "About", "Made by Minh Bang Vu (2022-2024)" + Environment.NewLine,
                                                                MessageBoxButtons.Ok,
                                                                "Thanks to the team from jisho.org for making this possible!" + Environment.NewLine +
                                                                "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict.");
    }
}
