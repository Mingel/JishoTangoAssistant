using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MJapVocab
{
    public class EnglishDefinitionCheckBox : CheckBox
    {
        public int EnglishDefinitionsRow { get; set; }

        public int EnglishDefinitionsColumn { get; set; }

        public int EnglishDefinitionsFlattenedIndex { get; set; }
    }
}
