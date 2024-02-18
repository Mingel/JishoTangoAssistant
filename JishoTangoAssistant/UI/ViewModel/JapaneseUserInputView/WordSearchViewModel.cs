using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace JishoTangoAssistant.UI.ViewModel.JapaneseUserInputView;

public partial class WordSearchViewModel : JishoTangoAssistantViewModelBase
{
    public WordSearchViewModel(IMessenger messenger) : base()
    {
        this.messenger = messenger;
    }

    private readonly IMessenger messenger;
    

    // Input, TextInputBackground, ProcessInputCommand
    [ObservableProperty]
    private string input = string.Empty;
    
    [ObservableProperty]
    private Color textInputBackground = App.UsesDarkMode() ? Color.Parse("#66000000") : Color.Parse("#66ffffff");


    [RelayCommand]
    private void ProcessInput()
    {
        messenger.Send(new ProcessInputMessage(Input));
    }
}
