using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Utils;
using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels;

public partial class OutputPanelViewModel : JishoTangoAssistantViewModelBase, IRecipient<UpdateVisualRelatedPropertiesMessage>, IRecipient<UpdateOutputTextMessage>
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    private readonly IVocabularyListService vocabularyListService;

    [ObservableProperty]
    private bool isDuplicateAndHasSameMeaning;

    [ObservableProperty]
    private bool isDuplicateAndHasDifferentMeaning;
    
    [ObservableProperty]
    private bool japaneseToEnglishDirection = true;
    
    partial void OnJapaneseToEnglishDirectionChanged(bool value) => WeakReferenceMessenger.Default.Send(new UpdateOutputTextMessage());

    private bool EnglishToJapaneseDirection => !JapaneseToEnglishDirection;

    public OutputPanelViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService, IVocabularyListService vocabularyListService)
    {
        this.currentSelectionService = currentSelectionService;
        this.vocabularyListService = vocabularyListService;
        WeakReferenceMessenger.Default.Register<UpdateVisualRelatedPropertiesMessage>(this);
        WeakReferenceMessenger.Default.Register<UpdateOutputTextMessage>(this);
    }
    
    public string OutputFrontSideText
    {
        get
        {
            var outputTextStringBuilder = new StringBuilder();

            if (JapaneseToEnglishDirection &&
                currentSelectionService.GetSelectedOtherFormsIndex() >= 0)
            {
                outputTextStringBuilder.Append(!currentSelectionService.GetWriteInKana() ? currentSelectionService.GetOtherForms().ElementAt(currentSelectionService.GetSelectedOtherFormsIndex()) : currentSelectionService.GetReadingOutput());
            }
            else if (EnglishToJapaneseDirection)
            {
                var meaningsString = string.Join("; ", currentSelectionService.GetMeaningGroups().Select(g => g.SimilarMeanings)
                                                                    .SelectMany(g => g)
                                                                    .Where(m => m.IsEnabled)
                                                                    .Select(m => m.Value)); // TODO optimize
                outputTextStringBuilder.AppendLine(meaningsString);
                outputTextStringBuilder.Append(currentSelectionService.GetAdditionalComments().Trim());
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
                var meaningsString = string.Join("; ", currentSelectionService.GetMeaningGroups().Select(g => g.SimilarMeanings)
                                                                    .SelectMany(g => g)
                                                                    .Where(m => m.IsEnabled)
                                                                    .Select(m => m.Value)); // TODO optimize
                if (!currentSelectionService.GetWriteInKana())
                    outputTextStringBuilder.AppendLine(currentSelectionService.GetReadingOutput());
                outputTextStringBuilder.AppendLine(meaningsString);
                outputTextStringBuilder.Append(currentSelectionService.GetAdditionalComments().Trim());
            }
            else // EnglishToJapaneseDirection
            {
                if (currentSelectionService.GetSelectedOtherFormsIndex() >= 0)
                    outputTextStringBuilder.AppendLine(!currentSelectionService.GetWriteInKana() ? currentSelectionService.GetOtherForms().ElementAt(currentSelectionService.GetSelectedOtherFormsIndex()) : currentSelectionService.GetReadingOutput());
                if (!currentSelectionService.GetWriteInKana())
                    outputTextStringBuilder.Append(currentSelectionService.GetReadingOutput());
            }
            return outputTextStringBuilder.ToString().Trim();
        }
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

    public void Receive(UpdateVisualRelatedPropertiesMessage message)
    {
        var itemFromCurrentUserInput = currentSelectionService.CreateVocabularyItem();
        UpdateDuplicateMeaningChecks(itemFromCurrentUserInput);
    }

    public void Receive(UpdateOutputTextMessage message)
    {
        OnPropertyChanged(nameof(OutputFrontSideText));
        OnPropertyChanged(nameof(OutputBackSideText));
    }
}