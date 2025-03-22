using System.Collections.Specialized;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using JishoTangoAssistant.Core.Collections;

namespace JishoTangoAssistant.UI.Views.VocabularyListViews;

public partial class VocabularyListDetailsView : UserControl
{
    private bool scrollToLastItemAfterLoading;
    private object? lastItem;
    
    public VocabularyListDetailsView()
    {
        InitializeComponent();
    }
    
    
    protected override void OnInitialized()
    {
        if (VocabularyItemsDataGrid.ItemsSource is ReadOnlyObservableVocabularyList vocabularyItems)
        {
            vocabularyItems.CollectionChanged += VocabularyItemsCollectionChangedHandler;

            lastItem = vocabularyItems.GetLastAddedItem();
            if (lastItem != null)
                scrollToLastItemAfterLoading = true;
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

        Focus();
        base.OnLoaded(e);
    }
    
    private void UpdateDeleteButtonVisibility(bool isDeleteAllButtonVisible)
    {
        DeleteSelectionButton.IsVisible = !isDeleteAllButtonVisible;
        DeleteAllButton.IsVisible = isDeleteAllButtonVisible;
    }

    private void VocabularyItemsCollectionChangedHandler(object? sender, NotifyCollectionChangedEventArgs e) {
        if (e is { Action: NotifyCollectionChangedAction.Add, NewItems.Count: >= 1 })
        {
            lastItem = e.NewItems[0]!;
            scrollToLastItemAfterLoading = true;
        }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
            UpdateDeleteButtonVisibility(true);

        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
            UpdateDeleteButtonVisibility(false);

        base.OnKeyUp(e);
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