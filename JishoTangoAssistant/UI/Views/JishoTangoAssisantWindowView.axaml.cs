using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using JishoTangoAssistant.UI.Elements;

namespace JishoTangoAssistant.UI.Views;

public partial class JishoTangoAssistantWindowView : Window
{
    public static JishoTangoAssistantWindowView? Instance;

    public JishoTangoAssistantWindowView()
    {
        Instance = this;
        InitializeComponent();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        // TODO FIX
        // Let ViewModel handle closing because the view model knows if the user has saved before
        // var shouldClose = jishoTangoAssistantWindowViewModel.OnClosingWindowAsync().Result;
        //if (!shouldClose)
        //    e.Cancel = true;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs args)
    {
        MessageBox.Show(this, "About", "Made by Minh Bang Vu (2022-2024)" + Environment.NewLine,
                        MessageBoxButtons.Ok,
                        "Thanks to the team from jisho.org for making this possible!" + Environment.NewLine +
                        "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict.");
    }
}