using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.Views.JapaneseUserInputViews;

/// <summary>
/// Interaction Logic for JapaneseUserInputView.xaml
/// </summary>
public partial class JapaneseUserInputView : UserControl
{
    public JapaneseUserInputView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new FocusInputTextBoxMessage());
        base.OnLoaded(e);
    }
}