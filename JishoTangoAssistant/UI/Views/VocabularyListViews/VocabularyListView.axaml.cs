using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace JishoTangoAssistant.UI.Views.VocabularyListViews;

public partial class VocabularyListView : UserControl
{
    public VocabularyListView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if (this.GetVisualRoot() is JishoTangoAssistantWindowView root)
            root.CurrentlyLoadedControlContent = this;
        base.OnAttachedToVisualTree(e);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        Focus();
        base.OnLoaded(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
        {
            var vocabularyListDetailsView = this.FindControl<VocabularyListDetailsView>("VocabularyListDetailsView");
            vocabularyListDetailsView?.UpdateDeleteButtonVisibility(true);
        }
        base.OnKeyDown(e);
    }
    
    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.Key is Key.LeftShift or Key.RightShift)
        {
            var vocabularyListDetailsView = this.FindControl<VocabularyListDetailsView>("VocabularyListDetailsView");
            vocabularyListDetailsView?.UpdateDeleteButtonVisibility(false);
        }
        base.OnKeyUp(e);
    }
}