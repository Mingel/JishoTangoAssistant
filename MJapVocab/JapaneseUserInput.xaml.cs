﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MJapVocab
{
    /// <summary>
    /// Interaktionslogik für JapaneseUserInput.xaml
    /// </summary>
    public partial class JapaneseUserInput : UserControl
    {
        private JapaneseUserInputViewModel _japaneseUserInputviewModel;

        public JapaneseUserInputViewModel JapaneseUserInputViewModel { get; set; }

        public JapaneseUserInput()
        {
            InitializeComponent();
            _japaneseUserInputviewModel = new JapaneseUserInputViewModel();
            DataContext = _japaneseUserInputviewModel;

            _japaneseUserInputviewModel.CheckBoxEvent += OnInputLoaded;
        }

        private void OnInputLoaded(int dataLength, IList<int> englishDefinitionsLengths, IList<string> flattenedEnglishDefinitions)
        {
            var startLocationX = 12;
            var totalStepLocationX = 0; // variable
            var startLocationY = 12;
            var stepLocationY = 25;

            int flattenedIndex = 0;

            this.englishDefinitionsGrid.Children.Clear();
            _japaneseUserInputviewModel.ClearSelectedIndicesOfEnglishDefinitions();
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
                    checkBox.Checked += (_, _) => _japaneseUserInputviewModel.UpdateOutputText();
                    englishDefinitionsGrid.Children.Add(checkBox);

                    checkBox.Click += (_, _) => { _japaneseUserInputviewModel.ChangeSelectedIndicesOfEnglishDefinitions(checkBox.EnglishDefinitionsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

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
    }
}