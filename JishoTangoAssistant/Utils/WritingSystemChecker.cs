using System.Linq;

namespace JishoTangoAssistant.Utils;

public static class WritingSystemChecker
{
    private const char UnicodeRomajiStart = '\u0020';
    private const char UnicodeRomajiEnd = '\u007E';
    private const char UnicodeHiraganaStart = '\u3040';
    private const char UnicodeHiraganaEnd = '\u309F';
    private const char UnicodeKatakanaStart = '\u30A0';
    private const char UnicodeKatakanaEnd = '\u30FF';
    private const char UnicodeKanjiStart = '\u4E00';
    private const char UnicodeKanjiEnd = '\u9FBF';

    public static bool OnlyContainsRomaji(string text)
    {
        return text.All(c => c is >= UnicodeRomajiStart and <= UnicodeRomajiEnd);
    }

    public static bool OnlyContainsHiragana(string text)
    {
        return text.All(c => c is >= UnicodeHiraganaStart and <= UnicodeHiraganaEnd);
    }

    public static bool OnlyContainsKatakana(string text)
    {
        return text.All(c => c is >= UnicodeKatakanaStart and <= UnicodeKatakanaEnd);
    }

    public static bool OnlyContainsKanji(string text)
    {
        return text.All(c => c is >= UnicodeKanjiStart and <= UnicodeKanjiEnd);
    }

    public static bool ContainsKanji(string text)
    {
        return text.Any(c => c is >= UnicodeKanjiStart and <= UnicodeKanjiEnd);
    }
}
