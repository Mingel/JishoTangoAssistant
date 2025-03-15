using System.Collections.Generic;
using System.Linq;
using JishoTangoAssistant.Core.Collections;
using JishoTangoAssistant.Core.Models;
using NUnit.Framework;

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
        public void AssignmentTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list[0] = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(new VocabularyItem("飲む", false, "のむ", [["to drink"]]), list[0]);
        }

        [Test]
        public void AddTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            Assert.AreEqual(2, list.Count);
        }
        
        [Test]
        public void InsertTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Insert(1, new VocabularyItem("ある", true, "ある", [["to be"]]));
            
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(new VocabularyItem("食べる", false, "たべる", [["to eat"]]), list[0]);
            Assert.AreEqual(new VocabularyItem("ある", true, "ある", [["to be"]]), list[1]);
            Assert.AreEqual(new VocabularyItem("飲む", false, "のむ", [["to drink"]]), list[2]);
        }

        [Test]
        public void GetTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));

            Assert.AreEqual(list[0], new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            Assert.AreEqual(list[1], new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            Assert.AreEqual(list[2], new VocabularyItem("ある", true, "ある", [["to be"]]));
        }

        [Test]
        public void ClearTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Clear();
            
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void RemoveAtTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            list.Clear();
            
            Assert.AreEqual(0, list.Count);
        }
        
        [Test]
        public void RemoveTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Remove(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            
            Assert.AreEqual(2, list.Count);
            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsFalse(list.ContainsWord("飲む"));
        }

        [Test]
        public void ContainsTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));

            Assert.IsTrue(list.Contains(new VocabularyItem("食べる", false, "たべる", [["to eat"]])));
            Assert.IsTrue(list.Contains(new VocabularyItem("飲む", false, "のむ", [["to drink"]])));
            Assert.IsTrue(list.Contains(new VocabularyItem("ある", true, "ある", [["to be"]])));
            Assert.IsTrue(list.Contains(new VocabularyItem("いる", true, "いる", [["to be"]])));
            Assert.IsFalse(list.Contains(new VocabularyItem("ある", false, "いる", [["to be"]])));
        }
        
        [Test]
        public void AddRangeTest()
        {
            var itemsToAdd = new List<VocabularyItem>()
            {
                new("飲む", false, "のむ", [["to drink"]]),
                new("ある", true, "ある", [["to be"]]),
                new("いる", true, "いる", [["to be"]])
            };
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            
            list.AddRange(itemsToAdd);
            
            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("飲む"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsTrue(list.ContainsWord("いる"));
        }

        [Test]
        public void ContainsWordTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));

            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("飲む"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsTrue(list.ContainsWord("いる"));
            Assert.IsFalse(list.ContainsWord(""));
            Assert.IsFalse(list.ContainsWord("する"));

            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("する", true, "する", [["to do"]]));

            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("飲む"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsTrue(list.ContainsWord("いる"));
            Assert.IsFalse(list.ContainsWord(""));
            Assert.IsTrue(list.ContainsWord("する"));
        }
        
        [Test]
        public void UndoEmptyListTest()
        {
            list.Undo();
            
            Assert.IsEmpty(list);
        }
        
        [Test]
        public void UndoAssignmentTest()
        {
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list[0] = new VocabularyItem("いる", true, "いる", [["to be"]]);
            
            list.Undo();
            
            Assert.AreEqual(new VocabularyItem("ある", true, "ある", [["to be"]]), list[0]);
        }

        [Test]
        public void UndoAddTest()
        {
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
            list.Undo();
            
            Assert.IsFalse(list.ContainsWord("いる"));
            Assert.AreEqual(new VocabularyItem("ある", true, "ある", [["to be"]]), list.Last());
        }
        
        [Test]
        public void UndoRemoveTest()
        {
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            list.Remove(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
            list.Undo();
            
            Assert.IsTrue(list.ContainsWord("いる"));
        }

        [Test]
        public void UndoClearTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
            list.Clear();
            
            list.Undo();
            
            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsTrue(list.ContainsWord("飲む"));
            Assert.IsTrue(list.ContainsWord("ある"));
            Assert.IsTrue(list.ContainsWord("いる"));
        }
        
        [Test]
        public void UndoRemoveAtTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
            list.RemoveAt(1);
            
            list.Undo();
            
            Assert.AreEqual(new VocabularyItem("食べる", false, "たべる", [["to eat"]]), list[0]);
            Assert.AreEqual(new VocabularyItem("飲む", false, "のむ", [["to drink"]]), list[1]);
            Assert.AreEqual(new VocabularyItem("ある", true, "ある", [["to be"]]), list[2]);
            Assert.AreEqual(new VocabularyItem("いる", true, "いる", [["to be"]]), list[3]);
        }
        
        [Test]
        public void UndoInsertTest()
        {
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
            list.Insert(1, new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            
            list.Undo();
            
            Assert.AreEqual(new VocabularyItem("食べる", false, "たべる", [["to eat"]]), list[0]);
            Assert.AreEqual(new VocabularyItem("ある", true, "ある", [["to be"]]), list[1]);
            Assert.AreEqual(new VocabularyItem("いる", true, "いる", [["to be"]]), list[2]);
            Assert.IsFalse(list.ContainsWord("飲む"));
        }
        
        [Test]
        public void UndoAddRangeTest()
        {
            var itemsToAdd = new List<VocabularyItem>()
            {
                new("飲む", false, "のむ", [["to drink"]]),
                new("ある", true, "ある", [["to be"]]),
                new("いる", true, "いる", [["to be"]])
            };
            list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            
            list.AddRange(itemsToAdd);
            
            list.Undo();
            
            Assert.IsTrue(list.ContainsWord("食べる"));
            Assert.IsFalse(list.ContainsWord("飲む"));
            Assert.IsFalse(list.ContainsWord("ある"));
            Assert.IsFalse(list.ContainsWord("いる"));
        }

        [Test]
        public void UndoMoreOperationsThanOtherOperationsTest()
        {
            list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            
            list.Undo();
            list.Undo();
            
            Assert.IsEmpty(list);
        }
    }
}
