using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View;

/// <summary>
/// Interaction Logic for JapaneseUserInputView.xaml
/// </summary>
public partial class JapaneseUserInputView : UserControl
{
    private readonly JapaneseUserInputViewModel japaneseUserInputViewModel;

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
        MeaningGrid.Children.Clear();
    }

    private void OnInputLoaded(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings)
    {
        var totalStepLocationX = 0;

        var flattenedIndex = 0;

        MeaningGrid.Children.Clear();
        japaneseUserInputViewModel.ClearSelectedIndicesOfMeanings();
        for (var i = 0; i < dataLength; i++)
        {
            for (var j = 0; j < meaningsLengths[i]; j++)
            {
                var checkBox = new MeaningCheckBox
                {
                    MeaningsRow = i,
                    MeaningsColumn = j,
                    MeaningsFlattenedIndex = flattenedIndex,
                    Margin = new Thickness(StartLocationX + totalStepLocationX, StartLocationY + i * StepLocationY, 0, 0),
                    Name = $"outputCheckBox{i}_{j}",
                    Content = flattenedMeanings[flattenedIndex]
                };

                var formattedText = new FormattedText(flattenedMeanings[flattenedIndex], CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily), FontSize, null);

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
                    checkBox.HotKey = KeyGesture.Parse("Ctrl+D" + (flattenedIndex + 1) % 10);

                MeaningGrid.Children.Add(checkBox);

                checkBox.Click += (_, _) => { japaneseUserInputViewModel.ChangeSelectedIndicesOfMeanings(checkBox.MeaningsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

                flattenedIndex++;
            }
            totalStepLocationX = 0;
        }

        if (MeaningGrid.Children is [MeaningCheckBox])
            ((MeaningCheckBox)MeaningGrid.Children.First()).IsChecked = true;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        InputTextBox.Focus();
    }
}