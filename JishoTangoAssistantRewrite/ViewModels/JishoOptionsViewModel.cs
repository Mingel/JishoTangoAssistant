using CommunityToolkit.Mvvm.ComponentModel;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoOptionsViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool writeInKana = false;
}
