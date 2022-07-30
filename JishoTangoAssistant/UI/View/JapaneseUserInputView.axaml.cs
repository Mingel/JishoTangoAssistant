using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View
{
    /// <summary>
    /// Interaktionslogik für JapaneseUserInputView.xaml
    /// </summary>
    public partial class JapaneseUserInputView : UserControl
    {
        private JapaneseUserInputViewModel _japaneseUserInputViewModel;

        public JapaneseUserInputView()
        {
            InitializeComponent();
            _japaneseUserInputViewModel = new JapaneseUserInputViewModel();
            DataContext = _japaneseUserInputViewModel;

            _japaneseUserInputViewModel.UpdateCheckBoxesEvent += OnInputLoaded;
            _japaneseUserInputViewModel.ClearCheckBoxesEvent += OnClearEnglishDefinitions;
        }

        private void OnClearEnglishDefinitions()
        {
            this.englishDefinitionsGrid.Children.Clear();
        }

        private void OnInputLoaded(int dataLength, IList<int> englishDefinitionsLengths, IList<string> flattenedEnglishDefinitions)
        {
            var startLocationX = 12;
            var totalStepLocationX = 0; // variable
            var startLocationY = 12;
            var stepLocationY = 28;

            int flattenedIndex = 0;

            this.englishDefinitionsGrid.Children.Clear();
            _japaneseUserInputViewModel.ClearSelectedIndicesOfEnglishDefinitions();
            for (int i = 0; i < dataLength; i++)
            {
                for (int j = 0; j < englishDefinitionsLengths[i]; j++)
                {
                    EnglishDefinitionCheckBox checkBox = new EnglishDefinitionCheckBox();

                    checkBox.EnglishDefinitionsRow = i;
                    checkBox.EnglishDefinitionsColumn = j;
                    checkBox.EnglishDefinitionsFlattenedIndex = flattenedIndex;

                    
                    checkBox.Margin = new Thickness(startLocationX + totalStepLocationX, startLocationY + i * stepLocationY, 0, 0);
                    checkBox.Name = $"outputCheckBox{i}_{j}";
                    checkBox.Content = flattenedEnglishDefinitions[flattenedIndex];

                    FormattedText formattedText = new FormattedText(flattenedEnglishDefinitions[flattenedIndex], System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily), FontSize, null);

                    checkBox.MaxWidth = formattedText.Width + 50;
                    checkBox.MaxHeight = formattedText.Height;

                    totalStepLocationX += (int)Math.Ceiling(checkBox.MaxWidth);
                    checkBox.Checked += (_, _) => _japaneseUserInputViewModel.UpdateOutputText();


                    checkBox.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                    checkBox.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
                    
                    checkBox.Background = SolidColorBrush.Parse("Transparent");
                    checkBox.BorderBrush = SolidColorBrush.Parse("Transparent");
                    checkBox.CornerRadius = new CornerRadius(3, 3, 3, 3);
                    checkBox.Foreground = SolidColorBrush.Parse("Black");
                    checkBox.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                    
                    englishDefinitionsGrid.Children.Add(checkBox);

                    checkBox.Click += (_, _) => { _japaneseUserInputViewModel.ChangeSelectedIndicesOfEnglishDefinitions(checkBox.EnglishDefinitionsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

                    flattenedIndex++;
                }
                totalStepLocationX = 0;
            }
        }

        private void inputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // ?????
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            inputTextBox.Focus();
        }
    }
}
