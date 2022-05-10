using NUnit.Framework;

namespace JishoTangoAssistant.Tests
{
    [TestFixture]
    public class ObservableVocabularyListTest
    {
        private ObservableVocabularyList _list;

        [SetUp]
        public void Init()
        {
            _list = new ObservableVocabularyList();
        }

        [TearDown]
        public void Cleanup()
        {
            _list = new ObservableVocabularyList();
        }

        [Test]
        public void EmptyListTest()
        {
            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void AddTest()
        {
            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            Assert.AreEqual(1, _list.Count);
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            Assert.AreEqual(2, _list.Count);
        }

        [Test]
        public void GetTest()
        {
            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            _list.Add(new VocabularyItem("ある", true, "ある", "to be"));

            Assert.AreEqual(_list[0], new VocabularyItem("食べる", false, "たべる", "to eat"));
            Assert.AreEqual(_list[1], new VocabularyItem("飲む", false, "のむ", "to drink"));
            Assert.AreEqual(_list[2], new VocabularyItem("ある", true, "ある", "to be"));
        }

        [Test]
        public void ClearTest()
        {
            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            _list.Add(new VocabularyItem("ある", true, "ある", "to be"));

            _list.Clear();
            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void RemoveAtTest()
        {
            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            _list.Add(new VocabularyItem("ある", true, "ある", "to be"));
            _list.Add(new VocabularyItem("いる", true, "いる", "to be"));

            _list.Clear();
            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void ContainsTest()
        {
            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            _list.Add(new VocabularyItem("ある", true, "ある", "to be"));
            _list.Add(new VocabularyItem("いる", true, "いる", "to be"));

            Assert.IsTrue(_list.Contains(new VocabularyItem("食べる", false, "たべる", "to eat")));
            Assert.IsTrue(_list.Contains(new VocabularyItem("飲む", false, "のむ", "to drink")));
            Assert.IsTrue(_list.Contains(new VocabularyItem("ある", true, "ある", "to be")));
            Assert.IsTrue(_list.Contains(new VocabularyItem("いる", true, "いる", "to be")));
            Assert.IsFalse(_list.Contains(new VocabularyItem("ある", false, "いる", "to be")));
        }

        [Test]
        public void ContainsWordTest()
        {
            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            _list.Add(new VocabularyItem("ある", true, "ある", "to be"));
            _list.Add(new VocabularyItem("いる", true, "いる", "to be"));

            Assert.IsTrue(_list.ContainsWord("食べる"));
            Assert.IsTrue(_list.ContainsWord("飲む"));
            Assert.IsTrue(_list.ContainsWord("ある"));
            Assert.IsTrue(_list.ContainsWord("いる"));
            Assert.IsFalse(_list.ContainsWord(""));
            Assert.IsFalse(_list.ContainsWord("する"));

            _list.Add(new VocabularyItem("食べる", false, "たべる", "to eat"));
            _list.Add(new VocabularyItem("飲む", false, "のむ", "to drink"));
            _list.Add(new VocabularyItem("する", true, "する", "to do"));

            Assert.IsTrue(_list.ContainsWord("食べる"));
            Assert.IsTrue(_list.ContainsWord("飲む"));
            Assert.IsTrue(_list.ContainsWord("ある"));
            Assert.IsTrue(_list.ContainsWord("いる"));
            Assert.IsFalse(_list.ContainsWord(""));
            Assert.IsTrue(_list.ContainsWord("する"));
        }
    }
}
