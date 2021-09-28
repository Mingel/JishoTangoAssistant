using System;
using System.Collections.Generic;
using System.Text;

namespace MJapVocab
{
    class JishoMessage
    {
        public JishoMeta meta;
        public JishoDatum[] data;
    }

    class JishoMeta
    {
        public int status;
    }

    class JishoDatum
    {
        public string slug;
        public JishoJapaneseItem[] japanese;
        public JishoSense[] senses;
    }

    class JishoJapaneseItem
    {
        public string word;
        public string reading;
    }

    class JishoSense
    {
        public string[] english_definitions;
        public string[] tags;
    }
}
