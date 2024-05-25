using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Utils;
using JishoTangoAssistant.Models;
using JishoTangoAssistant.UI.Elements;

namespace JishoTangoAssistant.UI.ViewModels;

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
    private ObservableRangeCollection<ObservableSimilarMeaningGroup> meaningGroups = [];
    
    [ObservableProperty]
    private string additionalCommentsJapanese = string.Empty;

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
    private bool isDuplicateAndHasSameMeaning;

    [ObservableProperty]
    private bool isDuplicateAndHasDifferentMeaning;

    [ObservableProperty]
    private bool showPreEnteredInputList;
    
    [ObservableProperty]
    private int preEnteredInputIndex;
    
    [ObservableProperty]
    private string preEnteredInputRawList = string.Empty;
    
    public bool PreEnteredInputNoPrevPossible => PreEnteredInputIndex > 0;

    public bool PreEnteredInputNoNextPossible => PreEnteredInputIndex < PreEnteredInputs.Count - 1;

    #endregion
    
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private readonly IVocabularyListService vocabularyListService;

    private bool isProcessingInput;

    public JapaneseUserInputViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService, IVocabularyListService vocabularyListService)
    {
        this.currentSelectionService = currentSelectionService;
        this.vocabularyListService = vocabularyListService;

        Words = currentSelectionService.GetWords();
        OtherForms = currentSelectionService.GetOtherForms();
        MeaningGroups = currentSelectionService.GetMeaningGroups();

        MeaningGroups.CollectionChanged += MeaningGroupsUpdateCollectionChanged;
        MeaningGroups.CollectionChanged += AutoEnableIfOnlyMeaning;

        vocabularyListService.GetList().CollectionChanged += (_, _) => UpdateVisualRelatedProperties();
    }

    private void AutoEnableIfOnlyMeaning(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e is not { NewItems.Count: 1, Action: NotifyCollectionChangedAction.Reset } && currentSelectionService.OnlyOneMeaningInSelection()) 
            currentSelectionService.ChangeIsEnabledForAllMeanings(true);
    }

    #region auto-properties
    
    public string OutputFrontSideText
    {
        get
        {
            var outputTextStringBuilder = new StringBuilder();

            if (JapaneseToEnglishDirection &&
                SelectedIndexOfOtherForms >= 0)
            {
                outputTextStringBuilder.Append(!WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput);
            }
            else if (EnglishToJapaneseDirection)
            {
                var meaningsString = string.Join("; ", MeaningGroups.Select(g => g.SimilarMeanings)
                                                                    .SelectMany(g => g)
                                                                    .Where(m => m.IsEnabled)
                                                                    .Select(m => m.Value)); // TODO optimize
                outputTextStringBuilder.AppendLine(meaningsString);
                outputTextStringBuilder.Append(AdditionalCommentsJapanese.Trim());
            }
            return outputTextStringBuilder.ToString().Trim();
        }
    }

    public string OutputBackSideText
    {
        get
        {
            var outputTextStringBuilder = new StringBuilder();
            if (JapaneseToEnglishDirection)
            {
                var meaningsString = string.Join("; ", MeaningGroups.Select(g => g.SimilarMeanings)
                                                                    .SelectMany(g => g)
                                                                    .Where(m => m.IsEnabled)
                                                                    .Select(m => m.Value)); // TODO optimize
                if (!WriteInKana)
                    outputTextStringBuilder.AppendLine(ReadingOutput);
                outputTextStringBuilder.AppendLine(meaningsString);
                outputTextStringBuilder.Append(AdditionalCommentsJapanese.Trim());
            }
            else // EnglishToJapaneseDirection
            {
                if (SelectedIndexOfOtherForms >= 0)
                    outputTextStringBuilder.AppendLine(!WriteInKana ? OtherForms.ElementAt(SelectedIndexOfOtherForms) : ReadingOutput);
                if (!WriteInKana)
                    outputTextStringBuilder.Append(ReadingOutput);
            }
            return outputTextStringBuilder.ToString().Trim();
        }
    }

    private IList<string> PreEnteredInputs => PreEnteredInputRawList.Split(Environment.NewLine).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

    partial void OnJapaneseToEnglishDirectionChanged(bool value) => UpdateOutputText();

    private bool EnglishToJapaneseDirection => !JapaneseToEnglishDirection;

    partial void OnReadingOutputChanged(string value) => UpdateVisualRelatedProperties();

    partial void OnAdditionalCommentsJapaneseChanged(string value) 
    {
        currentSelectionService.SetAdditionalComments(value);
        if (!isProcessingInput)
        {
            UpdateOutputText();
            UpdateVisualRelatedProperties();
        }
    }

    partial void OnWriteInKanaChanged(bool value) 
    {
        currentSelectionService.SetWriteInKana(value);
        if (!isProcessingInput)
        {
            UpdateOutputText();
            UpdateVisualRelatedProperties();
        }
    }

    partial void OnSelectedIndexOfWordsChanged(int value) 
    {
        if (value >= 0)
        {
            currentSelectionService.SetSelectedWordsIndex(value);

            if (!isProcessingInput)
            {
                currentSelectionService.UpdateOtherForms();
                currentSelectionService.SetSelectedOtherFormsIndex(0);
                UpdateAllNonCollectionProperties();
                UpdateVisualRelatedProperties();
            }
        }
    }

    partial void OnSelectedIndexOfOtherFormsChanged(int value) 
    {
        if (value >= 0)
        {
            currentSelectionService.SetSelectedOtherFormsIndex(value);

            if (!isProcessingInput)
            {
                currentSelectionService.UpdateReading();
                UpdateAllNonCollectionProperties();
                UpdateOutputText();
                UpdateVisualRelatedProperties();
            }
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

        await vocabularyListService.AddAsync(addedItem);
        CurrentSession.userMadeChanges = true;
    }

    [RelayCommand]
    private async Task ProcessInput()
    {
        if (!isProcessingInput)
        {
            isProcessingInput = true;
            Input = RomajiKanaConverter.Convert(Input.Trim());
            await currentSelectionService.UpdateSelectionAsync(Input);
            UpdateAllNonCollectionProperties();
            UpdateOutputText();
            UpdateVisualRelatedProperties();
            isProcessingInput = false;
        }
    }

    [RelayCommand]
    private async Task ToggleShowPreEnteredInputList()
    {
        ShowPreEnteredInputList = !ShowPreEnteredInputList;

        if (!ShowPreEnteredInputList)
        {
            PreEnteredInputIndex = 0;
            if (!string.IsNullOrWhiteSpace(PreEnteredInputRawList))
            {
                PreEnteredInputRawList = string.Join(Environment.NewLine, PreEnteredInputs.Select(s => s.Trim()));
                Input = PreEnteredInputs[PreEnteredInputIndex];
                await ProcessInput();
                OnPropertyChanged(nameof(PreEnteredInputNoPrevPossible));
                OnPropertyChanged(nameof(PreEnteredInputNoNextPossible));
            }
        }
    }

    [RelayCommand]
    private async Task LoadPreEnteredInputListFromFile()
    {
        var filePickerTitle = "Open file to load search queries";

        var loadedFile = await FilePicker.LoadAsync(filePickerTitle);

        // this case can occur if user cancels file dialog
        if (loadedFile == null)
            return;

        var queryList = loadedFile.Split(Environment.NewLine);
        var filteredQueryList = loadedFile.Split(Environment.NewLine).Where(s => s.Length <= 150).ToArray();
        
        if (filteredQueryList.Length < queryList.Length)
            await MessageBoxUtil.CreateAndShowAsync("Error", $"The file contains {queryList.Length - filteredQueryList.Length} queries will not be used because they are too long!", MessageBoxButtons.Ok);

        if (filteredQueryList.Length == 0)
            return;
        
        // Filter out strings that are too long
        PreEnteredInputRawList = string.Join(Environment.NewLine, filteredQueryList);
        
        await ToggleShowPreEnteredInputList();
    }

    [RelayCommand]
    private async Task PrevPreEnteredInput()
    {
        if (PreEnteredInputIndex > 0)
        {
            PreEnteredInputIndex--;
            Input = PreEnteredInputs[PreEnteredInputIndex];
            await ProcessInput();
            OnPropertyChanged(nameof(PreEnteredInputNoPrevPossible));
            OnPropertyChanged(nameof(PreEnteredInputNoNextPossible));
        }
    }
    
    [RelayCommand]
    private async Task NextPreEnteredInput()
    {
        if (PreEnteredInputIndex < PreEnteredInputs.Count - 1)
        {
            PreEnteredInputIndex++;
            Input = PreEnteredInputs[PreEnteredInputIndex];
            await ProcessInput();
            OnPropertyChanged(nameof(PreEnteredInputNoPrevPossible));
            OnPropertyChanged(nameof(PreEnteredInputNoNextPossible));
        }
    }
    
    [RelayCommand]
    private void RemovePreEnteredInputs()
    {
        PreEnteredInputIndex = 0;
        PreEnteredInputRawList = string.Empty;
    }
    
    private void MeaningGroupsUpdateCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        PropertyChangedEventHandler outputTextHandler = (_, _) => UpdateOutputText();
        PropertyChangedEventHandler textInputBackgroundHandler = (_, _) => UpdateVisualRelatedProperties();
        if (e.NewItems != null)
        {
            foreach (ObservableSimilarMeaningGroup meaningGroup in e.NewItems)
            {
                foreach (var meaning in meaningGroup.SimilarMeanings)
                {
                    meaning.PropertyChanged += outputTextHandler;
                    meaning.PropertyChanged += textInputBackgroundHandler;
                }
            }
        }
        if (e.OldItems != null)
        {
            foreach (ObservableSimilarMeaningGroup meaningGroup in e.OldItems)
            {
                foreach (var meaning in meaningGroup.SimilarMeanings)
                {
                    meaning.PropertyChanged -= outputTextHandler;
                    meaning.PropertyChanged -= textInputBackgroundHandler; 
                }
            }
        }
    }

    private void UpdateAllNonCollectionProperties()
    {
        SelectedIndexOfWords = currentSelectionService.GetSelectedWordsIndex();
        SelectedIndexOfOtherForms = currentSelectionService.GetSelectedOtherFormsIndex();
        ReadingOutput = currentSelectionService.GetReadingOutput();
        AdditionalCommentsJapanese = currentSelectionService.GetAdditionalComments();
        WriteInKana = currentSelectionService.GetWriteInKana();
        ItemAdditionPossible = currentSelectionService.GetItemAdditionPossible();
    }

    private void UpdateOutputText()
    {
        OnPropertyChanged(nameof(OutputFrontSideText));
        OnPropertyChanged(nameof(OutputBackSideText));
    }

    private void UpdateVisualRelatedProperties()
    {
        var itemFromCurrentUserInput = currentSelectionService.CreateVocabularyItem();
        UpdateDuplicateMeaningChecks(itemFromCurrentUserInput);
        UpdateItemAdditionPossibleProperty(itemFromCurrentUserInput);
    }

    private void UpdateDuplicateMeaningChecks(VocabularyItem? itemFromCurrentUserInput)
    {
        var isDuplicate = itemFromCurrentUserInput != null && 
                            (vocabularyListService.ContainsWord(itemFromCurrentUserInput.Word)
                            // check if current word is hiragana only, but same word with katakana only exists, and vice versa
                            || WritingSystemUtil.OnlyContainsHiragana(itemFromCurrentUserInput.Word) 
                                && vocabularyListService.ContainsWord(WritingSystemUtil.HiraganaToKatakana(itemFromCurrentUserInput.Word))
                            || WritingSystemUtil.OnlyContainsKatakana(itemFromCurrentUserInput.Word) 
                                && vocabularyListService.ContainsWord(WritingSystemUtil.KatakanaToHiragana(itemFromCurrentUserInput.Word)));
        IsDuplicateAndHasSameMeaning = isDuplicate && vocabularyListService.Contains(itemFromCurrentUserInput!);
        IsDuplicateAndHasDifferentMeaning = isDuplicate && !vocabularyListService.Contains(itemFromCurrentUserInput!);
    }

    private void UpdateItemAdditionPossibleProperty(VocabularyItem? itemFromCurrentUserInput)
    {
        ItemAdditionPossible = currentSelectionService.GetItemAdditionPossible() &&
                               itemFromCurrentUserInput != null &&
                               !vocabularyListService.Contains(itemFromCurrentUserInput);
    }
}
