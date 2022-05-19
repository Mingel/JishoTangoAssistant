using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace JishoTangoAssistant
{
    /// <summary>
    /// Interaktionslogik für VocabularyListView.xaml
    /// </summary>
    public partial class VocabularyListView : UserControl
    {
        private VocabularyListViewModel _vocabularyListViewModel;
        public VocabularyListViewModel VocabularyListViewModel { get; set; }

        private const int defaultFontSize = 28;
        public VocabularyListView()
        {
            InitializeComponent();
            _vocabularyListViewModel = new VocabularyListViewModel();
            DataContext = _vocabularyListViewModel;
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            vocabularyItemsDataGrid.Focus();
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            vocabularyItemsDataGrid.Focus();
        }

        private void fontSizeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            fontSizeTextBox.IsEnabled = fontSizeCheckBox.IsChecked ?? false;
            if (fontSizeTextBox.IsEnabled)
            {
                fontSizeTextBox.Focus();

                if (fontSizeTextBox.Text.Equals(String.Empty))
                    fontSizeTextBox.Text = defaultFontSize.ToString();

                fontSizeTextBox.Select(fontSizeTextBox.Text.Length, 0);
            }
            else
            {
                fontSizeTextBox.Text = String.Empty;
            }

            DependencyProperty property = TextBox.TextProperty;

            // update font size text box
            var binding = BindingOperations.GetBindingExpression(fontSizeTextBox, property);
            if (binding != null)
                binding.UpdateSource();
        }

        private void fontSizeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // only allow two digits in font size text box
            TextBox input = (TextBox)sender;
            Regex regex = new Regex(@"^[1-9][0-9]?$");
            var newText = input.Text.Remove(input.SelectionStart, input.SelectionLength).Insert(input.SelectionStart, e.Text);

            e.Handled = !regex.IsMatch(newText);
        }
    }
}
