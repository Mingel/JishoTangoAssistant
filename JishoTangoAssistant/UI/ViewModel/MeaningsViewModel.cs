using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JishoTangoAssistant.Models.Messages;

namespace JishoTangoAssistant.UI.ViewModel;

public partial class MeaningsViewModel : JishoTangoAssistantViewModelBase, 
    IRecipient<ResetMeaningsViewModelMessage>, 
    IRecipient<ClearCheckBoxesViewModelMessage>
{
    public MeaningsViewModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    [ObservableProperty]
    private ObservableCollection<string> meanings = [];

    [ObservableProperty]
    private ObservableCollection<int> selectedIndicesOfMeanings = [];
    
    public delegate void UpdateCheckBoxesEventHandler(int dataLength, IList<int> meaningsLengths, IList<string> flattenedMeanings);
    public event UpdateCheckBoxesEventHandler? UpdateCheckBoxesEvent;

    public delegate void ClearCheckBoxesEventHandler();
    public event ClearCheckBoxesEventHandler? ClearCheckBoxesEvent;
    
    public void Receive(ResetMeaningsViewModelMessage viewModelMessage)
    {
        Meanings.Clear();
        SelectedIndicesOfMeanings.Clear();
    }
    
    public void Receive(ClearCheckBoxesViewModelMessage viewModelMessage)
    {
        Meanings.Clear();
        SelectedIndicesOfMeanings.Clear();
    }
    
    public void ClearSelectedIndicesOfMeanings()
    {
        SelectedIndicesOfMeanings.Clear();
    }
    
    public void ChangeSelectedIndicesOfMeanings(int i, bool isSelected)
    {
        if (isSelected)
            SelectedIndicesOfMeanings.Add(i);
        else
            SelectedIndicesOfMeanings.Remove(i);
        UpdateOutputText();
    }

    public void UpdateOutputText()
    {
        WeakReferenceMessenger.Default.Send(new PerformUpdateOutputTextMessage(true));
    }

    partial void OnSelectedIndicesOfMeaningsChanged(ObservableCollection<int>? oldValue, ObservableCollection<int> newValue)
    {
        if (oldValue == null || !oldValue.SequenceEqual(newValue))
            WeakReferenceMessenger.Default.Send(new PerformUpdateTextInputBackgroundMessage(true));
    }
}
