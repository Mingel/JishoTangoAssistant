using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistantRewrite.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JishoTangoAssistantRewrite.ViewModels;

public partial class JishoVocabularyListViewModel : ViewModelBase
{
    public JishoVocabularyListViewModel()
    {
        DeleteFromList = new RelayCommand(ExecuteDeleteFromList);
        UndoOperationOnVocabularyList = new RelayCommand(ExecuteUndoOperationOnVocabularyList);
        GoUp = new RelayCommand(ExecuteGoUp);
        GoDown = new RelayCommand(ExecuteGoDown);
        LoadList = new RelayCommand(ExecuteLoadList);
        SaveList = new RelayCommand(ExecuteSaveList);
    }

    [ObservableProperty]
    private ObservableCollection<VocabularyItem> vocabularyWordList = new ObservableCollection<VocabularyItem>()
    {
        new VocabularyItem("word", true, "reading", "output"),
        new VocabularyItem("word2", true, "reading2", "output2")
    };

    [ObservableProperty]
    private int selectedVocabItemIndex = 0;

    [ObservableProperty]
    private int fontSize = 19;
    public ICommand DeleteFromList { get; }

    public ICommand UndoOperationOnVocabularyList { get; }

    public ICommand GoUp { get; }

    public ICommand GoDown { get; }

    public ICommand SaveList { get; }

    public ICommand LoadList { get; }

    [RelayCommand]
    public void ExecuteDeleteFromList()
    {
        // TODO
    }

    [RelayCommand]
    public void ExecuteUndoOperationOnVocabularyList()
    {
        // TODO
    }

    [RelayCommand]
    public void ExecuteGoUp()
    {
        // TODO
    }

    [RelayCommand]
    public void ExecuteGoDown()
    {
        // TODO
    }


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

