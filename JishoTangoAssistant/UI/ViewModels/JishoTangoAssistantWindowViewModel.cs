using System;
using JishoTangoAssistant.UI.Elements;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Styling;
using JishoTangoAssistant.UI.Services;
using JishoTangoAssistant.UI.Utils;
using JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

namespace JishoTangoAssistant.UI.ViewModels;

public partial class JishoTangoAssistantWindowViewModel(
    JapaneseUserInputViewModels.JapaneseUserInputViewModel japaneseUserInputViewModel,
    VocabularyListViewModel vocabularyListViewModel,
    SaveListService saveListService)
    : JishoTangoAssistantViewModelBase
{
    [ObservableProperty]
    private JapaneseUserInputViewModels.JapaneseUserInputViewModel japaneseUserInputViewModel = japaneseUserInputViewModel;
    
    [ObservableProperty]
    private VocabularyListViewModel vocabularyListViewModel = vocabularyListViewModel;

    [RelayCommand]
    private async Task SaveList() => await saveListService.PerformSave();
    
    [RelayCommand]
    private void ToggleLightDarkMode()
    {
        var app = Avalonia.Application.Current;
        if (app == null)
            return;

        app.RequestedThemeVariant = app.ActualThemeVariant == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
    }

    [RelayCommand]
    private async Task OpenAbout()
    {
        await MessageBoxUtil.CreateAndShowAsync("About", "Made by Minh Bang Vu (2022-2025)" + Environment.NewLine,
                                                                MessageBoxButtons.Ok,
                                                                "Thanks to the team from jisho.org for making this possible!" + Environment.NewLine +
                                                                "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict.");
    }
}
