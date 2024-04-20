using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.Utils;

namespace JishoTangoAssistant.UI.Views;

public partial class JishoTangoAssistantWindowView : Window
{
    private bool userWantsToQuit;

    public JishoTangoAssistantWindowView()
    {
        InitializeComponent();
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (!CurrentSession.userMadeChanges)
            userWantsToQuit = true;

        if (!userWantsToQuit)
        {
            e.Cancel = true;
            Task.Run(AskForCloseWindow);
        }

        base.OnClosing(e);
    }

    private async void AskForCloseWindow()
    {
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

        if (mainWindow == null)
            userWantsToQuit = true;

        var msgBoxResult = await Dispatcher.UIThread.InvokeAsync(() =>
            MessageBoxUtil.CreateAndShowAsync(mainWindow,
                                                "Warning",
                                                "You have made unsaved changes. Do you really want to close the application?",
                                                MessageBoxButtons.YesNo));
        var shouldClose = msgBoxResult.Equals(MessageBoxResult.Yes);
        Dispatcher.UIThread.Post(() => CloseWindowAfterAsking(shouldClose));
    }

    private void CloseWindowAfterAsking(bool shouldClose)
    {
        userWantsToQuit = shouldClose;
        if (shouldClose)
            Close();
    }
}