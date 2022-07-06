using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;

namespace JishoTangoAssistant.ui.view
{
    public partial class JishoTangoAssisantWindow : Window
    {
        private JishoTangoAssistantViewModel _jishoTangoAssistantViewModel;

        public JishoTangoAssisantWindow()
        {
            InitializeComponent();
            _jishoTangoAssistantViewModel = new JishoTangoAssistantViewModel();
            DataContext = _jishoTangoAssistantViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Let ViewModel handle closing because the view model knows if the user has saved before
            bool shouldClose = _jishoTangoAssistantViewModel.OnClosingWindow();
            if (!shouldClose)
                e.Cancel = true;
        }

        private void MenuItem_Click(object sender)
        {
            var msgBox = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ContentTitle = "About",
                ContentMessage = "Made by Minh Bang Vu (2022)\n" +
                "\n" +
                "Thanks to the team from jisho.org for making this possible!\n" +
                "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict."
            });
            msgBox.Show();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
