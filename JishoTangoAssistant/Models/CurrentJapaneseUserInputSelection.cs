using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JishoTangoAssistant.Models;

public class CurrentJapaneseUserInputSelection
{
    private int selectedWordsIndex;
    private bool writeInKana;
    private string additionalComments = string.Empty;
    public ObservableCollection<string> Words { get; } = [];
    public ObservableCollection<string> OtherForms { get; } = [];
    public ObservableRangeCollection<SimilarMeaningsGroup> Meanings { get; } = [];
    public string ReadingOutput { get; set; } = string.Empty;
    public bool ItemAdditionPossible { get; set; }
    public int SelectedOtherFormsIndex { get; set; } = -1;

    public int SelectedWordsIndex
    {
        get => selectedWordsIndex;
        set
        {
            selectedWordsIndex = value;
            SelectedWordsIndexChangeEvent?.Invoke(this, new SelectedIndexEventArgs(Meanings));
        }
    }

    public string AdditionalComments
    {
        get => additionalComments;
        set
        {
            additionalComments = value;
            AdditionalCommentsEvent?.Invoke(this, new AdditionalCommentsEventArgs(additionalComments));
        }
    }
    
    public bool WriteInKana
    {
        get => writeInKana;
        set
        {
            writeInKana = value;
            WriteInKanaEvent?.Invoke(this, new WriteInKanaEventArgs(writeInKana));
        }
    }

    public event SelectedIndexEventHandler? SelectedWordsIndexChangeEvent;
    public delegate void SelectedIndexEventHandler(object sender, SelectedIndexEventArgs e);
    
    public event AdditionalCommentsEventHandler? AdditionalCommentsEvent;
    public delegate void AdditionalCommentsEventHandler(object sender, AdditionalCommentsEventArgs e);
    
    public event WriteInKanaEventHandler? WriteInKanaEvent;
    public delegate void WriteInKanaEventHandler(object sender, WriteInKanaEventArgs e);
}

public class SelectedIndexEventArgs(IEnumerable<SimilarMeaningsGroup> meanings) : EventArgs
{
    public IEnumerable<SimilarMeaningsGroup> Meanings { get; } = meanings;
}

public class AdditionalCommentsEventArgs(string additionalComments) : EventArgs
{
    public string AdditionalComments { get; } = additionalComments;
}

public class WriteInKanaEventArgs(bool writeInKana) : EventArgs
{
    public bool WriteInKana { get; } = writeInKana;
}