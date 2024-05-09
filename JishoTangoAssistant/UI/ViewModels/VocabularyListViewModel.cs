using System;
using JishoTangoAssistant.UI.Elements;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Utils;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.UI.ViewModels;

public partial class VocabularyListViewModel(IVocabularyListService vocabularyListService) : JishoTangoAssistantViewModelBase
{
    public ReadOnlyObservableVocabularyList VocabularyList
    {
        get => vocabularyListService.GetList();
        set
        {
            var vocabularyList = vocabularyListService.GetList();
            SetProperty(ref vocabularyList, value);
        }
    }

    [ObservableProperty] 
    private int selectedVocabItemIndex;

    [Range(6, 96, ErrorMessage = "Value must be between 6 and 96, currently set to default value")]
    public int FontSize
    {
        get => CurrentSession.customFontSize;
        set
        {
            var fontSize = value is < 6 or > 96 ? CurrentSession.DefaultFontSize : value;
            SetProperty(ref CurrentSession.customFontSize, fontSize);
        }
    }

    [RelayCommand]
    private async Task LoadList()
    {
        bool? performOverwriting = null;
        if (vocabularyListService.Count() > 0)
        {
            var msgBoxResult = await MessageBoxUtil.CreateAndShowAsync("Warning", "Your vocabulary list is not empty." + Environment.NewLine + "Do you want to overwrite or merge into your current vocabulary list?",
                                                MessageBoxButtons.MergeOverwriteCancel);

            if (msgBoxResult == MessageBoxResult.Cancel)
                return;
            performOverwriting = msgBoxResult == MessageBoxResult.Overwrite;
        }

        var filePickerTitle = performOverwriting switch
        {
            true => "Open file to load vocabulary list (Overwrite)",
            false => "Open file to load vocabulary list (Merge)",
            _ => "Open file to load vocabulary list"
        };

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };

        var loadedVocabularyItems = await VocabularyListFilePicker.LoadAsync(filePickerTitle, filePickerFilter);

        // this case can occur if user cancels file dialog
        if (loadedVocabularyItems == null)
            return;

        if (performOverwriting == true)
            await vocabularyListService.ClearAsync();
        await vocabularyListService.AddRangeAsync(loadedVocabularyItems, true);

        CurrentSession.userMadeChanges = false;
    }

    [RelayCommand]
    private async Task SaveList()
    {
        var list = vocabularyListService.GetList();

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };

        var success = await VocabularyListFilePicker.SaveAsync(list, "Save vocabulary list as", filePickerFilter);

        if (success)
            CurrentSession.userMadeChanges = false;
    }

    [RelayCommand]
    private async Task ExportCsvJapaneseToEnglish()
    {
        await ExportCsv(false);
    }

    [RelayCommand]
    private async Task ExportCsvEnglishToJapanese()
    {
        await ExportCsv(true);
    }

    private async Task ExportCsv(bool toJapanese)
    {
        var filePickerFilter = new[] {
            new FilePickerFileType("CSV Files") { Patterns = ["*.csv"] }
        };

        var list = vocabularyListService.GetList();

        var contentToExport = toJapanese ? VocabularyListExporter.EnglishToJapanese(list) : VocabularyListExporter.JapaneseToEnglish(list);

        var success = await VocabularyListFilePicker.SaveAsync(contentToExport, "Export vocabulary list as", filePickerFilter);

        if (success)
            await ShowNotetypeMessageBox();
    }

    [RelayCommand]
    private async Task DeleteFromList()
    {
        if (0 <= SelectedVocabItemIndex && SelectedVocabItemIndex < VocabularyList.Count)
            await vocabularyListService.RemoveAtAsync(SelectedVocabItemIndex);
    }
    
    [RelayCommand]
    private async Task DeleteAllFromList()
    {
        await vocabularyListService.ClearAsync();
    }

    [RelayCommand]
    private async Task GoUp()
    {
        if (SelectedVocabItemIndex <= 0)
            return;
        await vocabularyListService.SwapAsync(SelectedVocabItemIndex - 1, SelectedVocabItemIndex);
    }

    [RelayCommand]
    private async Task GoDown()
    {
        if (SelectedVocabItemIndex <= -1 || SelectedVocabItemIndex >= vocabularyListService.Count() - 1) 
            return;
        await vocabularyListService.SwapAsync(SelectedVocabItemIndex, SelectedVocabItemIndex + 1);
        SelectedVocabItemIndex++;
    }

    [RelayCommand]
    private async Task UndoOperationOnVocabularyList()
    {
        await vocabularyListService.UndoAsync();
        CurrentSession.userMadeChanges = true;
    }

    private static async Task ShowNotetypeMessageBox()
    {
        await MessageBoxUtil.CreateAndShowAsync("Information", "Make sure to select the Notetype \"Basic\" when importing the exported file into Anki!",
            MessageBoxButtons.Ok);
    }
}
