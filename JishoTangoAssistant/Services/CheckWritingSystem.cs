using System.Linq;

namespace JishoTangoAssistant.Services;

public static class CheckWritingSystem
{
    private const char Unicode_Romaji_Start = '\u0020';
    private const char Unicode_Romaji_End = '\u007E';
    private const char Unicode_Hiragana_Start = '\u3040';
    private const char Unicode_Hiragana_End = '\u309F';
    private const char Unicode_Katakana_Start = '\u30A0';
    private const char Unicode_Katakana_End = '\u30FF';
    private const char Unicode_Kanji_Start = '\u4E00';
    private const char Unicode_Kanji_End = '\u9FBF';

    public static bool OnlyContainsRomaji(string text)
    {
        return text.All(c => Unicode_Romaji_Start <= c && c <= Unicode_Romaji_End);
    }

    public static bool OnlyContainsHiragana(string text)
    {
        return text.All(c => Unicode_Hiragana_Start <= c && c <= Unicode_Hiragana_End);
    }

    public static bool OnlyContainsKatakana(string text)
    {
        return text.All(c => Unicode_Katakana_Start <= c && c <= Unicode_Katakana_End);
    }

    public static bool OnlyContainsKanji(string text)
    {
        return text.All(c => Unicode_Kanji_Start <= c && c <= Unicode_Kanji_End);
    }

    public static bool ContainsKanji(string text)
    {
        return text.Any(c => Unicode_Kanji_Start <= c && c <= Unicode_Kanji_End);
    }
}
