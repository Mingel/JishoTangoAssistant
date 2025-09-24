using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Presentation.UI.Messages;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels;

public partial class JapaneseUserInputViewModel : JishoTangoAssistantViewModelBase, IRecipient<ChangePreEnteredInputViewVisibilityMessage>
{
    [ObservableProperty]
    private bool showPreEnteredInputList;

    public JapaneseUserInputViewModel(
        ICurrentSessionService currentSessionService,
        ICurrentJapaneseUserInputSelectionService currentSelectionService,
        IVocabularyListService vocabularyListService)
    {
        WordSearchViewModel = new WordSearchViewModel(currentSelectionService);
        PreEnteredInputViewModel = new PreEnteredInputViewModel();
        SelectedInputInformationViewModel = new SelectedInputInformationViewModel(currentSelectionService);
        WriteKanaViewModel = new WriteKanaViewModel(currentSelectionService);
        OutputPanelViewModel = new OutputPanelViewModel(currentSelectionService, vocabularyListService);
        MeaningsViewModel = new MeaningsViewModel(currentSelectionService);
        AdditionalCommentsViewModel = new AdditionalCommentsViewModel(currentSelectionService);
        VocabularyItemAdditionViewModel = new VocabularyItemAdditionViewModel(currentSelectionService, vocabularyListService, currentSessionService);

        vocabularyListService.GetList().CollectionChanged += (_, _) => WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
        
        WeakReferenceMessenger.Default.Register(this);
    }
    
    private WordSearchViewModel WordSearchViewModel { get; }
    private PreEnteredInputViewModel PreEnteredInputViewModel { get; }
    private SelectedInputInformationViewModel SelectedInputInformationViewModel { get; }
    private WriteKanaViewModel WriteKanaViewModel { get; }
    private OutputPanelViewModel OutputPanelViewModel { get; }
    private MeaningsViewModel MeaningsViewModel { get; }
    private AdditionalCommentsViewModel AdditionalCommentsViewModel { get; }
    private VocabularyItemAdditionViewModel VocabularyItemAdditionViewModel { get; }
    
    public void Receive(ChangePreEnteredInputViewVisibilityMessage message)
    {
        ShowPreEnteredInputList = message.Value;
    }
}
