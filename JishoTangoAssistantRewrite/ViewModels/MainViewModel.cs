using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistantRewrite.Services;
using System.Windows.Input;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IWindowService windowService;

    public MainViewModel()
    {
        this.windowService = new WindowService();
        ShowCardPreview = new RelayCommand(ExecuteShowCardPreview);
        AddToVocabularyList = new RelayCommand(ExecuteAddToVocabularyList);
    }

    public MainViewModel(IWindowService windowService)
    {
        this.windowService = windowService;
        ShowCardPreview = new RelayCommand(ExecuteShowCardPreview);
        AddToVocabularyList = new RelayCommand(ExecuteAddToVocabularyList);
    }

    [ObservableProperty]
    private bool itemAdditionPossible = true;

    public JishoQueryInfoViewModel JishoQueryInfoViewModel { get; } = new JishoQueryInfoViewModel();
    public JishoWordSearchViewModel JishoWordSearchViewModel { get; } = new JishoWordSearchViewModel();
    public JishoMeaningViewModel JishoMeaningViewModel { get; } = new JishoMeaningViewModel();
    public JishoAdditionalCommentsViewModel JishoAdditionalCommentsViewModel { get; } = new JishoAdditionalCommentsViewModel();
    public JishoOptionsViewModel JishoOptionsViewModel { get; } = new JishoOptionsViewModel();
    public JishoVocabularyListViewModel JishoVocabularyListViewModel { get; } = new JishoVocabularyListViewModel();

    public ICommand ShowCardPreview { get; }
    public ICommand AddToVocabularyList { get; }

    [RelayCommand]
    public void ExecuteShowCardPreview()
    {
        var cardPreviewViewModel = new CardPreviewViewModel();
        windowService.ShowWindow(cardPreviewViewModel);
    }

    [RelayCommand]
    public void ExecuteAddToVocabularyList()
    {
        // TODO
    }
}
