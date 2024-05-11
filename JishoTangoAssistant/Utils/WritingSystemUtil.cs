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
        return RomajiRegex().Matches(text).Count == text.Length;
    }

    public static bool OnlyContainsHiragana(string text)
    {
        return HiraganaRegex().Matches(text).Count == text.Length;
    }

    public static bool OnlyContainsKatakana(string text)
    {
        return KatakanaRegex().Matches(text).Count == text.Length;
    }

    public static bool OnlyContainsKanji(string text)
    {
        return KanjiRegex().Matches(text).Count == text.Length;
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

    [GeneratedRegex(RomajiRegexString)]
    private static partial Regex RomajiRegex();

    [GeneratedRegex(HiraganaRegexString)]
    private static partial Regex HiraganaRegex();

    [GeneratedRegex(KatakanaRegexString)]
    private static partial Regex KatakanaRegex();

    [GeneratedRegex(KanjiRegexString)]
    private static partial Regex KanjiRegex();
}
