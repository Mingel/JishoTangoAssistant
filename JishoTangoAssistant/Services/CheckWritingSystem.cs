using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JishoTangoAssistant.Services
{
    public static class CheckWritingSystem
    {
        private const char UNICODE_ROMAJI_START = '\u0020';
        private const char UNICODE_ROMAJI_END = '\u007E';
        private const char UNICODE_HIRAGANA_START = '\u3040';
        private const char UNICODE_HIRAGANA_END = '\u309F';
        private const char UNICODE_KATAKANA_START = '\u30A0';
        private const char UNICODE_KATAKANA_END = '\u30FF';
        private const char UNICODE_KANJI_START = '\u4E00';
        private const char UNICODE_KANJI_END = '\u9FBF';

        public static bool OnlyContainsRomaji(string text)
        {
            return text.All(c => UNICODE_ROMAJI_START <= c && c <= UNICODE_ROMAJI_END);
        }

        public static bool OnlyContainsHiragana(string text)
        {
            return text.All(c => UNICODE_HIRAGANA_START <= c && c <= UNICODE_HIRAGANA_END);
        }

        public static bool OnlyContainsKatakana(string text)
        {
            return text.All(c => UNICODE_KATAKANA_START <= c && c <= UNICODE_KATAKANA_END);
        }

        public static bool OnlyContainsKanji(string text)
        {
            return text.All(c => UNICODE_KANJI_START <= c && c <= UNICODE_KANJI_END);
        }

        public static bool ContainsKanji(string text)
        {
            return text.Any(c => UNICODE_KANJI_START <= c && c <= UNICODE_KANJI_END);
        }
    }
}
