using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Services;
using JishoTangoAssistant.Presentation.UI.Enums;
using JishoTangoAssistant.Presentation.UI.Messages;
using JishoTangoAssistant.Presentation.UI.Utils;
using Microsoft.Extensions.DependencyInjection;
using JapaneseUserInputView = JishoTangoAssistant.Presentation.UI.Views.JapaneseUserInputViews.JapaneseUserInputView;
using VocabularyListView = JishoTangoAssistant.Presentation.UI.Views.VocabularyListViews.VocabularyListView;

namespace JishoTangoAssistant.Presentation.UI.Views;

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
        serviceProvider = Avalonia.Application.Current?.Resources[typeof(IServiceProvider)] as IServiceProvider;
        
        await OverwriteDataFromLoadedFile();

        WeakReferenceMessenger.Default.Send(new UpdateWindowTitleMessage());
    }

    private async Task OverwriteDataFromLoadedFile()
    {
        var fileService = serviceProvider?.GetRequiredService<IFileService>();
        var currentSessionService = serviceProvider?.GetRequiredService<ICurrentSessionService>();
        var vocabularyListService = serviceProvider?.GetRequiredService<IVocabularyListService>();
        
        var loadedFilePath = currentSessionService?.GetLoadedFilePath();

        var loadSuccessful = false;
        if (currentSessionService?.GetUserMadeUnsavedChanges() == false && !string.IsNullOrEmpty(loadedFilePath) && fileService != null)
        {
            var loadedProfile = await fileService.LoadFromFile(loadedFilePath);
            loadSuccessful = loadedProfile != null;
            var loadedVocabularyItems = loadedProfile?.VocabularyItems;
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