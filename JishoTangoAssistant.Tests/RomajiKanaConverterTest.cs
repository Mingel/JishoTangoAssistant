using JishoTangoAssistant.Application.Core.Utils;
using NUnit.Framework;

namespace JishoTangoAssistant.Tests;

[TestFixture]
public class RomajiKanaConverterTest // using Jisho's Conversion (not e.g. Microsoft's IME conversion)
{
    [Test]
    public void TestRealExamples()
    {
        Assert.That(RomajiKanaConverter.Convert("taberu"), Is.EqualTo("たべる"));
        Assert.That(RomajiKanaConverter.Convert("nomu"), Is.EqualTo("のむ"));
        Assert.That(RomajiKanaConverter.Convert("aru"), Is.EqualTo("ある"));
        Assert.That(RomajiKanaConverter.Convert("omedetougozaimasu"), Is.EqualTo("おめでとうございます"));
        Assert.That(RomajiKanaConverter.Convert("tanjoubi"), Is.EqualTo("たんじょうび"));
        Assert.That(RomajiKanaConverter.Convert("atte"), Is.EqualTo("あって"));
    }

    // for more infos, see: https://en.wikipedia.org/wiki/Nihon-shiki_romanization
    [Test]
    public void TestNihonShikiSpecificStyle()
    {
        Assert.That(RomajiKanaConverter.Convert("sitituhuzididu"), Is.EqualTo("しちつふじぢづ"));
        Assert.That(RomajiKanaConverter.Convert("syasyusyotyatyutyozyazyuzyodyadyudyo"), Is.EqualTo("しゃしゅしょちゃちゅちょじゃじゅじょぢゃぢゅぢょ"));
    }

    // for more infos, see: https://en.wikipedia.org/wiki/Hepburn_romanization
    [Test]
    public void TestHepburnSpecificStyle()
    {
        Assert.That(RomajiKanaConverter.Convert("shichitsufuji"), Is.EqualTo("しちつふじ"));
        Assert.That(RomajiKanaConverter.Convert("shashushochachuchojajujo"), Is.EqualTo("しゃしゅしょちゃちゅちょじゃじゅじょ"));
    }

    [Test]
    public void TestMToHiraganaN()
    {
        Assert.That(RomajiKanaConverter.Convert("senpai"), Is.EqualTo("せんぱい"));
        Assert.That(RomajiKanaConverter.Convert("gambare"), Is.EqualTo("がんばれ"));

        Assert.That(RomajiKanaConverter.Convert("mba"), Is.EqualTo("んば"));
        Assert.That(RomajiKanaConverter.Convert("mpa"), Is.EqualTo("んぱ"));

        Assert.That(RomajiKanaConverter.Convert("bam"), Is.EqualTo("bam"));
        Assert.That(RomajiKanaConverter.Convert("pam"), Is.EqualTo("pam"));

        Assert.That(RomajiKanaConverter.Convert("kmba"), Is.EqualTo("kmba"));
        Assert.That(RomajiKanaConverter.Convert("kmpa"), Is.EqualTo("kmpa"));
    }

    [Test]
    public void TestSokuonLookThreeBehind()
    {
        Assert.That(RomajiKanaConverter.Convert("mm"), Is.EqualTo("mm"));
        Assert.That(RomajiKanaConverter.Convert("mma"), Is.EqualTo("っま"));
    }

    [Test]
    public void TestSokuonLookFourBehind()
    {
        Assert.That(RomajiKanaConverter.Convert("kky"), Is.EqualTo("kky"));
        Assert.That(RomajiKanaConverter.Convert("kkya"), Is.EqualTo("っきゃ"));
    }

    [Test]
    public void TestDoubleVowels() // no sokuons
    {
        Assert.That(RomajiKanaConverter.Convert("aa"), Is.EqualTo("ああ"));
        Assert.That(RomajiKanaConverter.Convert("oo"), Is.EqualTo("おお"));
        Assert.That(RomajiKanaConverter.Convert("ii"), Is.EqualTo("いい"));
        Assert.That(RomajiKanaConverter.Convert("ee"), Is.EqualTo("ええ"));
        Assert.That(RomajiKanaConverter.Convert("uu"), Is.EqualTo("うう"));
        Assert.That(RomajiKanaConverter.Convert("nn"), Is.EqualTo("ん"));
    }

    [Test]
    public void TestSokuonDoubleConsonantsNotValid()
    {
        Assert.That(RomajiKanaConverter.Convert("kkiitt"), Is.EqualTo("kkiitt"));
        Assert.That(RomajiKanaConverter.Convert("kkiittee"), Is.EqualTo("っきいってえ"));
        Assert.That(RomajiKanaConverter.Convert("kkttmm"), Is.EqualTo("kkttmm"));
        Assert.That(RomajiKanaConverter.Convert("kkttee"), Is.EqualTo("kkttee"));
        Assert.That(RomajiKanaConverter.Convert("kknnttnnmm"), Is.EqualTo("kknnttnnmm"));
        Assert.That(RomajiKanaConverter.Convert("kkkkkkkk"), Is.EqualTo("kkkkkkkk"));
    }

    [Test]
    public void TestSokuonMoreThanTwoConsonants()
    {
        Assert.That(RomajiKanaConverter.Convert("kkki"), Is.EqualTo("っっき"));
        Assert.That(RomajiKanaConverter.Convert("kkkki"), Is.EqualTo("っっっき"));

        Assert.That(RomajiKanaConverter.Convert("kkkya"), Is.EqualTo("っっきゃ"));
        Assert.That(RomajiKanaConverter.Convert("kkkkya"), Is.EqualTo("っっっきゃ"));
    }

    [Test]
    public void TestHiraganaN()
    {
        Assert.That(RomajiKanaConverter.Convert("n"), Is.EqualTo("ん"));
        Assert.That(RomajiKanaConverter.Convert("nn"), Is.EqualTo("ん"));
        Assert.That(RomajiKanaConverter.Convert("nnn"), Is.EqualTo("んん"));
        Assert.That(RomajiKanaConverter.Convert("nnnn"), Is.EqualTo("んん"));

        Assert.That(RomajiKanaConverter.Convert("nna"), Is.EqualTo("んな"));
        Assert.That(RomajiKanaConverter.Convert("nno"), Is.EqualTo("んの"));
        Assert.That(RomajiKanaConverter.Convert("nni"), Is.EqualTo("んに"));
        Assert.That(RomajiKanaConverter.Convert("nne"), Is.EqualTo("んね"));
        Assert.That(RomajiKanaConverter.Convert("nnu"), Is.EqualTo("んぬ"));

        Assert.That(RomajiKanaConverter.Convert("na"), Is.EqualTo("な"));
        Assert.That(RomajiKanaConverter.Convert("nna"), Is.EqualTo("んな"));
        Assert.That(RomajiKanaConverter.Convert("nnan"), Is.EqualTo("んなん"));
        Assert.That(RomajiKanaConverter.Convert("nnann"), Is.EqualTo("んなん"));
    }

    [Test]
    public void TestSpecialValidCharacters()
    {
        Assert.That(RomajiKanaConverter.Convert("-"), Is.EqualTo("ー"));
        Assert.That(RomajiKanaConverter.Convert("--"), Is.EqualTo("ーー"));

        Assert.That(RomajiKanaConverter.Convert("?"), Is.EqualTo("？"));
        Assert.That(RomajiKanaConverter.Convert("??"), Is.EqualTo("？？"));
    }

    [Test]
    public void TestKatakana()
    {
        Assert.That(RomajiKanaConverter.Convert("PA-TEXI"), Is.EqualTo("パーティ"));
        Assert.That(RomajiKanaConverter.Convert("DISUKO"), Is.EqualTo("ヂスコ"));

        Assert.That(RomajiKanaConverter.Convert("SUPEIN"), Is.EqualTo("スペイン"));
        Assert.That(RomajiKanaConverter.Convert("DOITSU"), Is.EqualTo("ドイツ"));

        Assert.That(RomajiKanaConverter.Convert("Pa-TeXi"), Is.EqualTo("パーティ"));
        Assert.That(RomajiKanaConverter.Convert("DiSuKo"), Is.EqualTo("ヂスコ"));

        Assert.That(RomajiKanaConverter.Convert("SuPeIN"), Is.EqualTo("スペイン"));
        Assert.That(RomajiKanaConverter.Convert("DoITsu"), Is.EqualTo("ドイツ"));
    }

    [Test]
    public void TestMixedKana()
    {
        Assert.That(RomajiKanaConverter.Convert("AkA"), Is.EqualTo("アか"));
        Assert.That(RomajiKanaConverter.Convert("DIsuKo"), Is.EqualTo("ヂすコ"));

        Assert.That(RomajiKanaConverter.Convert("sUPEIn"), Is.EqualTo("すペイん"));
        Assert.That(RomajiKanaConverter.Convert("DoItSU"), Is.EqualTo("ドイつ"));
    }

    [Test]
    public void TestValidMixOfKanaAndRomaji()
    {
        Assert.That(RomajiKanaConverter.Convert("haなび"), Is.EqualTo("はなび"));
        Assert.That(RomajiKanaConverter.Convert("hanaび"), Is.EqualTo("はなび"));
        Assert.That(RomajiKanaConverter.Convert("はnaび"), Is.EqualTo("はなび"));
        Assert.That(RomajiKanaConverter.Convert("はnabi"), Is.EqualTo("はなび"));
    }
}