using Avalonia.Controls;

namespace JishoTangoAssistant
{
    public class EnglishDefinitionCheckBox : CheckBox
    {
        public int EnglishDefinitionsRow { get; set; }

        public int EnglishDefinitionsColumn { get; set; }

        public int EnglishDefinitionsFlattenedIndex { get; set; }
    }
}
