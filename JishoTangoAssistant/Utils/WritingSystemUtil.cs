using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JishoTangoAssistant.Utils;

public static partial class WritingSystemUtil
{
    private const string RomajiRegexString = "[\u0020-\u007E]+";
    private const string HiraganaRegexString = "[\u3040-\u309F]+";
    private const string KatakanaRegexString = "[\u30A0-\u30FF]+";
    private const string KanjiRegexString = "[\x3400-\x4DB5\x4E00-\x9FCB\xF900-\xFA6A]+";

    public static bool OnlyContainsRomaji(string text)
    {
        return RomajiRegex().Matches(text).FirstOrDefault()?.Length == text.Length;
    }

    public static bool OnlyContainsHiragana(string text)
    {
        return HiraganaRegex().Matches(text).FirstOrDefault()?.Length == text.Length;
    }

    public static bool OnlyContainsKatakana(string text)
    {
        return KatakanaRegex().Matches(text).FirstOrDefault()?.Length == text.Length;
    }

    public static bool OnlyContainsKanji(string text)
    {
        return KanjiRegex().Matches(text).FirstOrDefault()?.Length == text.Length;
    }

    public static bool OnlyContainsKana(string text)
    {
        return OnlyContainsHiragana(text) || OnlyContainsKatakana(text);
    }

    public static bool ContainsKanji(string text)
    {
        return KanjiRegex().Matches(text).Count > 0;
    }

    public static string FilterKanji(string text)
    {
        return KanjiRegex().Match(text).Value;
    }

    public static char HiraganaToKatakana(char hiraganaLetter)
    {
        if (!OnlyContainsHiragana(hiraganaLetter.ToString()))
            throw new ArgumentException("Letter is not hiragana", nameof(hiraganaLetter));
        return (char)(hiraganaLetter + 96);
    }

    public static string HiraganaToKatakana(string hiraganaLetters)
    {
        if (!OnlyContainsHiragana(hiraganaLetters))
            throw new ArgumentException("Letters are not all hiragana", nameof(hiraganaLetters));
        return string.Join(string.Empty, hiraganaLetters.Select(HiraganaToKatakana));
    }

    public static char KatakanaToHiragana(char katakanaLetter)
    {
        if (!OnlyContainsKatakana(katakanaLetter.ToString()))
            throw new ArgumentException("Letter is not katakana", nameof(katakanaLetter));
        return (char)(katakanaLetter - 96);
    }

    public static string KatakanaToHiragana(string katakanaLetters)
    {
        if (!OnlyContainsKatakana(katakanaLetters))
            throw new ArgumentException("Letters are not all katakana", nameof(katakanaLetters));
        return string.Join(string.Empty, katakanaLetters.Select(KatakanaToHiragana));
    }

    [GeneratedRegex(RomajiRegexString)]
    private static partial Regex RomajiRegex();

    [GeneratedRegex(HiraganaRegexString)]
    private static partial Regex HiraganaRegex();

    [GeneratedRegex(KatakanaRegexString)]
    private static partial Regex KatakanaRegex();

    [GeneratedRegex(KanjiRegexString)]
    private static partial Regex KanjiRegex();
}
