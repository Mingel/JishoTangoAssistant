using System;
using Avalonia.Controls;
using JishoTangoAssistant.Presentation.UI.Enums;

namespace JishoTangoAssistant.Presentation.UI.Views;

public partial class MessageBox : Window
{
    public MessageBoxResult SelectedResult { get; set; } = MessageBoxResult.Ok;

    public MessageBox() : this(MessageBoxButtons.Ok) {}

    public MessageBox(MessageBoxButtons buttons)
    {
        InitializeComponent();

        switch (buttons)
        {
            case MessageBoxButtons.Ok:
                AddButton("OK", MessageBoxResult.Ok, true);
                break;
            case MessageBoxButtons.OkCancel:
                AddButton("OK", MessageBoxResult.Ok);
                AddButton("Cancel", MessageBoxResult.Cancel, true);
                break;
            case MessageBoxButtons.YesNo:
                AddButton("Yes", MessageBoxResult.Yes);
                AddButton("No", MessageBoxResult.No, true);
                break;
            case MessageBoxButtons.YesNoCancel:
                AddButton("Yes", MessageBoxResult.Yes);
                AddButton("No", MessageBoxResult.No);
                AddButton("Cancel", MessageBoxResult.Cancel, true);
                break;
            case MessageBoxButtons.MergeOverwriteCancel:
                AddButton("Merge", MessageBoxResult.Merge);
                AddButton("Overwrite", MessageBoxResult.Overwrite);
                AddButton("Cancel", MessageBoxResult.Cancel, true);
                break;
        }
    }

    private void AddButton(string caption, MessageBoxResult result, bool isDefault = false)
    {
        if (ButtonsStackPanel == null)
            throw new InvalidOperationException("buttonsStackPanel is null");

        var button = new Button { Content = caption, IsDefault = isDefault };
        button.Click += (_, _) => {
            SelectedResult = result;
            Close();
        };
        ButtonsStackPanel.Children.Add(button);
        if (isDefault)
            SelectedResult = result;
    }

    protected override void OnClosed(EventArgs e)
    {
        (App.GetMainWindow() as JishoTangoAssistantWindowView)?.FocusSelectedContentControlView();
        base.OnClosed(e);
    }
}