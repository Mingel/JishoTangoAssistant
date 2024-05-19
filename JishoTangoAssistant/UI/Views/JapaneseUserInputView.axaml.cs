using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace JishoTangoAssistant.UI.Views;

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
        InputTextBox.Focus();
        base.OnLoaded(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (AddButton.IsEnabled &&
            AddButton.HotKey?.KeyModifiers == e.KeyModifiers &&
            AddButton.HotKey?.Key == e.Key)
        {
            InputTextBox.Focus();
        }
        base.OnKeyUp(e);
    }

    private void AddToVocabularyListClickHandler(object? sender, RoutedEventArgs e)
    {
        InputTextBox.Focus();
    }

    private void RemovePreEnteredInputsButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        InputTextBox.Focus();
    }

    private void BackToUserInputButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        PreEnteredInputListTextBox.Focus();
    }
}