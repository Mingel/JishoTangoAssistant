using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistantRewrite.Services;
using System;
using System.Windows.Input;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IWindowService windowService;

    public MainViewModel()
    {
        this.windowService = new WindowService();
        ShowCardPreview = new RelayCommand(ExecuteShowCardPreview);
    }

    public MainViewModel(IWindowService windowService)
    {
        this.windowService = windowService;
        ShowCardPreview = new RelayCommand(ExecuteShowCardPreview);
    }

    public JishoQueryInfoViewModel JishoQueryInfoViewModel { get; } = new JishoQueryInfoViewModel();
    public JishoWordSearchViewModel JishoWordSearchViewModel { get; } = new JishoWordSearchViewModel();
    public JishoMeaningViewModel JishoMeaningViewModel { get; } = new JishoMeaningViewModel();
    public JishoAdditionalCommentsViewModel JishoAdditionalCommentsViewModel { get; } = new JishoAdditionalCommentsViewModel();
    public JishoOptionsViewModel JishoOptionsViewModel { get; } = new JishoOptionsViewModel();

    public ICommand ShowCardPreview { get; }

    [RelayCommand]
    public void ExecuteShowCardPreview()
    {
        var cardPreviewViewModel = new CardPreviewViewModel();
        windowService.ShowWindow(cardPreviewViewModel);
    }
}
