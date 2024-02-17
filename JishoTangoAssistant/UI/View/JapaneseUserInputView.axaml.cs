using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
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
        private JapaneseUserInputViewModel japaneseUserInputViewModel;

        private const int StartLocationX = 7;
        private const int StartLocationY = 4;
        private const int StepLocationY = 30;

        public JapaneseUserInputView()
        {
            InitializeComponent();
            japaneseUserInputViewModel = new JapaneseUserInputViewModel();
            DataContext = japaneseUserInputViewModel;

            japaneseUserInputViewModel.UpdateCheckBoxesEvent += OnInputLoaded;
            japaneseUserInputViewModel.ClearCheckBoxesEvent += OnClearMeanings;
        }

        private void OnClearMeanings()
        {
            meaningGrid.Children.Clear();
        }

        private void OnInputLoaded(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings)
        {
            var totalStepLocationX = 0;

            int flattenedIndex = 0;

            meaningGrid.Children.Clear();
            japaneseUserInputViewModel.ClearSelectedIndicesOfMeanings();
            for (int i = 0; i < dataLength; i++)
            {
                for (int j = 0; j < meaningsLengths[i]; j++)
                {
                    MeaningCheckBox checkBox = new MeaningCheckBox
                    {
                        MeaningsRow = i,
                        MeaningsColumn = j,
                        MeaningsFlattenedIndex = flattenedIndex,
                        Margin = new Thickness(StartLocationX + totalStepLocationX, StartLocationY + i * StepLocationY, 0, 0),
                        Name = $"outputCheckBox{i}_{j}",
                        Content = flattenedMeanings[flattenedIndex]
                    };

                    FormattedText formattedText = new FormattedText(flattenedMeanings[flattenedIndex], CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily), FontSize, null);

                    checkBox.MaxWidth = formattedText.Width + 50;
                    checkBox.MaxHeight = formattedText.Height + 2;

                    totalStepLocationX += (int)Math.Ceiling(checkBox.MaxWidth);
                    checkBox.IsCheckedChanged += (_, _) => japaneseUserInputViewModel.UpdateOutputText();

                    checkBox.HorizontalAlignment = HorizontalAlignment.Left;
                    checkBox.VerticalAlignment = VerticalAlignment.Top;
                    
                    checkBox.Background = SolidColorBrush.Parse("Transparent");
                    checkBox.BorderBrush = SolidColorBrush.Parse("White");
                    checkBox.CornerRadius = new CornerRadius(3, 3, 3, 3);
                    checkBox.HorizontalContentAlignment = HorizontalAlignment.Left;

                    if (flattenedIndex < 10)
                        checkBox.HotKey = KeyGesture.Parse("Ctrl+D" + ((flattenedIndex + 1) % 10));

                    meaningGrid.Children.Add(checkBox);

                    checkBox.Click += (_, _) => { japaneseUserInputViewModel.ChangeSelectedIndicesOfMeanings(checkBox.MeaningsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

                    flattenedIndex++;
                }
                totalStepLocationX = 0;
            }

            if (meaningGrid.Children.Count == 1 && meaningGrid.Children[0] is MeaningCheckBox)
                ((MeaningCheckBox)meaningGrid.Children[0]).IsChecked = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            inputTextBox.Focus();
        }
    }
}
