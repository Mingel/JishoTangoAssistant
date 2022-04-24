using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MJapVocab
{
    class CurrentSession
    {
        public static bool running = false;

        public static JishoDatum[] latestResult = null;

        public static ObservableCollection<VocabularyItem> addedVocabularyItems = new ObservableCollection<VocabularyItem>();

        public static bool userMadeChanges = false;
    }
}
