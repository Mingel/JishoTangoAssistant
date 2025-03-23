using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using Avalonia.VisualTree;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Utils;
using JishoTangoAssistant.UI.Views.JapaneseUserInputViews;
using JishoTangoAssistant.UI.Views.VocabularyListViews;
using Microsoft.Extensions.DependencyInjection;

namespace JishoTangoAssistant.UI.Views;

public partial class JishoTangoAssistantWindowView : Window
{
    private bool userWantsToQuit;
    private IServiceProvider? serviceProvider;
    private UserControl? currentUserControl;

    public JishoTangoAssistantWindowView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        serviceProvider = Application.Current?.Resources[typeof(IServiceProvider)] as IServiceProvider;
        var windowManipulatorService = serviceProvider?.GetRequiredService<IWindowManipulatorService>();
        windowManipulatorService?.UpdateTitle();
        
        currentUserControl = (ViewTabControl.SelectedItem as TabItem)?.GetLogicalDescendants()
            .FirstOrDefault()?
            .GetLogicalDescendants()
            .FirstOrDefault(element => element.GetType() == typeof(ContentControl))?
            .GetLogicalDescendants()
            .FirstOrDefault() as UserControl;
    }
    
    protected override async void OnClosing(WindowClosingEventArgs e)
    {
        var currentSessionService = serviceProvider?.GetRequiredService<ICurrentSessionService>();

        if (currentSessionService is null)
        {
            base.OnClosing(e);
            return;
        }
        
        if (!currentSessionService.GetUserMadeChanges())
            userWantsToQuit = true;

        if (!userWantsToQuit)
        {
            e.Cancel = true;
            await AskForCloseWindow();
        }

        base.OnClosing(e);
    }

    private void OnTabSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is TabControl { SelectedItem: TabItem selectedTab })
        {
            currentUserControl = selectedTab.GetLogicalDescendants()
                .FirstOrDefault(element => element.GetType() == typeof(ContentControl))?
                .GetLogicalDescendants()
                .FirstOrDefault() as UserControl; 
        }
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
        switch (currentUserControl)
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