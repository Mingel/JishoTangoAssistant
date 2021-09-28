using System.Collections.Generic;
using System.ComponentModel;

namespace MJapVocab
{
    class CurrentSession
    {
        public static bool running = false;

        public static JishoDatum[] latestResult = null;

        public static List<VocabularyItem> addedVocabularyItems = new List<VocabularyItem>();

        public static bool userMadeChanges = false;
    }
}
