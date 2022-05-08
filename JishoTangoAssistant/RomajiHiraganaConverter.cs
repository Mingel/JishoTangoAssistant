using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JishoTangoAssistant
{
    public static class RomajiHiraganaConverter
    {
        #region conversion-list
        private static readonly Dictionary<string, string> _romajiToHiraganaDictionary = new Dictionary<string, string>()
        {
            {"a", "あ"},{"i", "い"},{"u", "う"},{"e", "え"},{"o", "お"},
            {"ka", "か"},{"ki", "き"},{"ku", "く"},{"ke", "け"},{"ko", "こ"},
            {"ga", "が"},{"gi", "ぎ"},{"gu", "ぐ"},{"ge", "げ"},{"go", "ご"},
            {"sa", "さ"},{"shi", "し"},{"si", "し"},{"su", "す"},{"se", "せ"},{"so", "そ"},
            {"za", "ざ"},{"ji", "じ"},{"zu", "ず"},{"ze", "ぜ"},{"zo", "ぞ"},{"zi", "じ"},
            {"ta", "た"},{"chi", "ち"},{"tsu", "つ"},{"tu", "つ"},{"te", "て"},{"to", "と"},{"ti", "ち"},
            {"da", "だ"},{"di", "ぢ"},{"du", "づ"},{"de", "で"},{"do", "ど"},{"dzu", "づ"},
            {"na", "な"},{"ni", "に"},{"nu", "ぬ"},{"ne", "ね"},{"no", "の"},
            {"ha", "は"},{"hi", "ひ"},{"fu", "ふ"},{"hu", "ふ"},{"he", "へ"},{"ho", "ほ"},
            {"ba", "ば"},{"bi", "び"},{"bu", "ぶ"},{"be", "べ"},{"bo", "ぼ"},
            {"pa", "ぱ"},{"pi", "ぴ"},{"pu", "ぷ"},{"pe", "ぺ"},{"po", "ぽ"},
            {"ma", "ま"},{"mi", "み"},{"mu", "む"},{"me", "め"},{"mo", "も"},
            {"ya", "や"},{"yu", "ゆ"},{"yo", "よ"},
            {"ra", "ら"},{"ri", "り"},{"ru", "る"},{"re", "れ"},{"ro", "ろ"},
            {"la", "ら"},{"li", "り"},{"lu", "る"},{"le", "れ"},{"lo", "ろ"},
            {"wa", "わ"},{"wo", "を"},
            {"n", "ん"},
            {"kya", "きゃ"},{"kyu", "きゅ"},{"kyo", "きょ"},{"kye", "きぇ"},{"kyi", "きぃ"},
            {"gya", "ぎゃ"},{"gyu", "ぎゅ"},{"gyo", "ぎょ"},{"gye", "ぎぇ"},{"gyi", "ぎぃ"},
            {"kwa", "くぁ"},{"kwi", "くぃ"},{"kwu", "くぅ"},{"kwe", "くぇ"},{"kwo", "くぉ"},
            {"qwa", "ぐぁ"},{"gwi", "ぐぃ"},{"gwu", "ぐぅ"},{"gwe", "ぐぇ"},{"gwo", "ぐぉ"},
            {"sha", "しゃ"},{"syi", "しぃ"},{"shu", "しゅ"},{"she", "しぇ"},{"sho", "しょ"},
            {"sya", "しゃ"},{"syu", "しゅ"},{"sye", "しぇ"},{"syo", "しょ"},
            {"ja", "じゃ"},{"jya", "じゃ"},{"ju", "じゅ"},{"jyu", "じゅ"},{"je", "じぇ"},{"jye", "じぇ"},{"jo", "じょ"},{"jyo", "じょ"},{"jyi", "じぃ"},
            {"zya", "じゃ"},{"zyu", "じゅ"},{"zyye", "じぇ"},{"zyo", "じょ"},{"zyi", "じぃ"},
            {"swa", "すぁ"},{"swi", "すぃ"},{"swu", "すぅ"},{"swe", "すぇ"},{"swo", "すぉ"},
            {"cha", "ちゃ"},{"chu", "ちゅ"},{"tye", "ちぇ"},{"cho", "ちょ"},{"tyi", "ちぃ"},
            {"tya", "ちゃ"},{"tyu", "ちゅ"},{"che", "ちぇ"},{"tyo", "ちょ"},
            {"dyi", "ぢぃ"},{"dye", "ぢぇ"},{"dya", "ぢゃ"},{"dyu", "ぢゅ"},{"dyo", "ぢょ"},
            {"tsa", "つぁ"},{"tsi", "つぃ"},{"tse", "つぇ"},{"tso", "つぉ"},
            {"tha", "てゃ"},{"thi", "てぃ"},{"thu", "てゅ"},{"the", "てぇ"},{"tho", "てょ"},
            {"twa", "とぁ"},{"twi", "とぃ"},{"twu", "とぅ"},{"twe", "とぇ"},{"two", "とぉ"},
            {"dha", "でゃ"},{"dhi", "でぃ"},{"dhu", "でゅ"},{"dhe", "でぇ"},{"dho", "でょ"},
            {"dwa", "どぁ"},{"dwi", "どぃ"},{"dwu", "どぅ"},{"dwe", "どぇ"},{"dwo", "どぉ"},
            {"nya", "にゃ"},{"nyu", "にゅ"},{"nyo", "にょ"},{"nye", "にぇ"},{"nyi", "にぃ"},
            {"hya", "ひゃ"},{"hyi", "ひぃ"},{"hyu", "ひゅ"},{"hye", "ひぇ"},{"hyo", "ひょ"},
            {"bya", "びゃ"},{"byi", "びぃ"},{"byu", "びゅ"},{"bye", "びぇ"},{"byo", "びょ"},
            {"pya", "ぴゃ"},{"pyi", "ぴぃ"},{"pyu", "ぴゅ"},{"pye", "ぴぇ"},{"pyo", "ぴょ"},
            {"fwa", "ふぁ"},{"fa", "ふぁ"},{"fyi", "ふぃ"},{"fi", "ふぃ"},{"fye", "ふぇ"},
            {"fe", "ふぇ"},{"fwo", "ふぉ"},{"fo", "ふぉ"},{"fwu", "ふぅ"},
            {"fya", "ふゃ"},{"fyu", "ふゅ"},{"fyo", "ふょ"},
            {"mya", "みゃ"},{"myi", "みぃ"},{"myu", "みゅ"},{"mye", "みぇ"},{"myo", "みょ"},
            {"rya", "りゃ"},{"ryi", "りぃ"},{"ryu", "りゅ"},{"rye", "りぇ"},{"ryo", "りょ"},
            {"va", "ゔぁ"},{"vyi", "ゔぃ"},{"vu", "ゔ"},{"vye", "ゔぇ"},{"vo", "ゔぉ"},
            {"vya", "ゔゃ"},{"vyu", "ゔゅ"},{"vyo", "ゔょ"},{"wha", "うぁ"},
            {"ye", "いぇ"},
            {"who", "うぉ"},{"whi", "うぃ"},{"whe", "うぇ"},{"wi", "うぃ"},{"we", "うぇ"},
            {"xa", "ぁ"},{"xi", "ぃ"},{"xu", "ぅ"},{"xe", "ぇ"},{"xo", "ぉ"},{"xka", "ゕ"},{"xke", "ゖ"},{"xwa", "ゎ"},
            {"tchi", "っち"},
            {"-", "ー"},{"?", "？"},
        };
        #endregion

        private static List<string> _possibleConvertableRomajiPhrases = new List<string>(RomajiHiraganaConverter._romajiToHiraganaDictionary.Keys);

        private static readonly List<string> _noConversionToSokuon = new List<string>() { "a", "e", "i", "o", "u", "n", "va", "vyi", "vu", "vye", "vo", "vya", "vyu", "vyo", "-", "?" };
        private static readonly List<string> _vowels = new List<string>() { "a", "e", "i", "o", "u" };

        public static string Convert(string hiraganaInput)
        {
           var romajiOutput = "";
            // Consider: "m" not at the end of the string;
            var toMatch = "";
            for (int i = 0; i < hiraganaInput.Length; i++)
            {
                char c = hiraganaInput[i];
                char lowerCaseChar = Char.ToLower(c);
                if (IsInvalidCharacterForConversion(lowerCaseChar))
                {
                    romajiOutput += toMatch + lowerCaseChar;
                    toMatch = "";
                    continue;
                }
                
                toMatch += lowerCaseChar;

                int initialToMatchLength = toMatch.Length;
                for (int j = 0; j < initialToMatchLength; j++)
                {
                    if (_romajiToHiraganaDictionary.ContainsKey(toMatch))
                    {
                        // look ahead: special case 'n'
                        if (CheckSpecialCaseN(hiraganaInput, toMatch, i))
                        {
                            continue;
                        }
                        romajiOutput += _romajiToHiraganaDictionary[toMatch];
                        toMatch = "";
                    }
                    // special case 'p' or 'b' after 'm' (e.g. "sempai" -> "せんぱい"; "gambare" -> "がんばれ")
                    else if (CheckSpecialCasePBAfterM(hiraganaInput, toMatch, i))
                    {
                        romajiOutput += "ん";
                        toMatch = "";
                        continue;
                    }
                    // "nn" -> "ん"
                    else if (toMatch.Equals("nn"))
                    {
                        if (i + 1 < hiraganaInput.Length && _vowels.Contains(Char.ToLower(hiraganaInput[i + 1]).ToString())) // "nna" -> "な"
                        {
                            romajiOutput += GetNNVowelAsHiragana(Char.ToLower(hiraganaInput[i + 1]));
                            i++;
                        }
                        else
                        {
                            romajiOutput += "ん";
                        }
                        toMatch = "";
                    }

                    // small tsu/sokuon
                    // possible sokuon match: e.g. "kkkki" should be converted to "kkっき"
                    else if (CheckSokuonLookThreeBehind(toMatch))
                    {
                        var replacement = toMatch.Substring(0, toMatch.Length - 3) + "っ" + _romajiToHiraganaDictionary[toMatch.Substring(toMatch.Length - 2)];
                        romajiOutput += replacement;
                        toMatch = "";
                    }
                    else if (CheckSokuonLookFourBehind(toMatch))
                    {
                        var replacement = toMatch.Substring(0, toMatch.Length - 4) + "っ" + _romajiToHiraganaDictionary[toMatch.Substring(toMatch.Length - 3)];
                        romajiOutput += replacement;
                        toMatch = "";
                    }


                    if (toMatch.Equals(String.Empty))
                        break;

                    if (!CheckIfRomajiPhraseStillPossbile(toMatch))
                    {
                        romajiOutput += toMatch[0];
                        toMatch = toMatch.Substring(1);
                    }
                }
            }
            romajiOutput += toMatch; // append unmatched string to output
            return romajiOutput;
        }

        private static bool CheckIfRomajiPhraseStillPossbile(string toMatch)
        {
            foreach (var s in _possibleConvertableRomajiPhrases)
            {
                if (s.Contains(toMatch) || (s[0] + s).Contains(toMatch)) // "s[0] + s" for small tsu (in case that s[0] == s) possibility
                    return true;
            }
            return false;
        }

        // for strings like "ni","na" etc.
        private static bool CheckSokuonLookThreeBehind(string toMatch)
        {
            return toMatch.Length > 2 && toMatch[toMatch.Length - 2].Equals(toMatch[toMatch.Length - 3]) 
                && !_noConversionToSokuon.Contains(toMatch[toMatch.Length - 2].ToString()) // "aa" -> "ああ" (not "っあ")
                && _romajiToHiraganaDictionary.ContainsKey(toMatch.Substring(toMatch.Length - 2)); // for cases that e.g. "tt", which by itself cannot be converted" will be looked up in the dictionary
        }

        // for strings like "kyu","kya" etc.
        private static bool CheckSokuonLookFourBehind(string toMatch)
        {
            return toMatch.Length > 3 && toMatch[toMatch.Length - 3].Equals(toMatch[toMatch.Length - 4])
                && !_noConversionToSokuon.Contains(toMatch[toMatch.Length - 3].ToString())
                && _romajiToHiraganaDictionary.ContainsKey(toMatch.Substring(toMatch.Length - 3));
        }

        private static bool CheckSpecialCasePBAfterM(string hiraganaInput, string toMatch, int i)
        {
            return toMatch.Equals("m") && i + 1 < hiraganaInput.Length
                 && (Char.ToLower(hiraganaInput[i + 1]).Equals('b') || Char.ToLower(hiraganaInput[i + 1]).Equals('p'));
        }

        private static bool CheckSpecialCaseN(string hiraganaInput, string toMatch, int i)
        {
            return toMatch.Equals("n") &&
                (i + 1 < hiraganaInput.Length && Char.ToLower(hiraganaInput[i + 1]).Equals('n') // "nn" -> "ん"
                || i + 1 < hiraganaInput.Length && _vowels.Contains(Char.ToLower(hiraganaInput[i + 1]).ToString()) // "na" -> "な"
                || i + 2 < hiraganaInput.Length && Char.ToLower(hiraganaInput[i + 1]).Equals('y') && _vowels.Contains(Char.ToLower(hiraganaInput[i + 2]).ToString())); // "nya" -> "にゃ"
        }

        private static bool IsInvalidCharacterForConversion(char c)
        {
            return ('A' > c || c > 'z') && c != '？' && c != '-';
        }

        private static string GetNNVowelAsHiragana(char vowelAfterNN)
        {
            switch (Char.ToLower(vowelAfterNN))
            {
                case 'a':
                    return "んな";
                case 'o':
                    return "んの";
                case 'i':
                    return "んに";
                case 'e':
                    return "んね";
                case 'u':
                    return "んぬ";
                default:
                    throw new ArgumentException("input " + vowelAfterNN + " is not a vowel");
            }
        }
    }
}
