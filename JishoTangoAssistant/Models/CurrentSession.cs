using System.Collections.Generic;
using JishoTangoAssistant.Models.Jisho;

namespace JishoTangoAssistant.Models;

class CurrentSession
{
    public static bool running = false;

    public static IList<JishoDatum>? lastRetrievedResults = null;

    public static bool userMadeChanges = false;

    public const int DefaultFontSize = 28;

    public static int customFontSize = DefaultFontSize;
}