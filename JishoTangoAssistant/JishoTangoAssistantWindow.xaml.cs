
using System;
using System.Collections.Generic;
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
            {
                e.Cancel = true;
            }
        }
    }
}
