using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.Models.Messages;
using JishoTangoAssistant.Utils;

namespace JishoTangoAssistant.UI.ViewModel;

public partial class WordSearchViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateTextInputBackgroundMessage>
{
    public WordSearchViewModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
    }
    
    // Input, TextInputBackground, ProcessInputCommand
    [ObservableProperty]
    private string input = string.Empty;
    
    [ObservableProperty]
    private Color textInputBackground = App.UsesDarkMode() ? Color.Parse("#66000000") : Color.Parse("#66ffffff");
    
    [RelayCommand]
    private void ProcessInput()
    {
        Input = RomajiKanaConverter.Convert(Input.Trim());
        WeakReferenceMessenger.Default.Send(new ProcessInputMessage(Input));
    }

    public void Receive(UpdateTextInputBackgroundMessage message)
    {
        UpdateTextInputBackground(message.Value);
    }
    
    private void UpdateTextInputBackground(VocabularyItem? itemFromCurrentUserInput)
    {
        var color = InputTextColorNoDuplicate();
        
        if (itemFromCurrentUserInput != null && CurrentSession.VocabularyListService.ContainsWord(itemFromCurrentUserInput.Word))
        {
            color = CurrentSession.VocabularyListService.Contains(itemFromCurrentUserInput) ? InputTextColorSameMeaning() : InputTextColorDifferentMeaning();
        }
        TextInputBackground = Color.Parse(color);
    }
    
    private string InputTextColorNoDuplicate()
    {
        return App.UsesDarkMode() ? "#66000000" : "#66FFFFFF";
    }

    private string InputTextColorDifferentMeaning()
    {
        return App.UsesDarkMode() ? "#667D7D69" : "#66FAFAD2";
    }
    private string InputTextColorSameMeaning()
    {
        return App.UsesDarkMode() ? "#66744B3D" : "#66E9967A";
    }
}
