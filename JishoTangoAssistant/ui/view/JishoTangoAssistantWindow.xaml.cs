using System.Windows;

namespace JishoTangoAssistant
{
    /// <summary>
    /// Interaction logic for JishoTangoAssistantWindow.xaml
    /// </summary>
    public partial class JishoTangoAssistantWindow : Window
    {
        private JishoTangoAssistantViewModel _jishoTangoAssistantViewModel;

        public JishoTangoAssistantWindow()
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var about = "Made by Minh Bang Vu (2022)\n" +
                "\n" + 
                "Thanks to the team from jisho.org for making this possible!\n" +
                "Jisho.org uses several data sources, which can be found at jisho.org's About Page. Relevant results from jisho.org are taken from JMdict and JMnedict.";
            MessageBox.Show(about, "About");
        }
    }
}
