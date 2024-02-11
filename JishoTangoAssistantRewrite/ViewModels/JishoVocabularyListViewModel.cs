using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoVocabularyListViewModel : ViewModelBase
{
    public JishoVocabularyListViewModel()
    {
        LoadList = new RelayCommand(ExecuteLoadList);
        SaveList = new RelayCommand(ExecuteSaveList);
    }

    [ObservableProperty]
    private ObservableCollection<string> vocabularyWordList = new ObservableCollection<string>();

    [ObservableProperty]
    private int fontSize = 19;

    public ICommand LoadList { get; }

    public ICommand SaveList { get; }

    [RelayCommand]
    public void ExecuteLoadList()
    {
        // TODO
    }

    [RelayCommand]
    public void ExecuteSaveList()
    {
        // TODO
    }
}

