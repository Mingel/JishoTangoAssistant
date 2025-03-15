using JishoTangoAssistant.Common.Utils;
using NUnit.Framework;

namespace JishoTangoAssistant.Tests
{
    [TestFixture]
    public class RomajiKanaConverterTest // using Jisho's Conversion (not e.g. Microsoft's IME conversion)
    {
        [Test]
        public void TestRealExamples()
        {
            Assert.AreEqual("たべる", RomajiKanaConverter.Convert("taberu"));
            Assert.AreEqual("のむ", RomajiKanaConverter.Convert("nomu"));
            Assert.AreEqual("ある", RomajiKanaConverter.Convert("aru"));
            Assert.AreEqual("おめでとうございます", RomajiKanaConverter.Convert("omedetougozaimasu"));
            Assert.AreEqual("たんじょうび", RomajiKanaConverter.Convert("tanjoubi"));
            Assert.AreEqual("あって", RomajiKanaConverter.Convert("atte"));
        }

        // for more infos, see: https://en.wikipedia.org/wiki/Nihon-shiki_romanization
        [Test]
        public void TestNihonShikiSpecificStyle()
        {
            Assert.AreEqual("しちつふじぢづ", RomajiKanaConverter.Convert("sitituhuzididu"));
            Assert.AreEqual("しゃしゅしょちゃちゅちょじゃじゅじょぢゃぢゅぢょ", RomajiKanaConverter.Convert("syasyusyotyatyutyozyazyuzyodyadyudyo"));
        }

        // for more infos, see: https://en.wikipedia.org/wiki/Hepburn_romanization
        [Test]
        public void TestHepburnSpecificStyle()
        {
            // not Hepburn: "ぢ" -> di, "づ" -> du, "ぢゃ" -> dya, "ぢゅ" -> dyu, "ぢょ" -> dyo
            Assert.AreEqual("しちつふじ", RomajiKanaConverter.Convert("shichitsufuji"));
            Assert.AreEqual("しゃしゅしょちゃちゅちょじゃじゅじょ", RomajiKanaConverter.Convert("shashushochachuchojajujo"));
        }

        [Test]
        public void TestMToHiraganaN()
        {
            Assert.AreEqual("せんぱい", RomajiKanaConverter.Convert("senpai"));
            Assert.AreEqual("がんばれ", RomajiKanaConverter.Convert("gambare"));

            Assert.AreEqual("んば", RomajiKanaConverter.Convert("mba")); // Microsoft IME: mば
            Assert.AreEqual("んぱ", RomajiKanaConverter.Convert("mpa")); // Microsoft IME: mぱ

            Assert.AreEqual("bam", RomajiKanaConverter.Convert("bam")); // Microsoft IME: ばm
            Assert.AreEqual("pam", RomajiKanaConverter.Convert("pam")); // Microsoft IME: ぱm

            Assert.AreEqual("kmba", RomajiKanaConverter.Convert("kmba")); // Microsoft IME: kmば
            Assert.AreEqual("kmpa", RomajiKanaConverter.Convert("kmpa")); // Microsoft IME: kmぱ
        }

        [Test]
        public void TestSokuonLookThreeBehind()
        {
            Assert.AreEqual("mm", RomajiKanaConverter.Convert("mm"));
            Assert.AreEqual("っま", RomajiKanaConverter.Convert("mma"));
        }

        [Test]
        public void TestSokuonLookFourBehind()
        {
            Assert.AreEqual("kky", RomajiKanaConverter.Convert("kky"));
            Assert.AreEqual("っきゃ", RomajiKanaConverter.Convert("kkya"));
        }

        [Test]
        public void TestDoubleVowels() // no sokuons
        {
            Assert.AreEqual("ああ", RomajiKanaConverter.Convert("aa"));
            Assert.AreEqual("おお", RomajiKanaConverter.Convert("oo"));
            Assert.AreEqual("いい", RomajiKanaConverter.Convert("ii"));
            Assert.AreEqual("ええ", RomajiKanaConverter.Convert("ee"));
            Assert.AreEqual("うう", RomajiKanaConverter.Convert("uu"));
            Assert.AreEqual("ん", RomajiKanaConverter.Convert("nn"));
        }

        [Test]
        public void TestSokuonDoubleConsonantsNotValid()
        {
            Assert.AreEqual("kkiitt", RomajiKanaConverter.Convert("kkiitt")); // Microsoft IME: っきいtt
            Assert.AreEqual("っきいってえ", RomajiKanaConverter.Convert("kkiittee"));
            Assert.AreEqual("kkttmm", RomajiKanaConverter.Convert("kkttmm"));
            Assert.AreEqual("kkttee", RomajiKanaConverter.Convert("kkttee")); // Microsoft IME: kkってえ
            Assert.AreEqual("kknnttnnmm", RomajiKanaConverter.Convert("kknnttnnmm")); // Microsoft IME: kkんttんmm
            Assert.AreEqual("kkkkkkkk", RomajiKanaConverter.Convert("kkkkkkkk"));
        }

        [Test]
        public void TestSokuonMoreThanTwoConsonants()
        {
            Assert.AreEqual("っっき", RomajiKanaConverter.Convert("kkki")); // Microsoft IME: kっき
            Assert.AreEqual("っっっき", RomajiKanaConverter.Convert("kkkki")); // Microsoft IME: kkっき

            Assert.AreEqual("っっきゃ", RomajiKanaConverter.Convert("kkkya")); // Microsoft IME: kっきゃ
            Assert.AreEqual("っっっきゃ", RomajiKanaConverter.Convert("kkkkya")); // Microsoft IME: kkっきゃ
        }

        [Test]
        public void TestHiraganaN()
        {
            Assert.AreEqual("ん", RomajiKanaConverter.Convert("n"));
            Assert.AreEqual("ん", RomajiKanaConverter.Convert("nn"));
            Assert.AreEqual("んん", RomajiKanaConverter.Convert("nnn"));
            Assert.AreEqual("んん", RomajiKanaConverter.Convert("nnnn"));

            Assert.AreEqual("んな", RomajiKanaConverter.Convert("nna"));
            Assert.AreEqual("んの", RomajiKanaConverter.Convert("nno"));
            Assert.AreEqual("んに", RomajiKanaConverter.Convert("nni"));
            Assert.AreEqual("んね", RomajiKanaConverter.Convert("nne"));
            Assert.AreEqual("んぬ", RomajiKanaConverter.Convert("nnu"));

            Assert.AreEqual("な", RomajiKanaConverter.Convert("na"));
            Assert.AreEqual("んな", RomajiKanaConverter.Convert("nna"));
            Assert.AreEqual("んなん", RomajiKanaConverter.Convert("nnan"));
            Assert.AreEqual("んなん", RomajiKanaConverter.Convert("nnann"));
        }

        [Test]
        public void TestSpecialValidCharacters()
        {
            Assert.AreEqual("ー", RomajiKanaConverter.Convert("-"));
            Assert.AreEqual("ーー", RomajiKanaConverter.Convert("--"));

            Assert.AreEqual("？", RomajiKanaConverter.Convert("?"));
            Assert.AreEqual("？？", RomajiKanaConverter.Convert("??"));
        }

        [Test]
        public void TestKatakana()
        {
            Assert.AreEqual("パーティ", RomajiKanaConverter.Convert("PA-TEXI"));
            Assert.AreEqual("ヂスコ", RomajiKanaConverter.Convert("DISUKO"));

            Assert.AreEqual("スペイン", RomajiKanaConverter.Convert("SUPEIN"));
            Assert.AreEqual("ドイツ", RomajiKanaConverter.Convert("DOITSU"));

            Assert.AreEqual("パーティ", RomajiKanaConverter.Convert("Pa-TeXi"));
            Assert.AreEqual("ヂスコ", RomajiKanaConverter.Convert("DiSuKo"));

            Assert.AreEqual("スペイン", RomajiKanaConverter.Convert("SuPeIN"));
            Assert.AreEqual("ドイツ", RomajiKanaConverter.Convert("DoITsu"));
        }

        [Test]
        public void TestMixedKana()
        {
            Assert.AreEqual("アか", RomajiKanaConverter.Convert("AkA"));
            Assert.AreEqual("ヂすコ", RomajiKanaConverter.Convert("DIsuKo"));

            Assert.AreEqual("すペイん", RomajiKanaConverter.Convert("sUPEIn"));
            Assert.AreEqual("ドイつ", RomajiKanaConverter.Convert("DoItSU"));
        }

        [Test]
        public void TestValidMixOfKanaAndRomaji()
        {
            Assert.AreEqual("はなび", RomajiKanaConverter.Convert("haなび"));
            Assert.AreEqual("はなび", RomajiKanaConverter.Convert("hanaび"));
            Assert.AreEqual("はなび", RomajiKanaConverter.Convert("はnaび"));;
            Assert.AreEqual("はなび", RomajiKanaConverter.Convert("はnabi"));
        }
    }
}