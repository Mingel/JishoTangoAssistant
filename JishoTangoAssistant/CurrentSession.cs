using System.Collections.Generic;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.Infrastructure.Dtos;

namespace JishoTangoAssistant;

class CurrentSession
{
    public static IEnumerable<JishoDatum>? lastRetrievedResults = null;

    public static bool userMadeChanges = false;

    public const int DefaultFontSize = 28;

    public static int customFontSize = DefaultFontSize;

    public static string? loadedFilePath = null;
}