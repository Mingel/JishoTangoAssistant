using Avalonia.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using JishoTangoAssistant.Services.Commands;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.View;
using JishoTangoAssistant.Services;
using System.ComponentModel.DataAnnotations;
using Avalonia.Controls.ApplicationLifetimes;

namespace JishoTangoAssistant.UI.ViewModel
{
    public class VocabularyListViewModel : JishoTangoAssistantViewModelBase
    {
        private int _selectedVocabItemIndex = -1;

        private readonly DelegateCommand _loadListCommand;
        private readonly DelegateCommand _saveListCommand;
        private readonly DelegateCommand _exportCsvJapToEngCommand;
        private readonly DelegateCommand _exportCsvEngToJapCommand;
        private readonly DelegateCommand _deleteFromListCommand;
        private readonly DelegateCommand _goUpCommand;
        private readonly DelegateCommand _goDownCommand;

        public ICommand SaveListCommand => _saveListCommand;
        public ICommand LoadListCommand => _loadListCommand;
        public ICommand ExportCsvJapToEngCommand => _exportCsvJapToEngCommand;
        public ICommand ExportCsvEngToJapCommand => _exportCsvEngToJapCommand;
        public ICommand DeleteFromListCommand => _deleteFromListCommand;
        public ICommand GoUpCommand => _goUpCommand;
        public ICommand GoDownCommand => _goDownCommand;

        public ObservableVocabularyList VocabularyList
        {
            get => CurrentSession.addedVocabularyItems;
            set
            {
                SetProperty(ref CurrentSession.addedVocabularyItems, value);
            }
        }

        public int SelectedVocabItemIndex
        {
            get => _selectedVocabItemIndex;
            set
            {
                SetProperty(ref _selectedVocabItemIndex, value);
            }
        }

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

        public VocabularyListViewModel()
        {
            _loadListCommand = new DelegateCommand(OnLoadList, _ => true);
            _saveListCommand = new DelegateCommand(OnSaveList, _ => true);
            _exportCsvJapToEngCommand = new DelegateCommand(OnExportCsvJapeneseToEnglish, _ => true);
            _exportCsvEngToJapCommand = new DelegateCommand(OnExportCsvEnglishToJapanese, _ => true);
            _deleteFromListCommand = new DelegateCommand(OnDeleteFromList, _ => true);
            _goUpCommand = new DelegateCommand(OnGoUp, _ => true);
            _goDownCommand = new DelegateCommand(OnGoDown, _ => true);
        }

        private async void OnLoadList(Object commandParameter)
        {
            bool? performOverwriting = null;
            if (CurrentSession.addedVocabularyItems.Count > 0)
            {
                var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime).MainWindow;
                var msgBoxResult = await MessageBox.Show(mainWindow, "Warning", "Your vocabulary list is not empty. Do you want to overwrite your current vocabulary list?\n\n" +
                                                    "Press Yes, if you want to overwrite your list\n" +
                                                    "Press No, if you want to merge into your current list\n",
                                                    MessageBoxButtons.YesNoCancel);

                if (msgBoxResult.Equals(MessageBoxResult.Cancel))
                    return;
                performOverwriting = msgBoxResult.Equals(MessageBoxResult.Yes);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (performOverwriting == true)
                openFileDialog.Title = "Open file to load vocabulary list (Overwriting)";
            else if (performOverwriting == false)
                openFileDialog.Title = "Open file to load vocabulary list (Merging)";
            else
                openFileDialog.Title = "Open file to load vocabulary list";

            openFileDialog.Filters.Add(new FileDialogFilter() { Name = "MJV Files", Extensions = { "mjv" } });
            //openFileDialog.RestoreDirectory = true;

            var result = await openFileDialog.ShowAsync(JishoTangoAssisantWindow.Instance);

            if (result != null)
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

        private async void OnSaveList(Object commandParameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Save vocabulary list as";

            saveFileDialog.Filters.Add(new FileDialogFilter() { Name = "MJV Files", Extensions = { "mjv" } });
            //saveFileDialog.RestoreDirectory = true;

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

        private async void OnExportCsvJapeneseToEnglish(Object commandParameter)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
            //exportFileDialog.RestoreDirectory = true;

            var result = await exportFileDialog.ShowAsync(JishoTangoAssisantWindow.Instance);

            if (result != null)
            {
                using (StreamWriter sw = new StreamWriter(result, false, Encoding.UTF8))
                {
                    sw.Write(VocabularyListExporter.JapaneseToEnglish(VocabularyList));
                    ShowHtmlMessageBox();
                }
            }
        }

        private async void OnExportCsvEnglishToJapanese(Object commandParameter)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filters.Add(new FileDialogFilter() { Name = "CSV Files", Extensions = { "csv" } });
            //exportFileDialog.RestoreDirectory = true;

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

        private void OnDeleteFromList(object commandParameter)
        {
            if (0 <= SelectedVocabItemIndex && SelectedVocabItemIndex < VocabularyList.Count)
                VocabularyList.RemoveAt(SelectedVocabItemIndex);
        }

        private void OnGoUp(object commandParameter)
        {
            if (SelectedVocabItemIndex > 0)
            {
                var tmpIndex = SelectedVocabItemIndex;
                var tmpItem = VocabularyList[SelectedVocabItemIndex - 1];
                VocabularyList[SelectedVocabItemIndex - 1] = VocabularyList[SelectedVocabItemIndex];
                VocabularyList[SelectedVocabItemIndex] = tmpItem; // this line makes tmpIndex necessary because this line resets SelectedVocabItemIndex to -1
                SelectedVocabItemIndex = tmpIndex - 1;
            }
        }

        private void OnGoDown(object commandParameter)
        {
            if (SelectedVocabItemIndex < CurrentSession.addedVocabularyItems.Count - 1)
            {
                var tmpIndex = SelectedVocabItemIndex;
                var tmpItem = VocabularyList[SelectedVocabItemIndex + 1];
                VocabularyList[SelectedVocabItemIndex + 1] = VocabularyList[SelectedVocabItemIndex];
                VocabularyList[SelectedVocabItemIndex] = tmpItem; // this line makes tmpIndex necessary because this line resets SelectedVocabItemIndex to -1
                SelectedVocabItemIndex = tmpIndex + 1;
            }
        }

        private void ShowHtmlMessageBox()
        {
            var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime).MainWindow;
            MessageBox.Show(mainWindow, "Information", "Make sure to ENABLE \"Allow HTML in fields\" when importing the exported file into Anki!",
                MessageBoxButtons.Ok);
        }
    }
}
