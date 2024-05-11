using System.Linq;
using System.Text.RegularExpressions;

namespace JishoTangoAssistant.Utils;

public static class WritingSystemUtil
{
    private const string RomajiRegex = "[\u0020-\u007E]";
    private const string HiraganaRegex = "[\u3040-\u309F]";
    private const string KatakanaRegex = "[\u30A0-\u30FF]";
    private const string KanjiRegex = "[\x3400-\x4DB5\x4E00-\x9FCB\xF900-\xFA6A]";

    public static bool OnlyContainsRomaji(string text)
    {
        return new Regex($"{RomajiRegex}*").Match(text).Success;
    }

    public static bool OnlyContainsHiragana(string text)
    {
        return new Regex($"{HiraganaRegex}*").Match(text).Success;
    }

    public static bool OnlyContainsKatakana(string text)
    {
        return new Regex($"{KatakanaRegex}*").Match(text).Success;
    }

    public static bool OnlyContainsKanji(string text)
    {
        return new Regex($"{KanjiRegex}*").Match(text).Success;
    }

    public static bool OnlyContainsKana(string text)
    {
        return OnlyContainsHiragana(text) || OnlyContainsKatakana(text);
    }

    public static bool ContainsKanji(string text)
    {
        return new Regex($".*{KanjiRegex}.*").Match(text).Success;
    }

    public static string FilterKanji(string text)
    {
        return new Regex(KanjiRegex).Match(text).Value;
    }
}
