using System.Threading.Tasks;
using Avalonia.Controls;
using JishoTangoAssistant.UI.Elements;

namespace JishoTangoAssistant.UI.View;

public partial class MessageBox : Window
{
    private MessageBoxResult selectedResult = MessageBoxResult.Ok;

    private MessageBox(MessageBoxButtons buttons = MessageBoxButtons.Ok)
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
            throw new System.InvalidOperationException("buttonsStackPanel is null");

        var button = new Button { Content = caption };
        button.Click += (_, _) => {
            selectedResult = result;
            Close();
        };
        ButtonsStackPanel.Children.Add(button);
        if (isDefault)
            selectedResult = result;
    }

    public static Task<MessageBoxResult> Show(Window? parent, string title, string text, MessageBoxButtons buttons)
    {
        var messageBox = new MessageBox(buttons)
        {
            Title = title
        };

        var messageBoxTextBlock = messageBox.MessageBoxTextBlock;
        if (messageBoxTextBlock == null)
            throw new System.InvalidOperationException("MessageBoxTextBlock is null");
        messageBoxTextBlock.Text = text;

        var taskCompletionSource = new TaskCompletionSource<MessageBoxResult>();
        messageBox.Closed += (_, _) => taskCompletionSource.SetResult(messageBox.selectedResult);

        if (parent != null)
            messageBox.ShowDialog(parent);
        else
            messageBox.Show();

        return taskCompletionSource.Task;
    }
}