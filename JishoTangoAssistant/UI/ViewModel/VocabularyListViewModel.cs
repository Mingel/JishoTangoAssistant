using Avalonia.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using JishoTangoAssistant.Services;
using System.ComponentModel.DataAnnotations;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using JishoTangoAssistant.Helpers;

namespace JishoTangoAssistant.UI.ViewModel;

public partial class VocabularyListViewModel : JishoTangoAssistantViewModelBase
{
    public ObservableVocabularyList VocabularyList
    {
        get => CurrentSession.addedVocabularyItems;
        set => SetProperty(ref CurrentSession.addedVocabularyItems, value);
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
        if (CurrentSession.addedVocabularyItems.Count > 0)
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

        if (JishoTangoAssistantWindow.Instance == null)
            return;

        var result = await openFileDialog.ShowAsync(JishoTangoAssistantWindow.Instance);

        
        var filename = result?.FirstOrDefault();
        if (filename is null)
            return;
        
        using var reader = new StreamReader(filename);
        var fileContent = await reader.ReadToEndAsync();
        var loadedVocabularyItems = JsonConvert.DeserializeObject<VocabularyItem[]>(fileContent);

        if (loadedVocabularyItems == null)
            throw new ArgumentNullException($"{nameof(loadedVocabularyItems)} is null");

        if (performOverwriting == true)
            VocabularyList.Clear();
        VocabularyList.AddRange(loadedVocabularyItems);

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

        if (JishoTangoAssistantWindow.Instance == null)
            return;

        var result = await saveFileDialog.ShowAsync(JishoTangoAssistantWindow.Instance);

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

        var result = await exportFileDialog.ShowAsync(JishoTangoAssistantWindow.Instance!);

        if (result != null)
        {
            await using var sw = new StreamWriter(result, false, Encoding.UTF8);
            await sw.WriteAsync(VocabularyListExporter.JapaneseToEnglish(VocabularyList));
            ShowHtmlMessageBox();
        }
    }

    [RelayCommand]
    private async Task ExportCsvEnglishToJapanese()
    {
#pragma warning disable CS0618
        var exportFileDialog = new SaveFileDialog();
        exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
#pragma warning restore CS0618

        if (JishoTangoAssistantWindow.Instance == null)
            return;

        var result = await exportFileDialog.ShowAsync(JishoTangoAssistantWindow.Instance);

        if (result != null)
        {
            await using var sw = new StreamWriter(result, false, Encoding.UTF8);
            await sw.WriteAsync(VocabularyListExporter.EnglishToJapanese(VocabularyList));
            ShowHtmlMessageBox();
        }
    }

    [RelayCommand]
    private void DeleteFromList()
    {
        if (0 <= SelectedVocabItemIndex && SelectedVocabItemIndex < VocabularyList.Count)
            VocabularyList.RemoveAt(SelectedVocabItemIndex);
    }

    [RelayCommand]
    private void GoUp()
    {
        if (SelectedVocabItemIndex <= 0) return;
        var currentIndex = SelectedVocabItemIndex;
        (VocabularyList[currentIndex - 1], VocabularyList[currentIndex]) = (VocabularyList[currentIndex], VocabularyList[currentIndex - 1]);
        SelectedVocabItemIndex = currentIndex - 1;
    }

    [RelayCommand]
    private void GoDown()
    {
        if (SelectedVocabItemIndex <= -1 || SelectedVocabItemIndex >= CurrentSession.addedVocabularyItems.Count - 1) 
            return;
        var currentIndex = SelectedVocabItemIndex;
        (VocabularyList[currentIndex + 1], VocabularyList[currentIndex]) = (VocabularyList[currentIndex], VocabularyList[currentIndex + 1]);
        SelectedVocabItemIndex = currentIndex + 1;
    }

    [RelayCommand]
    private void UndoOperationOnVocabularyList() => CurrentSession.addedVocabularyItems.Undo();

    private void ShowHtmlMessageBox()
    {
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;

        if (mainWindow != null)
        {
            MessageBox.Show(mainWindow, "Information", "Make sure to ENABLE \"Allow HTML in fields\" when importing the exported file into Anki!",
                MessageBoxButtons.Ok);
        }
    }
}
