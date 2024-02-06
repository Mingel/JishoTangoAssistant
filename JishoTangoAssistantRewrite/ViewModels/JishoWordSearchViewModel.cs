using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoWordSearchViewModel : ViewModelBase
{
    public JishoWordSearchViewModel() : base() {
        ProcessInputCommand = new RelayCommand(IncrementCounter);
    }

    [ObservableProperty]
    private string input = string.Empty;

    private int counter;

    public int Counter
    {
        get => counter;
        private set => SetProperty(ref counter, value);
    }

    public ICommand ProcessInputCommand { get; }

    private void IncrementCounter() => Counter++;

    [RelayCommand]
    public void TestMethod()
    {
        IncrementCounter();
    }
}
