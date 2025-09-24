using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.Views.JapaneseUserInputViews;

/// <summary>
/// Interaction Logic for JapaneseUserInputView.xaml
/// </summary>
public partial class JapaneseUserInputView : UserControl
{
    public JapaneseUserInputView()
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
        WeakReferenceMessenger.Default.Send(new FocusInputTextBoxMessage());
        base.OnLoaded(e);
    }
}