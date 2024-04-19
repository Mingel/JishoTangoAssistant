using Avalonia.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Views;
using System.ComponentModel.DataAnnotations;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
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
            if (value is < 6 or > 96)
                SetProperty(ref CurrentSession.customFontSize, CurrentSession.DefaultFontSize);
            SetProperty(ref CurrentSession.customFontSize, value);
        }
    }

    [RelayCommand]
    private async Task LoadList()
    {
        bool? performOverwriting = null;
        if (vocabularyListService.Count() > 0)
        {
            var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;
            if (mainWindow == null)
                return;

            var msgBoxResult = await MessageBox.Show(mainWindow, "Warning", "Your vocabulary list is not empty." + Environment.NewLine + "Do you want to overwrite or merge into your current vocabulary list?",
                                                MessageBoxButtons.MergeOverwriteCancel);

            if (msgBoxResult.Equals(MessageBoxResult.Cancel))
                return;
            performOverwriting = msgBoxResult.Equals(MessageBoxResult.Overwrite);
        }

#pragma warning disable CS0618
        var openFileDialog = new OpenFileDialog
        {
            Title = performOverwriting switch
            {
                true => "Open file to load vocabulary list (Overwrite)",
                false => "Open file to load vocabulary list (Merge)",
                _ => "Open file to load vocabulary list"
            }
        };

        openFileDialog.Filters.Add(new FileDialogFilter() { Name = "MJV Files", Extensions = { "mjv" } });
#pragma warning restore CS0618

        if (JishoTangoAssistantWindowView.Instance == null)
            return;

        var result = await openFileDialog.ShowAsync(JishoTangoAssistantWindowView.Instance);

        
        var filename = result?.FirstOrDefault();
        if (filename is null)
            return;
        
        using var reader = new StreamReader(filename);
        var fileContent = await reader.ReadToEndAsync();
        var loadedVocabularyItems = JsonConvert.DeserializeObject<VocabularyItem[]>(fileContent);

        if (loadedVocabularyItems == null)
            throw new ArgumentNullException($"{nameof(loadedVocabularyItems)} is null");

        if (performOverwriting == true)
            await vocabularyListService.ClearAsync();
        await vocabularyListService.AddRangeAsync(loadedVocabularyItems);

        CurrentSession.userMadeChanges = false;
    }

    [RelayCommand]
    private async Task SaveList()
    {
#pragma warning disable CS0618
        var saveFileDialog = new SaveFileDialog
        {
            Title = "Save vocabulary list as"
        };
        saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "MJV Files", Extensions = { "mjv" } });
#pragma warning restore CS0618

        if (JishoTangoAssistantWindowView.Instance == null)
            return;

        var result = await saveFileDialog.ShowAsync(JishoTangoAssistantWindowView.Instance);

        if (result != null)
        {
            await using var sw = new StreamWriter(result, false, Encoding.UTF8);
            var json = JsonConvert.SerializeObject(VocabularyList.ToArray(), Formatting.Indented);
            await sw.WriteAsync(json);
            CurrentSession.userMadeChanges = false;
        }
    }

    [RelayCommand]
    private async Task ExportCsvJapaneseToEnglish()
    {
#pragma warning disable CS0618
        var exportFileDialog = new SaveFileDialog();
        exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
#pragma warning restore CS0618

        var result = await exportFileDialog.ShowAsync(JishoTangoAssistantWindowView.Instance!);

        if (result != null)
        {
            await using var sw = new StreamWriter(result, false, Encoding.UTF8);
            await sw.WriteAsync(VocabularyListExporter.JapaneseToEnglish(VocabularyList));
            await ShowNotetypeMessageBox();
        }
    }

    [RelayCommand]
    private async Task ExportCsvEnglishToJapanese()
    {
#pragma warning disable CS0618
        var exportFileDialog = new SaveFileDialog();
        exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
#pragma warning restore CS0618

        if (JishoTangoAssistantWindowView.Instance == null)
            return;

        var result = await exportFileDialog.ShowAsync(JishoTangoAssistantWindowView.Instance);

        if (result != null)
        {
            await using var sw = new StreamWriter(result, false, Encoding.UTF8);
            await sw.WriteAsync(VocabularyListExporter.EnglishToJapanese(VocabularyList));
            await ShowNotetypeMessageBox();
        }
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

    private async Task ShowNotetypeMessageBox()
    {
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

        if (mainWindow != null)
        {
            await MessageBox.Show(mainWindow, "Information", "Make sure to select the Notetype \"Basic\" when importing the exported file into Anki!",
                MessageBoxButtons.Ok);
        }
    }
}
