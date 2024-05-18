using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Views;

namespace JishoTangoAssistant.Utils;

public class MessageBoxUtil {
    public static async Task<MessageBoxResult> CreateAndShowAsync(string title, string text, MessageBoxButtons buttons, string subText = "")
    {
        var parent = App.GetMainWindow();
        return await CreateAndShowAsync(parent, title, text, buttons, subText);
    }

    public static async Task<MessageBoxResult> CreateAndShowAsync(Window? parent, string title, string text, MessageBoxButtons buttons, string subText = "")
    {
        ArgumentNullException.ThrowIfNull(subText);


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
        messageBox.Closed += (_, _) => taskCompletionSource.SetResult(messageBox.SelectedResult);

        if (parent != null)
            await messageBox.ShowDialog(parent);
        else
            messageBox.Show();

        return await taskCompletionSource.Task;
    }
}