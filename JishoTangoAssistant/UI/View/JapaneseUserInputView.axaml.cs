using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
            _japaneseUserInputViewModel.ClearCheckBoxesEvent += OnClearMeanings;
        }

        private void OnClearMeanings()
        {
            this.meaningGrid.Children.Clear();
        }

        private void OnInputLoaded(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings)
        {
            var startLocationX = 7;
            var totalStepLocationX = 0; // variable
            var startLocationY = 4;
            var stepLocationY = 30;

            int flattenedIndex = 0;

            this.meaningGrid.Children.Clear();
            _japaneseUserInputViewModel.ClearSelectedIndicesOfMeanings();
            for (int i = 0; i < dataLength; i++)
            {
                for (int j = 0; j < meaningsLengths[i]; j++)
                {
                    MeaningCheckBox checkBox = new MeaningCheckBox();

                    checkBox.MeaningsRow = i;
                    checkBox.MeaningsColumn = j;
                    checkBox.MeaningsFlattenedIndex = flattenedIndex;

                    
                    checkBox.Margin = new Thickness(startLocationX + totalStepLocationX, startLocationY + i * stepLocationY, 0, 0);
                    checkBox.Name = $"outputCheckBox{i}_{j}";
                    checkBox.Content = flattenedMeanings[flattenedIndex];

                    FormattedText formattedText = new FormattedText(flattenedMeanings[flattenedIndex], System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily), FontSize, null);

                    checkBox.MaxWidth = formattedText.Width + 50;
                    checkBox.MaxHeight = formattedText.Height + 1;

                    totalStepLocationX += (int)Math.Ceiling(checkBox.MaxWidth);
                    checkBox.Checked += (_, _) => _japaneseUserInputViewModel.UpdateOutputText();


                    checkBox.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                    checkBox.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
                    
                    checkBox.Background = SolidColorBrush.Parse("Transparent");
                    checkBox.BorderBrush = SolidColorBrush.Parse("Transparent");
                    checkBox.CornerRadius = new CornerRadius(3, 3, 3, 3);
                    checkBox.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                    
                    meaningGrid.Children.Add(checkBox);

                    checkBox.Click += (_, _) => { _japaneseUserInputViewModel.ChangeSelectedIndicesOfMeanings(checkBox.MeaningsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

                    flattenedIndex++;
                }
                totalStepLocationX = 0;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            inputTextBox.Focus();
        }
    }
}
