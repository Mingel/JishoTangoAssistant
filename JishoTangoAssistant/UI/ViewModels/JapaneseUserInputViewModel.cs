using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Utils;
using JishoTangoAssistant.Models;

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
    private ObservableRangeCollection<SimilarMeaningGroup> meaningGroups = [];
    
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
    private bool isDuplicateAndHasSameMeaning = false;

    [ObservableProperty]
    private bool isDuplicateAndHasDifferentMeaning = false;

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
                outputTextStringBuilder.Append(AdditionalComments);
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
                outputTextStringBuilder.Append(AdditionalComments);
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
    
    partial void OnJapaneseToEnglishDirectionChanged(bool value) => UpdateOutputText();

    private bool EnglishToJapaneseDirection => !JapaneseToEnglishDirection;

    partial void OnInputChanged(string value) => UpdateVisualRelatedProperties();

    partial void OnReadingOutputChanged(string value) => UpdateVisualRelatedProperties();

    partial void OnAdditionalCommentsChanged(string value) 
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


    private void MeaningGroupsUpdateCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        PropertyChangedEventHandler outputTextHandler = (_, _) => UpdateOutputText();
        PropertyChangedEventHandler textInputBackgroundHandler = (_, _) => UpdateVisualRelatedProperties();
        if (e.NewItems != null)
        {
            foreach (SimilarMeaningGroup meaningGroup in e.NewItems)
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
            foreach (SimilarMeaningGroup meaningGroup in e.OldItems)
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
        AdditionalComments = currentSelectionService.GetAdditionalComments();
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
        UpdateTextInputBackground(itemFromCurrentUserInput);
        UpdateItemAdditionPossibleProperty(itemFromCurrentUserInput);
    }

    private void UpdateTextInputBackground(VocabularyItem? itemFromCurrentUserInput)
    {
        var isDuplicate = itemFromCurrentUserInput != null && vocabularyListService.ContainsWord(itemFromCurrentUserInput.Word);
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
