using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.UI.ViewModel;
using System;
using System.Text.RegularExpressions;

namespace JishoTangoAssistant.UI.View
{
    public partial class VocabularyListView : UserControl
    {
        private VocabularyListViewModel _vocabularyListViewModel;

        public VocabularyListViewModel VocabularyListViewModel { get; set; }

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
                //var fontSizeText = fontSizeTextBox.Text;
                //Regex regex = new Regex(@"^[1-9][0-9]?$");
                //var newText = input.Text.Remove(input.SelectionStart, input.SelectedText.Length);//.Insert(input.SelectionStart, "5");

                fontSizeTextBox.Focus();

                if (String.IsNullOrEmpty(fontSizeTextBox.Text))
                    fontSizeTextBox.Text = CurrentSession.DefaultFontSize.ToString();



                //fontSizeTextBox.Select(fontSizeTextBox.Text.Length, 0);
            }
            else
            {
                fontSizeTextBox.Text = CurrentSession.DefaultFontSize.ToString();
            }

            /*
            DependencyProperty property = TextBox.TextProperty;

            // update font size text box
            var binding = BindingExpression(fontSizeTextBox, property);
            if (binding != null)
                binding.UpdateSource();
            */
        }
        /*
        private void fontSizeTextBox_PreviewTextInput(object sender, Event e)
        {
            // only allow two digits in font size text box
            TextBox input = (TextBox)sender;
            Regex regex = new Regex(@"^[1-9][0-9]?$");
            var newText = input.Text.Remove(input.SelectionStart, input.SelectedText.Length).Insert(input.SelectionStart, e.Text);

            e.Handled = !regex.IsMatch(newText);
        }*/
    }
}
