using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoQueryInfoViewModel : ViewModelBase
{
    [ObservableProperty]
    private int selectedIndexOfWords = 0;
    [ObservableProperty]
    private ObservableCollection<string> words = new ObservableCollection<string>() { "Test", "Test2", "Test3" };

    [ObservableProperty]
    private int selectedIndexOfOtherForms = 1;
    [ObservableProperty]
    private ObservableCollection<string> otherForms = ["Test", "Test2", "Test3"];
    [ObservableProperty]
    private string readingOutput = "Test";
}
