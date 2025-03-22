using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Common.Collections;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Messages;

namespace JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels;

public partial class MeaningsViewModel : JishoTangoAssistantViewModelBase
{
    private readonly ICurrentJapaneseUserInputSelectionService currentSelectionService;
    
    [ObservableProperty]
    private ObservableRangeCollection<ObservableSimilarMeaningGroup> meaningGroups = [];

    public MeaningsViewModel(ICurrentJapaneseUserInputSelectionService currentSelectionService)
    {
        this.currentSelectionService = currentSelectionService;

        MeaningGroups = currentSelectionService.GetMeaningGroups();

        MeaningGroups.CollectionChanged += MeaningGroupsUpdateCollectionChanged;
        MeaningGroups.CollectionChanged += AutoEnableIfOnlyMeaning;
    }
    
    private void MeaningGroupsUpdateCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        PropertyChangedEventHandler outputTextHandler =
            (_, _) => WeakReferenceMessenger.Default.Send(new UpdateOutputTextMessage());
        PropertyChangedEventHandler textInputBackgroundHandler = 
            (_, _) => WeakReferenceMessenger.Default.Send(new UpdateVisualRelatedPropertiesMessage());
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
    
    private void AutoEnableIfOnlyMeaning(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e is not { NewItems.Count: 1, Action: NotifyCollectionChangedAction.Reset } 
            && currentSelectionService.OnlyOneMeaningInSelection()) 
            currentSelectionService.ChangeIsEnabledForAllMeanings(true);
    }
}