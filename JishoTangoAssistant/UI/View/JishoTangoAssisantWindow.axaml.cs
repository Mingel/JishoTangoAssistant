using Avalonia.Controls;
using Avalonia.Interactivity;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View
{
    public partial class JishoTangoAssisantWindow : Window
    {
        private JishoTangoAssistantViewModel _jishoTangoAssistantViewModel;
        public static JishoTangoAssisantWindow Instance;

        public JishoTangoAssisantWindow()
        {
            Instance = this;
            InitializeComponent();
            _jishoTangoAssistantViewModel = new JishoTangoAssistantViewModel();
            DataContext = _jishoTangoAssistantViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Let ViewModel handle closing because the view model knows if the user has saved before
            bool shouldClose = _jishoTangoAssistantViewModel.OnClosingWindowAsync().Result;
            if (!shouldClose)
                e.Cancel = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl).SelectedItem == null)
                return;

            string tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

            switch (tabItem)
            {
                case "Vocabulary List":
                    // TEMPORARY FIX: Because without the fix, the data grid does not update when adding new items,
                    //                we force an update by re-assigning the vocabulary list to the data grid...
                    //                ...might be related to https://github.com/AvaloniaUI/Avalonia/issues/9527
                    // TODO APPARENTLY THE FIX IS OUT
                    var gridItems = vocabularyListView.vocabularyItemsDataGrid.ItemsSource;
                    vocabularyListView.vocabularyItemsDataGrid.ItemsSource = null;
                    vocabularyListView.vocabularyItemsDataGrid.ItemsSource = gridItems;
                    break;
                case "Dictionary":
                    break;
                default:
                    return;
            }
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
