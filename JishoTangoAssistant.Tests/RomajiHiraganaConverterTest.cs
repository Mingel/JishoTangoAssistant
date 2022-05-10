using NUnit.Framework;

namespace JishoTangoAssistant.Tests
{
    [TestFixture]
    public class RomajiHiraganaConverterTest // using Jisho's Conversion (not e.g. Microsoft's IME conversion)
    {
        [Test]
        public void TestRealExamples()
        {
            Assert.AreEqual("たべる", RomajiHiraganaConverter.Convert("taberu"));
            Assert.AreEqual("のむ", RomajiHiraganaConverter.Convert("nomu"));
            Assert.AreEqual("ある", RomajiHiraganaConverter.Convert("aru"));
            Assert.AreEqual("おめでとうございます", RomajiHiraganaConverter.Convert("omedetougozaimasu"));
            Assert.AreEqual("たんじょうび", RomajiHiraganaConverter.Convert("tanjoubi"));
            Assert.AreEqual("あって", RomajiHiraganaConverter.Convert("atte"));
        }

        // for more infos, see: https://en.wikipedia.org/wiki/Nihon-shiki_romanization
        [Test]
        public void TestNihonShikiSpecificStyle()
        {
            Assert.AreEqual("しちつふじぢづ", RomajiHiraganaConverter.Convert("sitituhuzididu"));
            Assert.AreEqual("しゃしゅしょちゃちゅちょじゃじゅじょぢゃぢゅぢょ", RomajiHiraganaConverter.Convert("syasyusyotyatyutyozyazyuzyodyadyudyo"));
        }

        // for more infos, see: https://en.wikipedia.org/wiki/Hepburn_romanization
        [Test]
        public void TestHepburnSpecificStyle()
        {
            // not Hepburn: "ぢ" -> di, "づ" -> du, "ぢゃ" -> dya, "ぢゅ" -> dyu, "ぢょ" -> dyo
            Assert.AreEqual("しちつふじ", RomajiHiraganaConverter.Convert("shichitsufuji"));
            Assert.AreEqual("しゃしゅしょちゃちゅちょじゃじゅじょ", RomajiHiraganaConverter.Convert("shashushochachuchojajujo"));
        }

        [Test]
        public void TestMToHiraganaN()
        {
            Assert.AreEqual("せんぱい", RomajiHiraganaConverter.Convert("senpai"));
            Assert.AreEqual("がんばれ", RomajiHiraganaConverter.Convert("gambare"));

            Assert.AreEqual("んば", RomajiHiraganaConverter.Convert("mba")); // Microsoft IME: mば
            Assert.AreEqual("んぱ", RomajiHiraganaConverter.Convert("mpa")); // Microsoft IME: mぱ

            Assert.AreEqual("bam", RomajiHiraganaConverter.Convert("bam")); // Microsoft IME: ばm
            Assert.AreEqual("pam", RomajiHiraganaConverter.Convert("pam")); // Microsoft IME: ぱm

            Assert.AreEqual("kmba", RomajiHiraganaConverter.Convert("kmba")); // Microsoft IME: kmば
            Assert.AreEqual("kmpa", RomajiHiraganaConverter.Convert("kmpa")); // Microsoft IME: kmぱ
        }

        [Test]
        public void TestSokuonLookThreeBehind()
        {
            Assert.AreEqual("mm", RomajiHiraganaConverter.Convert("mm"));
            Assert.AreEqual("っま", RomajiHiraganaConverter.Convert("mma"));
        }

        [Test]
        public void TestSokuonLookFourBehind()
        {
            Assert.AreEqual("kky", RomajiHiraganaConverter.Convert("kky"));
            Assert.AreEqual("っきゃ", RomajiHiraganaConverter.Convert("kkya"));
        }

        [Test]
        public void TestDoubleVowels() // no sokuons
        {
            Assert.AreEqual("ああ", RomajiHiraganaConverter.Convert("aa"));
            Assert.AreEqual("おお", RomajiHiraganaConverter.Convert("oo"));
            Assert.AreEqual("いい", RomajiHiraganaConverter.Convert("ii"));
            Assert.AreEqual("ええ", RomajiHiraganaConverter.Convert("ee"));
            Assert.AreEqual("うう", RomajiHiraganaConverter.Convert("uu"));
            Assert.AreEqual("ん", RomajiHiraganaConverter.Convert("nn"));
        }

        [Test]
        public void TestSokuonDoubleConsonantsNotValid()
        {
            Assert.AreEqual("kkiitt", RomajiHiraganaConverter.Convert("kkiitt")); // Microsoft IME: っきいtt
            Assert.AreEqual("っきいってえ", RomajiHiraganaConverter.Convert("kkiittee"));
            Assert.AreEqual("kkttmm", RomajiHiraganaConverter.Convert("kkttmm"));
            Assert.AreEqual("kkttee", RomajiHiraganaConverter.Convert("kkttee")); // Microsoft IME: kkってえ
            Assert.AreEqual("kknnttnnmm", RomajiHiraganaConverter.Convert("kknnttnnmm")); // Microsoft IME: kkんttんmm
            Assert.AreEqual("kkkkkkkk", RomajiHiraganaConverter.Convert("kkkkkkkk"));
        }

        [Test]
        public void TestSokuonMoreThanTwoConsonants()
        {
            Assert.AreEqual("っっき", RomajiHiraganaConverter.Convert("kkki")); // Microsoft IME: kっき
            Assert.AreEqual("っっっき", RomajiHiraganaConverter.Convert("kkkki")); // Microsoft IME: kkっき

            Assert.AreEqual("っっきゃ", RomajiHiraganaConverter.Convert("kkkya")); // Microsoft IME: kっきゃ
            Assert.AreEqual("っっっきゃ", RomajiHiraganaConverter.Convert("kkkkya")); // Microsoft IME: kkっきゃ
        }

        [Test]
        public void TestHiraganaN()
        {
            Assert.AreEqual("ん", RomajiHiraganaConverter.Convert("n"));
            Assert.AreEqual("ん", RomajiHiraganaConverter.Convert("nn"));
            Assert.AreEqual("んん", RomajiHiraganaConverter.Convert("nnn"));
            Assert.AreEqual("んん", RomajiHiraganaConverter.Convert("nnnn"));

            Assert.AreEqual("んな", RomajiHiraganaConverter.Convert("nna"));
            Assert.AreEqual("んの", RomajiHiraganaConverter.Convert("nno"));
            Assert.AreEqual("んに", RomajiHiraganaConverter.Convert("nni"));
            Assert.AreEqual("んね", RomajiHiraganaConverter.Convert("nne"));
            Assert.AreEqual("んぬ", RomajiHiraganaConverter.Convert("nnu"));

            Assert.AreEqual("な", RomajiHiraganaConverter.Convert("na"));
            Assert.AreEqual("んな", RomajiHiraganaConverter.Convert("nna"));
            Assert.AreEqual("んなん", RomajiHiraganaConverter.Convert("nnan"));
            Assert.AreEqual("んなん", RomajiHiraganaConverter.Convert("nnann"));
        }

        [Test]
        public void TestSpecialValidCharacters()
        {
            Assert.AreEqual("ー", RomajiHiraganaConverter.Convert("-"));
            Assert.AreEqual("ーー", RomajiHiraganaConverter.Convert("--"));

            Assert.AreEqual("？", RomajiHiraganaConverter.Convert("?"));
            Assert.AreEqual("？？", RomajiHiraganaConverter.Convert("??"));
        }
    }
}