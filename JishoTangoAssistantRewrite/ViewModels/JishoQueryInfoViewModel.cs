using CommunityToolkit.Mvvm.ComponentModel;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoQueryInfoViewModel : ViewModelBase
{
    [ObservableProperty]
    private string selectedIndexOfWords = "Test";
    [ObservableProperty]
    private string[] words = ["Test"];
    [ObservableProperty]
    private string selectedIndexOfOtherForms = "Test";
    [ObservableProperty]
    private string[] otherForms = ["Test"];
    [ObservableProperty]
    private string readingOutput = "Test";
}
