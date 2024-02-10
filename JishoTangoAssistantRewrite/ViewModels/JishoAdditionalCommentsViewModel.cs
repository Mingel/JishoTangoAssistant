using CommunityToolkit.Mvvm.ComponentModel;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoAdditionalCommentsViewModel : ViewModelBase
{
    [ObservableProperty]
    private string additionalComments = string.Empty;
}
