using System;
using System.Threading.Tasks;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Presentation.UI.Enums;
using JishoTangoAssistant.Presentation.UI.Messages;
using JishoTangoAssistant.Presentation.UI.Services;
using JishoTangoAssistant.Presentation.UI.Utils;
using JishoTangoAssistant.Presentation.UI.ViewModels.VocabularyListViewModels;
using JapaneseUserInputViewModel = JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels.JapaneseUserInputViewModel;

namespace JishoTangoAssistant.Presentation.UI.ViewModels;

public partial class JishoTangoAssistantWindowViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateWindowTitleMessage>
{
    [ObservableProperty]
    private JapaneseUserInputViewModel japaneseUserInputViewModel;
    
    [ObservableProperty]
    private VocabularyListViewModel vocabularyListViewModel;

    [ObservableProperty]
    private string windowTitle = "JishoTangoAssistant";

    private readonly SaveListUiService saveListUiService;
    private readonly ICurrentSessionService currentSessionService;

    public JishoTangoAssistantWindowViewModel(JapaneseUserInputViewModel japaneseUserInputViewModel,
        VocabularyListViewModel vocabularyListViewModel,
        SaveListUiService saveListUiService,
        ICurrentSessionService currentSessionService)
    {
        this.saveListUiService = saveListUiService;
        this.currentSessionService = currentSessionService;
        this.japaneseUserInputViewModel = japaneseUserInputViewModel;
        this.vocabularyListViewModel = vocabularyListViewModel;
        WeakReferenceMessenger.Default.Register(this);
    }

    [RelayCommand]
    private async Task SaveList() => await saveListUiService.PerformSave();

    [RelayCommand]
    private async Task SaveAs() => await saveListUiService.PerformSaveAs();

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

    public void Receive(UpdateWindowTitleMessage message)
    {
        var filePath = currentSessionService.GetLoadedFilePath();
        var changesMade = currentSessionService.GetUserMadeUnsavedChanges();

        if (string.IsNullOrEmpty(filePath))
            WindowTitle = "JishoTangoAssistant";
        else
            WindowTitle = "JishoTangoAssistant - " + filePath;
        
        if (changesMade)
            WindowTitle += "*";
    }
}
