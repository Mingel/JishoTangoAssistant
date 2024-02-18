using JishoTangoAssistant.Services.Jisho;

namespace JishoTangoAssistant.Model;

class CurrentSession
{
    public static bool running = false;

    public static JishoDatum[]? lastRetrievedResults = null;

    public static ObservableVocabularyList addedVocabularyItems = [];

    public static bool userMadeChanges = false;

    public const int DefaultFontSize = 28;

    public static int customFontSize = DefaultFontSize;
}