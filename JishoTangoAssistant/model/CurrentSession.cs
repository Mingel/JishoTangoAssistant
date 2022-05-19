using System.Collections.ObjectModel;

namespace JishoTangoAssistant
{
    class CurrentSession
    {
        public static bool running = false;

        public static JishoDatum[]? latestResult = null;

        public static ObservableVocabularyList addedVocabularyItems = new ObservableVocabularyList();

        public static bool userMadeChanges = false;

        public static int customFontSize = -1;
    }
}
