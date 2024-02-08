namespace JishoTangoAssistantRewrite.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public JishoQueryInfoViewModel JishoQueryInfoViewModel { get; } = new JishoQueryInfoViewModel();

    public JishoWordSearchViewModel JishoWordSearchViewModel { get; } = new JishoWordSearchViewModel();

    public JishoMeaningViewModel JishoMeaningViewModel { get; } = new JishoMeaningViewModel();
}
