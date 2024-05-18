using System;
using JishoTangoAssistant.UI.Elements;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Utils;
using Avalonia.Styling;

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
        await MessageBoxUtil.CreateAndShowAsync("About", "Made by Minh Bang Vu (2022-2024)" + Environment.NewLine,
                                                                MessageBoxButtons.Ok,
                                                                "Thanks to the team from jisho.org for making this possible!" + Environment.NewLine +
                                                                "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict.");
    }
}
