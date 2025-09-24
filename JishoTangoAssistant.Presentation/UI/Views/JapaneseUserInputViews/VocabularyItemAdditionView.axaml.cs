using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.Views.JapaneseUserInputViews;

public partial class VocabularyItemAdditionView : UserControl
{
    public VocabularyItemAdditionView()
    {
        InitializeComponent();
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (AddButton.IsEnabled &&
            AddButton.HotKey?.KeyModifiers == e.KeyModifiers &&
            AddButton.HotKey?.Key == e.Key)
        {
            WeakReferenceMessenger.Default.Send(new FocusInputTextBoxMessage());
        }
        base.OnKeyUp(e);
    }

    private void AddToVocabularyListClickHandler(object? sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new FocusInputTextBoxMessage());
    }
}