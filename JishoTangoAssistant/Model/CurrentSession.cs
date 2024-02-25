using JishoTangoAssistant.Model.Jisho;
using JishoTangoAssistant.Services;

namespace JishoTangoAssistant.Model;

class CurrentSession
{
    public static bool running = false;

    public static JishoDatum[]? lastRetrievedResults = null;

    public static bool userMadeChanges = false;

    public const int DefaultFontSize = 28;

    public static int customFontSize = DefaultFontSize;

    public static readonly VocabularyListService VocabularyListService = new();
}