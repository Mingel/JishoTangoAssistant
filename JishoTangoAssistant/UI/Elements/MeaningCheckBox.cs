using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace JishoTangoAssistant.UI.Elements
{
    public class MeaningCheckBox : CheckBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(CheckBox);

        public int MeaningsRow { get; set; }

        public int MeaningsColumn { get; set; }

        public int MeaningsFlattenedIndex { get; set; }
    }
}
