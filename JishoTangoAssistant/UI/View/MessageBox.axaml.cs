using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JishoTangoAssistant.UI.Elements;

namespace JishoTangoAssistant.UI.View
{
    public partial class MessageBox : Window
    {
        private MessageBoxResult selectedResult = MessageBoxResult.Ok;

        public MessageBox()
        {
            InitializeComponent();
        }

        public MessageBox(MessageBoxButtons buttons = MessageBoxButtons.Ok)
        {
            InitializeComponent();

            if (buttons == MessageBoxButtons.Ok || buttons == MessageBoxButtons.OkCancel)
                this.AddButton("OK", MessageBoxResult.Ok, true);
            if (buttons == MessageBoxButtons.YesNo || buttons == MessageBoxButtons.YesNoCancel)
            {
                this.AddButton("Yes", MessageBoxResult.Yes);
                this.AddButton("No", MessageBoxResult.No, true);
            }

            if (buttons == MessageBoxButtons.OkCancel || buttons == MessageBoxButtons.YesNoCancel)
                this.AddButton("Cancel", MessageBoxResult.Cancel, true);

            if (buttons == MessageBoxButtons.MergeOverwriteCancel)
            {
                this.AddButton("Merge", MessageBoxResult.Merge);
                this.AddButton("Overwrite", MessageBoxResult.Overwrite);
                this.AddButton("Cancel", MessageBoxResult.Cancel, true);
            }
        }

        private void AddButton(string caption, MessageBoxResult result, bool isDefault = false)
        {
            var buttonsStackPanel = this.FindControl<StackPanel>("buttonsStackPanel");

            if (buttonsStackPanel == null)
                throw new System.InvalidOperationException("buttonsStackPanel is null");

            var button = new Button { Content = caption };
            button.Click += (_, _) => {
                selectedResult = result;
                this.Close();
            };
            buttonsStackPanel.Children.Add(button);
            if (isDefault)
                selectedResult = result;
        }

        public static Task<MessageBoxResult> Show(Window parent, string title, string text, MessageBoxButtons buttons)
        {
            var messageBox = new MessageBox(buttons);
            messageBox.Title = title;

            var messageBoxTextBlock = messageBox.FindControl<TextBlock>("messageBoxTextBlock");
            if (messageBoxTextBlock == null)
                throw new System.InvalidOperationException("messageBoxTextBlock is null");
            messageBoxTextBlock.Text = text;

            var taskCompletionSource = new TaskCompletionSource<MessageBoxResult>();
            messageBox.Closed += (_, _) => taskCompletionSource.SetResult(messageBox.selectedResult);

            if (parent != null)
                messageBox.ShowDialog(parent);
            else
                messageBox.Show();

            return taskCompletionSource.Task;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
