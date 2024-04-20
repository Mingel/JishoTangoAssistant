using System.Collections.Specialized;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.UI.Views;

public partial class VocabularyListView : UserControl
{
    private bool scrollToLastItemAfterLoading;
    private object? lastItem;

    public VocabularyListView()
    {
        InitializeComponent();

        KeyDown += KeyDownHandler;
        KeyUp += KeyUpHandler;
    }

    protected override void OnInitialized()
    {
        if (VocabularyItemsDataGrid.ItemsSource is ReadOnlyObservableVocabularyList vocabularyItems)
        {
            vocabularyItems.CollectionChanged += (_, e) => {
                if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems?.Count >= 1)
                {
                    lastItem = e.NewItems[0]!;
                    scrollToLastItemAfterLoading = true;
                }
            };

            lastItem = vocabularyItems.GetLastAddedItem();
            if (lastItem != null)
            {
                scrollToLastItemAfterLoading = true;
            }
                
        }

        base.OnInitialized();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        if (scrollToLastItemAfterLoading)
        {
            VocabularyItemsDataGrid.ScrollIntoView(lastItem, null);
            VocabularyItemsDataGrid.SelectedItem = lastItem;
            scrollToLastItemAfterLoading = false;
        }

        base.OnLoaded(e);
    }

    private void KeyDownHandler(object? _, KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
        {
            DeleteSelectionButton.IsVisible = false;
            DeleteAllButton.IsVisible = true;
        }
    }

    private void KeyUpHandler(object? _, KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
        {
            DeleteSelectionButton.IsVisible = true;
            DeleteAllButton.IsVisible = false;
        }
    }

    private void UpButtonClickHandler(object sender, RoutedEventArgs e)
    {
        VocabularyItemsDataGrid.Focus();
    }

    private void DownButtonClickHandler(object sender, RoutedEventArgs e)
    {
        VocabularyItemsDataGrid.Focus();
    }
}