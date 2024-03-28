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
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Models.Messages;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.ViewModel;

namespace JishoTangoAssistant.UI.View;

public partial class MeaningsView : UserControl, 
    IRecipient<UpdateMeaningViewModelMessage>
{
    private const int StartLocationX = 7;
    private const int StartLocationY = 4;
    private const int StepLocationY = 30;
    
    public MeaningsView()
    {
        InitializeComponent();
    }
    
    protected override void OnLoaded(RoutedEventArgs e)
    {
        if (DataContext is not MeaningsViewModel viewModel) 
            return;
        
        viewModel.UpdateCheckBoxesEvent += OnInputLoaded;
        viewModel.ClearCheckBoxesEvent += OnClearMeanings;
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        if (DataContext is not MeaningsViewModel viewModel) 
            return;
        
        viewModel.UpdateCheckBoxesEvent -= OnInputLoaded;
        viewModel.ClearCheckBoxesEvent -= OnClearMeanings;
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

        var viewModel = DataContext as MeaningsViewModel;
        viewModel?.ClearSelectedIndicesOfMeanings();
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
                    Name = $"OutputCheckBox{i}_{j}",
                    Content = flattenedMeanings[flattenedIndex],
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Background = SolidColorBrush.Parse("Transparent"),
                    BorderBrush = SolidColorBrush.Parse("White"),
                    CornerRadius = new CornerRadius(3, 3, 3, 3),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    HotKey = flattenedIndex < 9 ? KeyGesture.Parse("Ctrl+D" + (flattenedIndex + 1)) : null
                };

                var formattedText = new FormattedText(flattenedMeanings[flattenedIndex], CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily), FontSize, null);

                checkBox.MaxWidth = formattedText.Width + 50;
                checkBox.MaxHeight = formattedText.Height + 6;

                totalStepLocationX += (int)Math.Ceiling(checkBox.MaxWidth);
                checkBox.IsCheckedChanged += (_, _) => viewModel?.UpdateOutputText();
                checkBox.Click += (_, _) => { viewModel?.ChangeSelectedIndicesOfMeanings(checkBox.MeaningsFlattenedIndex, isSelected: checkBox.IsChecked == true); };

                MeaningGrid.Children.Add(checkBox);
                flattenedIndex++;
            }
            totalStepLocationX = 0;
        }

        if (MeaningGrid.Children is [MeaningCheckBox])
            ((MeaningCheckBox)MeaningGrid.Children.First()).IsChecked = true;
    }

    public void Receive(UpdateMeaningViewModelMessage message)
    {
        var meanings = message.Value.ToList();
        OnInputLoaded(meanings.Count, meanings.Select(x => x.Count()).ToList(), meanings.SelectMany(x => x).ToList());
    }
}