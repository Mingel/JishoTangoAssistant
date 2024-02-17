using Avalonia.Controls;
using Avalonia.Interactivity;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View
{
    public partial class JishoTangoAssisantWindow : Window
    {
        private JishoTangoAssistantViewModel jishoTangoAssistantViewModel;
        public static JishoTangoAssisantWindow? Instance;

        public JishoTangoAssisantWindow()
        {
            Instance = this;
            InitializeComponent();
            jishoTangoAssistantViewModel = new JishoTangoAssistantViewModel();
            DataContext = jishoTangoAssistantViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Let ViewModel handle closing because the view model knows if the user has saved before
            bool shouldClose = jishoTangoAssistantViewModel.OnClosingWindowAsync().Result;
            if (!shouldClose)
                e.Cancel = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs args)
        {
            MessageBox.Show(this, "About", "Made by Minh Bang Vu (2022-2023)\n" +
                "\n" +
                "Thanks to the team from jisho.org for making this possible!\n" +
                "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict.",
                MessageBoxButtons.Ok);
        }
    }
}
