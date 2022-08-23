using System;
using System.Collections.Generic;
using System.Linq;

namespace JishoTangoAssistant.Services
{
    public static class RomajiKanaConverter
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

        private static readonly List<string> _noConversionToSokuon = new List<string>() { "a", "e", "i", "o", "u", "n", "va", "vyi", "vu", "vye", "vo", "vya", "vyu", "vyo", "-", "?" };
        private static readonly List<string> _vowels = new List<string>() { "a", "e", "i", "o", "u" };

        // Converts based on the search query's conversion to hiragana/katakana in jisho.org
        public static string Convert(string romajiInput)
        {
            var romajiOutput = "";
            var toMatch = "";
            bool forceNextKanaLetterFromNaRow = false;
            bool forceNextKatakanaLetter = false;
            for (int i = 0; i < romajiInput.Length; i++)
            {
                char c = romajiInput[i];

                if (!IsRomajiCharacter(c))
                    return romajiInput;
                
                toMatch += c;

                if (_vowels.Contains(toMatch.ToLower()) && forceNextKanaLetterFromNaRow)
                {
                    // things to consider for this case:
                    // - _vowels is a subset of _romajiToKanaDictionary.Keys
                    // -> toMatch must be a single vowel
                    // - forceKanaLetterFromNaRow can only be true if:
                    //   - the input processed until now ended with the substring "nn"
                    //   -> therefore, e.g. (already processed) "nn" + "a" (to be processed) should output "んな", not "んあ"
                    var firstNKana = forceNextKatakanaLetter ? "N" : "n";
                    var secondNKana = toMatch;
                    romajiOutput += ToKanaLetter(String.Concat(firstNKana, secondNKana));
                    toMatch = String.Empty;

                    forceNextKanaLetterFromNaRow = forceNextKatakanaLetter = false;
                }
                else if (_romajiToHiraganaDictionary.ContainsKey(toMatch.ToLower()) && !VowelOrLetterNAfterLetterN(romajiInput, toMatch.ToLower(), i)) // consider look ahead: special case 'n'
                {
                    romajiOutput += ToKanaLetter(toMatch);
                    toMatch = String.Empty;
                }
                // "nn" -> "ん"
                else if (toMatch.ToLower().Equals("nn"))
                {
                    romajiOutput += Char.IsLower(c) ? "ん" : ToKatakana("ん");
                    toMatch = String.Empty;

                    forceNextKanaLetterFromNaRow = VowelAfterNN(romajiInput, i + 1);
                    forceNextKatakanaLetter = Char.IsUpper(c);
                }
                // special case: 'p' or 'b' after 'm' (e.g. "sempai" -> "せんぱい"; "gambare" -> "がんばれ")
                else if (LetterPOrLetterBAfterLetterM(romajiInput, toMatch, i))
                {
                    romajiOutput += Char.IsLower(c) ? "ん" : ToKatakana("ん");
                    toMatch = String.Empty;
                }
                // small tsu/sokuon
                // sokuon match: e.g. "kki" should be converted to "っき"
                else if (DoubleConsonantsAt(toMatch.ToLower(), toMatch.Length - 2) && OnlySameConsonantsBeforeIndex(toMatch.ToLower(), toMatch.Length - 1))
                {
                    var smallTsus = DetermineSokuons(toMatch.Substring(0, toMatch.Length - 2));
                    var kanaOfMatchedLastLetters = ToKanaLetter(toMatch.Substring(toMatch.Length - 2));

                    romajiOutput += String.Concat(smallTsus, kanaOfMatchedLastLetters);
                    toMatch = String.Empty;
                }
                // sokuon match: e.g. "kkya" should be converted to "っきゃ"
                else if (DoubleConsonantsAt(toMatch.ToLower(), toMatch.Length - 3) && OnlySameConsonantsBeforeIndex(toMatch.ToLower(), toMatch.Length - 2))
                {
                    var smallTsus = DetermineSokuons(toMatch.Substring(0, toMatch.Length - 3));
                    var kanaOfMatchedLastRomajiLetters = ToKanaLetter(toMatch.Substring(toMatch.Length - 3));

                    romajiOutput += String.Concat(smallTsus, kanaOfMatchedLastRomajiLetters);
                    toMatch = String.Empty;
                }
            }

            // toMatch must contain non-hiragana letters
            if (!String.IsNullOrEmpty(toMatch))
                return romajiInput;

            return romajiOutput;
        }

        private static string DetermineSokuons(string romajiLettersToBeConvertedToSokuons)
        {
            char[] smallTsuArray = romajiLettersToBeConvertedToSokuons.Select(c => Char.IsLower(c) ? 'っ' : ToKatakana('っ')).ToArray();
            return new string(smallTsuArray);
        }

        private static string ToKanaLetter(string romajiSyllable)
        {
            var kanaLetter = _romajiToHiraganaDictionary[romajiSyllable.ToLower()];

            if (romajiSyllable.Equals('?') || romajiSyllable.Equals('-'))
                return kanaLetter;

            // Rule: If first letter of romaji syllable is uppercase, then the kana letter is in katakana, otherwise hiragana
            if (!String.IsNullOrEmpty(romajiSyllable) && romajiSyllable.Length > 0 && Char.IsUpper(romajiSyllable.First()))
                kanaLetter = ToKatakana(kanaLetter).ToString();
            return kanaLetter;
        }

        private static char ToKatakana(char hiraganaLetter)
        {
            return (char)(hiraganaLetter + 96);
        }

        private static char ToKatakana(string hiraganaLetter)
        {
            return ToKatakana(char.Parse(hiraganaLetter));
        }

        private static bool VowelAfterNN(string romajiInput, int vowelIndex)
        {
            return vowelIndex < romajiInput.Length && _vowels.Contains(Char.ToLower(romajiInput[vowelIndex]).ToString());
        }

        private static bool OnlySameConsonantsBeforeIndex(string toMatch, int endIndex)
        {
            return toMatch.Length == 0 || toMatch.Substring(0, endIndex).Distinct().Count() == 1;
        }

        // for strings like "ni", "na" etc. if index = toMatch.Length - 2
        // for strings like "kyu", "kya" etc. if index = toMatch.Length - 3
        private static bool DoubleConsonantsAt(string toMatch, int indexOfSecondLetter)
        {
            return indexOfSecondLetter > 0
                && toMatch[indexOfSecondLetter].Equals(toMatch[indexOfSecondLetter - 1])  
                && !_noConversionToSokuon.Contains(toMatch[indexOfSecondLetter].ToString()) // for index = toMatch.Length - 2: "aa" -> "ああ" (not "っあ")
                && _romajiToHiraganaDictionary.ContainsKey(toMatch.Substring(indexOfSecondLetter)); // for cases that e.g. "tt" and index = toMatch.Length - 2, which by itself cannot be converted" will be looked up in the dictionary
        }

        private static bool LetterPOrLetterBAfterLetterM(string romajiInput, string toMatch, int i)
        {
            return toMatch.Equals("m") && i + 1 < romajiInput.Length
                 && (Char.ToLower(romajiInput[i + 1]) == 'b' || Char.ToLower(romajiInput[i + 1]) == 'p');
        }

        private static bool VowelOrLetterNAfterLetterN(string romajiInput, string toMatch, int i)
        {
            return toMatch.Equals("n") &&
                (i + 1 < romajiInput.Length && Char.ToLower(romajiInput[i + 1]).Equals('n') // "nn" -> "ん"
                || i + 1 < romajiInput.Length && _vowels.Contains(Char.ToLower(romajiInput[i + 1]).ToString()) // "na" -> "な"
                || i + 2 < romajiInput.Length && Char.ToLower(romajiInput[i + 1]).Equals('y') && _vowels.Contains(Char.ToLower(romajiInput[i + 2]).ToString())); // "nya" -> "にゃ"
        }

        // Includes '?' and '-'
        private static bool IsRomajiCharacter(char c)
        {
            return ('A' <= c && c <= 'z') || c == '?' || c == '-';
        }
    }
}
