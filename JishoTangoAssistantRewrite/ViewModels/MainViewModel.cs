namespace JishoTangoAssistantRewrite.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        JishoWordSearch = new JishoWordSearchViewModel();
    }

    public JishoWordSearchViewModel JishoWordSearch { get; }
}
