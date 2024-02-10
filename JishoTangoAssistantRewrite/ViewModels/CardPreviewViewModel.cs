using CommunityToolkit.Mvvm.ComponentModel;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class CardPreviewViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool japaneseToEnglishDirection = false;

    [ObservableProperty]
    private bool englishToJapaneseDirection = false;

    [ObservableProperty]
    private bool showFrontSide = false;

    [ObservableProperty]
    private bool showBackSide = false;

    [ObservableProperty]
    private string outputText = string.Empty;
}
