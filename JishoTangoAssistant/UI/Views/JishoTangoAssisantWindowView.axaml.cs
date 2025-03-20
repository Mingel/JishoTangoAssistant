using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Utils;
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

    protected override void OnLoaded(RoutedEventArgs e)
    {
        serviceProvider = Application.Current?.Resources[typeof(IServiceProvider)] as IServiceProvider;
        var windowManipulatorService = serviceProvider?.GetRequiredService<IWindowManipulatorService>();
        windowManipulatorService?.UpdateTitle();
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

    private async Task AskForCloseWindow()
    {
        var msgBoxResult = await Dispatcher.UIThread.InvokeAsync(() =>
            MessageBoxUtil.CreateAndShowAsync("Warning",
                                              "You have made unsaved changes. Do you really want to close the application?",
                                              MessageBoxButtons.YesNo));
        Dispatcher.UIThread.Post(() => CloseWindowAfterAsking(msgBoxResult == MessageBoxResult.Yes));
    }

    private void CloseWindowAfterAsking(bool shouldClose)
    {
        userWantsToQuit = shouldClose;
        if (shouldClose)
            Close();
    }
}