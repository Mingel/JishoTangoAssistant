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

namespace JishoTangoAssistant.UI.ViewModel;

public partial class VocabularyListViewModel : JishoTangoAssistantViewModelBase
{
    public ObservableVocabularyList VocabularyList
    {
        get => CurrentSession.addedVocabularyItems;
        set
        {
            SetProperty(ref CurrentSession.addedVocabularyItems, value);
        }
    }

    [ObservableProperty]
    public int selectedVocabItemIndex;

    [Range(6, 96, ErrorMessage = "Value must be between 6 and 96, currently set to default value")]
    public int FontSize
    {
        get => CurrentSession.customFontSize;
        set
        {
            if (value < 6 || 96 < value)
                SetProperty(ref CurrentSession.customFontSize, CurrentSession.DefaultFontSize);
            SetProperty(ref CurrentSession.customFontSize, value);
        }
    }

    [RelayCommand]
    public async Task LoadList()
    {
        bool? performOverwriting = null;
        if (CurrentSession.addedVocabularyItems.Count > 0)
        {
            var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;
            if (mainWindow == null)
                return;

            var msgBoxResult = await MessageBox.Show(mainWindow, "Warning", "Your vocabulary list is not empty.\nDo you want to overwrite or merge into your current vocabulary list?",
                                                MessageBoxButtons.MergeOverwriteCancel);

            if (msgBoxResult.Equals(MessageBoxResult.Cancel))
                return;
            performOverwriting = msgBoxResult.Equals(MessageBoxResult.Overwrite);
        }

#pragma warning disable CS0618
        OpenFileDialog openFileDialog = new OpenFileDialog();

        if (performOverwriting == true)
            openFileDialog.Title = "Open file to load vocabulary list (Overwrite)";
        else if (performOverwriting == false)
            openFileDialog.Title = "Open file to load vocabulary list (Merge)";
        else
            openFileDialog.Title = "Open file to load vocabulary list";

        openFileDialog.Filters.Add(new FileDialogFilter() { Name = "MJV Files", Extensions = { "mjv" } });
#pragma warning restore CS0618

        if (JishoTangoAssisantWindow.Instance == null)
            return;

        var result = await openFileDialog.ShowAsync(JishoTangoAssisantWindow.Instance);

        if (result != null && result.Length > 0)
        {
            var filename = result[0];
            using (StreamReader reader = new StreamReader(filename))
            {
                var fileContent = reader.ReadToEnd();
                var loadedVocabularyItems = JsonConvert.DeserializeObject<VocabularyItem[]>(fileContent);

                if (loadedVocabularyItems == null)
                    throw new ArgumentNullException($"{nameof(loadedVocabularyItems)} is null");

                if (performOverwriting == true)
                    VocabularyList.Clear();
                VocabularyList.AddRange(loadedVocabularyItems);

                CurrentSession.userMadeChanges = false;
            }
        }
    }

    [RelayCommand]
    public async Task SaveList()
    {
#pragma warning disable CS0618
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Title = "Save vocabulary list as";
        saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "MJV Files", Extensions = { "mjv" } });
#pragma warning restore CS0618

        if (JishoTangoAssisantWindow.Instance == null)
            return;

        var result = await saveFileDialog.ShowAsync(JishoTangoAssisantWindow.Instance);

        if (result != null)
        {
            using (StreamWriter sw = new StreamWriter(result, false, Encoding.UTF8))
            {
                var json = JsonConvert.SerializeObject(VocabularyList.ToArray(), Formatting.Indented);
                sw.Write(json);
                CurrentSession.userMadeChanges = false;
            }
        }
    }

    [RelayCommand]
    private async Task ExportCsvJapeneseToEnglish()
    {
#pragma warning disable CS0618
        SaveFileDialog exportFileDialog = new SaveFileDialog();
        exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
#pragma warning restore CS0618

        var result = await exportFileDialog.ShowAsync(JishoTangoAssisantWindow.Instance!);

        if (result != null)
        {
            using (StreamWriter sw = new StreamWriter(result, false, Encoding.UTF8))
            {
                sw.Write(VocabularyListExporter.JapaneseToEnglish(VocabularyList));
                ShowHtmlMessageBox();
            }
        }
    }

    [RelayCommand]
    private async Task ExportCsvEnglishToJapanese()
    {
#pragma warning disable CS0618
        SaveFileDialog exportFileDialog = new SaveFileDialog();
        exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
#pragma warning restore CS0618

        if (JishoTangoAssisantWindow.Instance == null)
            return;

        var result = await exportFileDialog.ShowAsync(JishoTangoAssisantWindow.Instance);

        if (result != null)
        {
            using (StreamWriter sw = new StreamWriter(result, false, Encoding.UTF8))
            {
                var vocabItems = VocabularyList.ToArray();
                sw.Write(VocabularyListExporter.EnglishToJapanese(VocabularyList));
                ShowHtmlMessageBox();
            }
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
        if (SelectedVocabItemIndex > 0)
        {
            var currentIndex = SelectedVocabItemIndex;
            var tmpItem = VocabularyList[currentIndex - 1];
            VocabularyList[currentIndex - 1] = VocabularyList[currentIndex];
            VocabularyList[currentIndex] = tmpItem; // this line makes tmpIndex necessary because this line resets SelectedVocabItemIndex to -1
            SelectedVocabItemIndex = currentIndex - 1;
        }
    }

    [RelayCommand]
    private void GoDown()
    {
        if (SelectedVocabItemIndex > -1 && SelectedVocabItemIndex < CurrentSession.addedVocabularyItems.Count - 1)
        {
            var currentIndex = SelectedVocabItemIndex;
            var tmpItem = VocabularyList[currentIndex + 1];
            VocabularyList[currentIndex + 1] = VocabularyList[currentIndex];
            VocabularyList[currentIndex] = tmpItem; // this line makes tmpIndex necessary because this line resets SelectedVocabItemIndex to -1
            SelectedVocabItemIndex = currentIndex + 1;
        }
    }

    [RelayCommand]
    private void UndoOperationOnVocabularyList()
    {
        CurrentSession.addedVocabularyItems.Undo();
    }

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
