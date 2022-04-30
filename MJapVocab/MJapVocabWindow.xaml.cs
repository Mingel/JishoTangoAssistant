
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MJapVocab
{
    /// <summary>
    /// Interaction logic for MJapVocabWindow.xaml
    /// </summary>
    public partial class MJapVocabWindow : Window
    {
        private MJapVocabViewModel viewModel;

        public MJapVocabWindow()
        {
            viewModel = new MJapVocabViewModel();
            DataContext = viewModel;

            viewModel.CheckBoxEvent += OnInputLoaded;

            InitializeComponent();
        }

        private void OnInputLoaded(int dataLength, IList<int> englishDefinitionsLengths, IList<string> flattenedEnglishDefinitions)
        {
            var startLocationX = 12;
            var totalStepLocationX = 0; // variable
            var startLocationY = 12;
            var stepLocationY = 25;

            int flattenedIndex = 0;

            this.englishDefinitionsGrid.Children.Clear();
            viewModel.ClearSelectedIndicesOfEnglishDefinitions();
            for (int i = 0; i < dataLength; i++)
            {
                for (int j = 0; j < englishDefinitionsLengths[i]; j++)
                {
                    EnglishDefinitionCheckBox checkBox = new EnglishDefinitionCheckBox();

                    checkBox.EnglishDefinitionsRow = i;
                    checkBox.EnglishDefinitionsColumn = j;
                    checkBox.EnglishDefinitionsFlattenedIndex = flattenedIndex;

                    checkBox.Margin = new Thickness(startLocationX + totalStepLocationX, startLocationY + i * stepLocationY, 0, 0);
                    checkBox.Name = String.Format("outputCheckBox{0}_{1}", i, j);
                    checkBox.Content = flattenedEnglishDefinitions[flattenedIndex];
                    checkBox.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    totalStepLocationX = (int)Math.Ceiling(checkBox.DesiredSize.Width);
                    checkBox.Checked += (_, _) => viewModel.UpdateOutputText();
                    englishDefinitionsGrid.Children.Add(checkBox);

                    checkBox.Click += (_, _) => { viewModel.ChangeSelectedIndicesOfEnglishDefinitions(checkBox.EnglishDefinitionsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

                    flattenedIndex++;
                }
                totalStepLocationX = 0;
            }
            
        }

        private void inputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox input = (TextBox)sender;
                DependencyProperty property = TextBox.TextProperty;

                var binding = BindingOperations.GetBindingExpression(input, property);
                if (binding != null) 
                    binding.UpdateSource();
            }
        }

        private void vocabularyItemsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (1 < vocabularyItemsDataGrid.Columns.Count)
                vocabularyItemsDataGrid.Columns[1].Visibility = Visibility.Hidden;

            vocabularyItemsDataGrid.Columns[0].MinWidth = 70;
            vocabularyItemsDataGrid.Columns[2].MinWidth = 70;
            vocabularyItemsDataGrid.Columns[0].Width = 70;
            vocabularyItemsDataGrid.Columns[2].Width = 70;
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            vocabularyItemsDataGrid.Focus();
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            vocabularyItemsDataGrid.Focus();
        }
    }
}
