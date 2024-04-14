using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using JishoTangoAssistant.UI.Elements;

namespace JishoTangoAssistant.UI.Views;

public partial class MessageBox : Window
{
    private MessageBoxResult selectedResult = MessageBoxResult.Ok;

    public MessageBox() : this(MessageBoxButtons.Ok) {}

    public MessageBox(MessageBoxButtons buttons)
    {
        InitializeComponent();

        switch (buttons)
        {
            case MessageBoxButtons.Ok or MessageBoxButtons.OkCancel:
                AddButton("OK", MessageBoxResult.Ok, true);
                break;
            case MessageBoxButtons.YesNo or MessageBoxButtons.YesNoCancel:
                AddButton("Yes", MessageBoxResult.Yes);
                AddButton("No", MessageBoxResult.No, true);
                break;
        }

        switch (buttons)
        {
            case MessageBoxButtons.OkCancel or MessageBoxButtons.YesNoCancel:
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

        var button = new Button { Content = caption };
        button.Click += (_, _) => {
            selectedResult = result;
            Close();
        };
        ButtonsStackPanel.Children.Add(button);
        if (isDefault)
            selectedResult = result;
    }

    public static Task<MessageBoxResult> Show(Window? parent, string title, string text, MessageBoxButtons buttons, string subText = "")
    {
        if (subText == null) throw new ArgumentNullException(nameof(subText));
        var messageBox = new MessageBox(buttons)
        {
            Title = title,
            MessageBoxTextBlock =
            {
                Text = text
            },
            MessageBoxSubTextBlock =
            {
                IsVisible = !string.IsNullOrEmpty(subText),
                Text = subText
            }
        };

        var taskCompletionSource = new TaskCompletionSource<MessageBoxResult>();
        messageBox.Closed += (_, _) => taskCompletionSource.SetResult(messageBox.selectedResult);

        if (parent != null)
            messageBox.ShowDialog(parent);
        else
            messageBox.Show();

        return taskCompletionSource.Task;
    }
}