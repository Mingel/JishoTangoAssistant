using NUnit.Framework;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.Tests
{
    [TestFixture]
    public class ObservableVocabularyListTest
    {
        private ObservableVocabularyList list = [];

        [SetUp]
        public void Init()
        {
            list = [];
        }

        [TearDown]
        public void Cleanup()
        {
            list = [];
        }

        [Test]
        public void EmptyListTest()
        {
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void AddTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            Assert.AreEqual(1, list.Count);
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void GetTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            list.Add(new VocabularyItem("ある", true, "ある", "to be"));

            Assert.AreEqual(list[0], new VocabularyItem("食べる", false, "たべる", "to eat"));
            Assert.AreEqual(list[1], new VocabularyItem("飲む", false, "のむ", "to drink"));
            Assert.AreEqual(list[2], new VocabularyItem("ある", true, "ある", "to be"));
        }

        [Test]
        public void ClearTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            list.Add(new VocabularyItem("ある", true, "ある", "to be"));

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveAtTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            list.Add(new VocabularyItem("ある", true, "ある", "to be"));
            list.Add(new VocabularyItem("いる", true, "いる", "to be"));

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void ContainsTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            list.Add(new VocabularyItem("ある", true, "ある", "to be"));
            list.Add(new VocabularyItem("いる", true, "いる", "to be"));

            Assert.IsTrue(list.Contains(new VocabularyItem("食べる", false, "たべる", "to eat")));
            Assert.IsTrue(list.Contains(new VocabularyItem("飲む", false, "のむ", "to drink")));
            Assert.IsTrue(list.Contains(new VocabularyItem("ある", true, "ある", "to be")));
            Assert.IsTrue(list.Contains(new VocabularyItem("いる", true, "いる", "to be")));
            Assert.IsFalse(list.Contains(new VocabularyItem("ある", false, "いる", "to be")));
        }

        [Test]
        public void ContainsWordTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            list.Add(new VocabularyItem("ある", true, "ある", "to be"));
            list.Add(new VocabularyItem("いる", true, "いる", "to be"));

            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("飲む"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsTrue(list.ContainsWord("いる"));
            Assert.IsFalse(list.ContainsWord(""));
            Assert.IsFalse(list.ContainsWord("する"));

            list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            list.Add(new VocabularyItem("する", true, "する", "to do"));

            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("飲む"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsTrue(list.ContainsWord("いる"));
            Assert.IsFalse(list.ContainsWord(""));
            Assert.IsTrue(list.ContainsWord("する"));
        }
    }
}
