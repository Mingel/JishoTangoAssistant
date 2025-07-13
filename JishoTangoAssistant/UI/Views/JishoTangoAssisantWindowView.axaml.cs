using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Services;
using JishoTangoAssistant.UI.Utils;
using JishoTangoAssistant.UI.Views.JapaneseUserInputViews;
using JishoTangoAssistant.UI.Views.VocabularyListViews;
using Microsoft.Extensions.DependencyInjection;

namespace JishoTangoAssistant.UI.Views;

public partial class JishoTangoAssistantWindowView : Window
{
    private bool userWantsToQuit;
    private IServiceProvider? serviceProvider;

    public JishoTangoAssistantWindowView()
    {
        InitializeComponent();
    }
    
    public UserControl? CurrentlyLoadedControlContent { get; set; }

    protected override async void OnLoaded(RoutedEventArgs e)
    {
        serviceProvider = Application.Current?.Resources[typeof(IServiceProvider)] as IServiceProvider;
        
        await OverwriteDataFromLoadedFile();

        var windowManipulatorService = serviceProvider?.GetRequiredService<WindowManipulatorService>();
        windowManipulatorService?.UpdateTitle();
    }

    private async Task OverwriteDataFromLoadedFile()
    {
        var loadListService = serviceProvider?.GetRequiredService<LoadListService>();
        var currentSessionService = serviceProvider?.GetRequiredService<ICurrentSessionService>();
        var vocabularyListService = serviceProvider?.GetRequiredService<IVocabularyListService>();
        
        var loadedFilePath = currentSessionService?.GetLoadedFilePath();

        var loadSuccessful = false;
        if (currentSessionService?.GetUserMadeUnsavedChanges() == false && !string.IsNullOrEmpty(loadedFilePath) && loadListService != null)
        {
            var loadedVocabularyItems = await loadListService.LoadFromFile(loadedFilePath);
            loadSuccessful = loadedVocabularyItems != null;
            var loadedVocabularyItemList = loadedVocabularyItems?.ToList();

            if (loadedVocabularyItemList != null && vocabularyListService?.SequenceEqual(loadedVocabularyItemList) == false)
            {
                await vocabularyListService.ClearAsync();
                await vocabularyListService.AddRangeAsync(loadedVocabularyItemList);
                Console.WriteLine($"File {currentSessionService.GetLoadedFilePath()} contains different data than in local database despite user not having changes made, local database content overwritten");
                return;
            }
        }
        if (currentSessionService?.GetUserMadeUnsavedChanges() == false && !string.IsNullOrEmpty(loadedFilePath) && !loadSuccessful)
        {
            if (!string.IsNullOrEmpty(loadedFilePath))
            {
                currentSessionService.SetLoadedFilePath(string.Empty);
                currentSessionService.SetUserMadeUnsavedChanges(false);                
            }
            
            if (vocabularyListService != null)
                await vocabularyListService.ClearAsync();
            
            Console.WriteLine($"File {loadedFilePath} could not be loaded, data was reset to initial state");
        }
    }

    protected override async void OnClosing(WindowClosingEventArgs e)
    {
        var currentSessionService = serviceProvider?.GetRequiredService<ICurrentSessionService>();

        if (currentSessionService is null)
        {
            base.OnClosing(e);
            return;
        }
        
        if (!currentSessionService.GetUserMadeUnsavedChanges())
            userWantsToQuit = true;

        if (!userWantsToQuit)
        {
            e.Cancel = true;
            await AskForCloseWindow();
        }

        base.OnClosing(e);
    }

    private async Task AskForCloseWindow()
    {
        var msgBoxResult = await Dispatcher.UIThread.InvokeAsync(() =>
            MessageBoxUtil.CreateAndShowAsync("Warning",
                                              "You have made unsaved changes. Do you really want to close the application?",
                                              MessageBoxButtons.YesNo));
        Dispatcher.UIThread.Post(() => CloseWindowAfterAsking(msgBoxResult == MessageBoxResult.Yes));
    }
    
    public void FocusSelectedContentControlView()
    {
        switch (CurrentlyLoadedControlContent)
        {
            case JapaneseUserInputView { IsLoaded: true, IsFocused: false } japaneseUserInputView:
                japaneseUserInputView.Focus();
                break;
            case VocabularyListView { IsLoaded: true, IsFocused: false } vocabularyListView:
                vocabularyListView.Focus();
                break;
        }
    }

    private void CloseWindowAfterAsking(bool shouldClose)
    {
        userWantsToQuit = shouldClose;
        if (shouldClose)
            Close();
    }
}