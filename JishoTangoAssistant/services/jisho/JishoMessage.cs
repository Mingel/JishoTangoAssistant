namespace JishoTangoAssistant
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
        public JishoAttribution attribution;
    }

    class JishoJapaneseItem
    {
        public string word;
        public string reading;
    }

    class JishoSense
    {
        public string[] english_definitions;
        public string[] parts_of_speech;
        public string[] tags;
    }

    class JishoAttribution
    {
        public bool jmdict;
        public bool jmnedict;
        public string dbpedia;
    }
}
