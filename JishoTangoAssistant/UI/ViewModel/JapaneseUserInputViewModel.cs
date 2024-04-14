using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using JishoTangoAssistant.Utils;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.Services;

namespace JishoTangoAssistant.UI.ViewModel;

public partial class JapaneseUserInputViewModel : JishoTangoAssistantViewModelBase
{
    #region attributes
    [ObservableProperty]
    private string input = string.Empty;

    [ObservableProperty]
    private bool writeInKana;

    [ObservableProperty]
    private bool japaneseToEnglishDirection = true;

    [ObservableProperty]
    private bool showFrontSide = true;

    [ObservableProperty]
    private ObservableRangeCollection<SimilarMeaningsGroup> meanings = [];

    private readonly List<int> selectedIndicesOfMeanings = [];
    
    [ObservableProperty]
    private string additionalComments = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> words = [];

    [ObservableProperty]
    private int selectedIndexOfWords = -1;

    [ObservableProperty]
    private ObservableCollection<string> otherForms = [];

    [ObservableProperty]
    private int selectedIndexOfOtherForms = -1;

    [ObservableProperty]
    private string readingOutput = string.Empty;

    [ObservableProperty]
    private int selectedVocabItemIndex = -1;

    [ObservableProperty]
    private bool itemAdditionPossible;
    
    [ObservableProperty]
    private Color textInputBackground = App.UsesDarkMode() ? Color.Parse("#66000000") : Color.Parse("#66ffffff");

    #endregion
    
    private readonly CurrentJapaneseUserInputSelectionService currentSelectionService;

    public JapaneseUserInputViewModel()
    {
        currentSelectionService = new CurrentJapaneseUserInputSelectionService(); // TODO DI
        Words = currentSelectionService.GetWords();
        OtherForms = currentSelectionService.GetOtherForms();
        Meanings = currentSelectionService.GetMeanings();

        Meanings.CollectionChanged += MeaningsUpdateCollectionChanged;
        Meanings.CollectionChanged += AutoEnableIfOnlyMeaning;

        CurrentSession.VocabularyListService.GetList().CollectionChanged += (_, _) => UpdateTextInputBackground();
    }

    private void AutoEnableIfOnlyMeaning(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e is not { NewItems.Count: 1, Action: NotifyCollectionChangedAction.Reset } && currentSelectionService.OnlyOneMeaningInSelection()) 
            currentSelectionService.ChangeIsEnabledForAllMeanings(true);
    }

    #region auto-properties

    public string OutputText
    {
        get
        {
            var outputTextStringBuilder = new StringBuilder();

            if (JapaneseToEnglishDirection && ShowFrontSide && SelectedIndexOfOtherForms >= 0)
            {
                outputTextStringBuilder.Append(!WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput);
            }
            else if (JapaneseToEnglishDirection && ShowBackSide)
            {
                var meaningsString = string.Join("; ", Meanings.Select(g => g.SimilarMeanings)
                                                               .SelectMany(g => g)
                                                               .Where(m => m.IsEnabled)
                                                               .Select(m => m.Value)); // TODO optimize
                if (!WriteInKana)
                {
                    outputTextStringBuilder.Append(ReadingOutput);
                    if (!string.IsNullOrWhiteSpace(meaningsString))
                        outputTextStringBuilder.Append(Environment.NewLine);
                }
                outputTextStringBuilder.Append(meaningsString);
                if (!string.IsNullOrWhiteSpace(AdditionalComments))
                {
                    outputTextStringBuilder.Append(Environment.NewLine);
                    outputTextStringBuilder.Append(AdditionalComments);
                }
            }
            else if (EnglishToJapaneseDirection && ShowFrontSide)
            {
                var meaningsString = string.Join("; ", Meanings.Select(g => g.SimilarMeanings)
                                                               .SelectMany(g => g)
                                                               .Where(m => m.IsEnabled)
                                                               .Select(m => m.Value)); // TODO optimize
                outputTextStringBuilder.Append(meaningsString);
                if (!string.IsNullOrWhiteSpace(AdditionalComments))
                {
                    outputTextStringBuilder.Append(Environment.NewLine);
                    outputTextStringBuilder.Append(AdditionalComments);
                }
            }
            else // (EnglishToJapaneseDirection && ShowBackSide)
            {
                if (SelectedIndexOfOtherForms >= 0)
                    outputTextStringBuilder.Append(!WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput);
                if (!WriteInKana)
                {
                    outputTextStringBuilder.Append(Environment.NewLine);
                    outputTextStringBuilder.Append(ReadingOutput);
                }
            }
            return outputTextStringBuilder.ToString();
        }
    }
    
    partial void OnJapaneseToEnglishDirectionChanged(bool value) => UpdateOutputText();

    private bool EnglishToJapaneseDirection => !JapaneseToEnglishDirection;

    partial void OnShowFrontSideChanged(bool value) => UpdateOutputText();

    private bool ShowBackSide => !ShowFrontSide;

    partial void OnInputChanged(string value) => UpdateTextInputBackground();

    partial void OnReadingOutputChanged(string value)
    {
        UpdateTextInputBackground();
    }

    partial void OnAdditionalCommentsChanged(string value) 
    {
        currentSelectionService.SetAdditionalComments(value);
        UpdateOutputText();
        UpdateTextInputBackground();
    }

    partial void OnWriteInKanaChanged(bool value) 
    {
        currentSelectionService.SetWriteInKana(value);
        UpdateOutputText();
        UpdateTextInputBackground();
    }

    partial void OnSelectedIndexOfWordsChanged(int value) 
    {
        if (value >= 0)
        {
            currentSelectionService.SetSelectedWordsIndex(value);
            currentSelectionService.UpdateOtherForms();
            currentSelectionService.SetSelectedOtherFormsIndex(0);
            UpdateAllNonCollectionProperties();
            UpdateTextInputBackground();
        }
    }

    partial void OnSelectedIndexOfOtherFormsChanged(int value) 
    {
        if (value >= 0)
        {
            currentSelectionService.SetSelectedOtherFormsIndex(value);
            UpdateOutputText();
            UpdateTextInputBackground();
        }
    }
    #endregion

    [RelayCommand]
    private async Task AddToList()
    {
        if (CurrentSession.lastRetrievedResults == null)
            return;

        var addedItem = currentSelectionService.CreateVocabularyItem();

        if (addedItem == null)
            return;

        await CurrentSession.VocabularyListService.AddAsync(addedItem);
        CurrentSession.userMadeChanges = true;
    }

    [RelayCommand]
    private async Task ProcessInput()
    {
        if (!CurrentSession.running)
        {
            CurrentSession.running = true;
            Input = RomajiKanaConverter.Convert(Input.Trim());
            await currentSelectionService.UpdateSelectionAsync(Input);
            UpdateAllNonCollectionProperties();
            UpdateOutputText();
            CurrentSession.running = false;
        }
    }


    private void MeaningsUpdateCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        PropertyChangedEventHandler handler = (_, _) => UpdateOutputText();
        if (e.NewItems != null)
        {
            foreach (SimilarMeaningsGroup meaningsGroup in e.NewItems)
            {
                foreach (var meaning in meaningsGroup.SimilarMeanings)
                {
                    meaning.PropertyChanged += handler;
                }
            }
        }
        if (e.OldItems != null)
        {
            foreach (SimilarMeaningsGroup meaningsGroup in e.OldItems)
            {
                foreach (var meaning in meaningsGroup.SimilarMeanings)
                {
                    meaning.PropertyChanged -= handler;
                }
            }
        }
    }

    private void UpdateAllNonCollectionProperties()
    {
        SelectedIndexOfWords = currentSelectionService.GetSelectedWordsIndex();
        SelectedIndexOfOtherForms = currentSelectionService.GetSelectedOtherFormsIndex();
        ReadingOutput = currentSelectionService.GetReadingOutput();
        AdditionalComments = currentSelectionService.GetAdditionalComments();
        WriteInKana = currentSelectionService.GetWriteInKana();
        ItemAdditionPossible = currentSelectionService.GetItemAdditionPossible();
    }

    private void UpdateOutputText()
    {
        OnPropertyChanged(nameof(OutputText));
    }

    private void UpdateTextInputBackground()
    {
        var color = InputTextColorNoDuplicate();
        var itemFromCurrentUserInput = currentSelectionService.CreateVocabularyItem();
        if (itemFromCurrentUserInput != null && CurrentSession.VocabularyListService.ContainsWord(itemFromCurrentUserInput.Word))
        {
            color = CurrentSession.VocabularyListService.Contains(itemFromCurrentUserInput) ? InputTextColorSameMeaning() : InputTextColorDifferentMeaning();
        }
        
        TextInputBackground = Color.Parse(color);
    }
    private string InputTextColorNoDuplicate()
    {
        return App.UsesDarkMode() ? "#66000000" : "#66FFFFFF";
    }

    private string InputTextColorDifferentMeaning()
    {
        return App.UsesDarkMode() ? "#667D7D69" : "#66FAFAD2";
    }
    private string InputTextColorSameMeaning()
    {
        return App.UsesDarkMode() ? "#66744B3D" : "#66E9967A";
    }
}
