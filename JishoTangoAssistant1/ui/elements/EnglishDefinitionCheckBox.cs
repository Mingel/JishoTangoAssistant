using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace JishoTangoAssistant.UI.Elements
{
    public class EnglishDefinitionCheckBox : CheckBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(CheckBox);

        public int EnglishDefinitionsRow { get; set; }

        public int EnglishDefinitionsColumn { get; set; }

        public int EnglishDefinitionsFlattenedIndex { get; set; }
    }
}
