using System.Collections.Generic;
using System.Linq;
using JishoTangoAssistant.Core.Collections;
using JishoTangoAssistant.Core.Models;
using NUnit.Framework;

namespace JishoTangoAssistant.Tests;

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
        Assert.That(list, Is.Empty);
    }
        
    [Test]
    public void AssignmentTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            
        // Act
        list[0] = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
            
        // Assert
        Assert.That(list, Has.Count.EqualTo(1));
        var expected = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
        Assert.That(list, Contains.Item(expected));
    }

    [Test]
    public void AddTest()
    {
        // Act
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            
        // Assert
        Assert.That(list, Has.Count.EqualTo(2));
    }
        
    [Test]
    public void InsertTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            
        // Act
        list.Insert(1, new VocabularyItem("ある", true, "ある", [["to be"]]));
            
        // Assert
        Assert.That(list, Has.Count.EqualTo(3));
        var expected1 = new VocabularyItem("食べる", false, "たべる", [["to eat"]]);
        var expected2 = new VocabularyItem("ある", true, "ある", [["to be"]]);
        var expected3 = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
        Assert.That(list[0], Is.EqualTo(expected1));
        Assert.That(list[1], Is.EqualTo(expected2));
        Assert.That(list[2], Is.EqualTo(expected3));
    }

    [Test]
    public void GetTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            
        // Act
        var actual1 = list[0];
        var actual2 = list[1];
        var actual3 = list[2];

        // Assert
        var expected1 = new VocabularyItem("食べる", false, "たべる", [["to eat"]]);
        var expected2 = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
        var expected3 = new VocabularyItem("ある", true, "ある", [["to be"]]);
        Assert.That(actual1, Is.EqualTo(expected1));
        Assert.That(actual2, Is.EqualTo(expected2));
        Assert.That(actual3, Is.EqualTo(expected3));
    }

    [Test]
    public void ClearTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            
        // Act
        list.Clear();
            
        // Assert
        Assert.That(list, Is.Empty);
    }

    [Test]
    public void RemoveAtTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
        // Act
        list.RemoveAt(3);
            
        // Assert
        Assert.That(list, Has.Count.EqualTo(3));
        var expected1 = new VocabularyItem("食べる", false, "たべる", [["to eat"]]);
        var expected2 = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
        var expected3 = new VocabularyItem("ある", true, "ある", [["to be"]]);
        Assert.That(list[0], Is.EqualTo(expected1));
        Assert.That(list[1], Is.EqualTo(expected2));
        Assert.That(list[2], Is.EqualTo(expected3));
    }
        
    [Test]
    public void RemoveTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
            
        // Act
        list.Remove(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            
        // Assert
        Assert.That(list, Has.Count.EqualTo(2));
        var expected1 = new VocabularyItem("食べる", false, "たべる", [["to eat"]]);
        var expected2 = new VocabularyItem("ある", true, "ある", [["to be"]]);
        Assert.That(list[0], Is.EqualTo(expected1));
        Assert.That(list[1], Is.EqualTo(expected2));
    }

    [Test]
    public void ContainsTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));

        // Act
        var containsValue1 = list.Contains(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        var containsValue2 = list.Contains(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        var containsValue3 = list.Contains(new VocabularyItem("ある", true, "ある", [["to be"]]));
        var containsValue4 = list.Contains(new VocabularyItem("いる", true, "いる", [["to be"]]));
        var containsValue5 = list.Contains(new VocabularyItem("ある", false, "いる", [["to be"]]));
            
        // Assert
        Assert.That(containsValue1, Is.True);
        Assert.That(containsValue2, Is.True);
        Assert.That(containsValue3, Is.True);
        Assert.That(containsValue4, Is.True);
        Assert.That(containsValue5, Is.False);
    }
        
    [Test]
    public void AddRangeTest()
    {
        // Arrange
        var itemsToAdd = new List<VocabularyItem>
        {
            new("飲む", false, "のむ", [["to drink"]]),
            new("ある", true, "ある", [["to be"]]),
            new("いる", true, "いる", [["to be"]])
        };
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
            
        // Act
        list.AddRange(itemsToAdd);
            
        // Assert
        var containsWordValue1 = list.ContainsWord("食べる");
        var containsWordValue2 = list.ContainsWord("飲む");
        var containsWordValue3 = list.ContainsWord("ある");
        var containsWordValue4 = list.ContainsWord("いる");
        Assert.That(containsWordValue1, Is.True);
        Assert.That(containsWordValue2, Is.True);
        Assert.That(containsWordValue3, Is.True);
        Assert.That(containsWordValue4, Is.True);
    }

    [Test]
    public void ContainsWordTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));

        // Act
        var containsWordValue1 = list.ContainsWord("食べる");
        var containsWordValue2 = list.ContainsWord("飲む");
        var containsWordValue3 = list.ContainsWord("ある");
        var containsWordValue4 = list.ContainsWord("いる");
        var containsWordValue5 = list.ContainsWord("");
        var containsWordValue6 = list.ContainsWord("する");
            
        // Assert
        Assert.That(containsWordValue1, Is.True);
        Assert.That(containsWordValue2, Is.True);
        Assert.That(containsWordValue3, Is.True);
        Assert.That(containsWordValue4, Is.True);
        Assert.That(containsWordValue5, Is.False);
        Assert.That(containsWordValue6, Is.False);
    }
        
    [Test]
    public void ContainsWordTest_AddDuplicates()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("する", true, "する", [["to do"]]));

        // Act
        var containsWordValue1 = list.ContainsWord("食べる");
        var containsWordValue2 = list.ContainsWord("飲む");
        var containsWordValue3 = list.ContainsWord("ある");
        var containsWordValue4 = list.ContainsWord("いる");
        var containsWordValue5 = list.ContainsWord("");
        var containsWordValue6 = list.ContainsWord("する");
            
        // Assert
        Assert.That(containsWordValue1, Is.True);
        Assert.That(containsWordValue2, Is.True);
        Assert.That(containsWordValue3, Is.True);
        Assert.That(containsWordValue4, Is.True);
        Assert.That(containsWordValue5, Is.False);
        Assert.That(containsWordValue6, Is.True);
    }
        
    [Test]
    public void UndoEmptyListTest()
    {
        // Act
        list.Undo();
            
        // Assert
        Assert.That(list, Is.Empty);
    }
        
    [Test]
    public void UndoAssignmentTest()
    {
        // Arrange
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list[0] = new VocabularyItem("いる", true, "いる", [["to be"]]);
            
        // Act
        list.Undo();
            
        // Assert
        var expected = new VocabularyItem("ある", true, "ある", [["to be"]]);
        Assert.That(list[0], Is.EqualTo(expected));
    }

    [Test]
    public void UndoAddTest()
    {
        // Arrange
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
        // Act
        list.Undo();
            
        // Assert
        var containsIru = list.ContainsWord("いる");
        Assert.That(containsIru, Is.False);
        var expectedLastItem = new VocabularyItem("ある", true, "ある", [["to be"]]);
        Assert.That(list.Last(), Is.EqualTo(expectedLastItem));
    }
        
    [Test]
    public void UndoRemoveTest()
    {
        // Arrange
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
        list.Remove(new VocabularyItem("いる", true, "いる", [["to be"]]));
            
        // Act
        list.Undo();
            
        // Assert
        var containsIru = list.ContainsWord("いる");
        Assert.That(containsIru, Is.True);
    }

    [Test]
    public void UndoClearTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
        list.Clear();
            
        // Act
        list.Undo();
            
        // Assert
        var containsWordValue1 = list.ContainsWord("食べる");
        var containsWordValue2 = list.ContainsWord("飲む");
        var containsWordValue3 = list.ContainsWord("ある");
        var containsWordValue4 = list.ContainsWord("いる");
        Assert.That(containsWordValue1, Is.True);
        Assert.That(containsWordValue2, Is.True);
        Assert.That(containsWordValue3, Is.True);
        Assert.That(containsWordValue4, Is.True);
    }
        
    [Test]
    public void UndoRemoveAtTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
        list.RemoveAt(1);
            
        // Act
        list.Undo();
            
        // Assert
        var expected1 = new VocabularyItem("食べる", false, "たべる", [["to eat"]]);
        var expected2 = new VocabularyItem("飲む", false, "のむ", [["to drink"]]);
        var expected3 = new VocabularyItem("ある", true, "ある", [["to be"]]);
        var expected4 = new VocabularyItem("いる", true, "いる", [["to be"]]);
        Assert.That(list[0], Is.EqualTo(expected1));
        Assert.That(list[1], Is.EqualTo(expected2));
        Assert.That(list[2], Is.EqualTo(expected3));
        Assert.That(list[3], Is.EqualTo(expected4));
    }
        
    [Test]
    public void UndoInsertTest()
    {
        // Arrange
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));
        list.Add(new VocabularyItem("いる", true, "いる", [["to be"]]));
        list.Insert(1, new VocabularyItem("飲む", false, "のむ", [["to drink"]]));
            
        // Act
        list.Undo();
            
        // Assert
        var expected1 = new VocabularyItem("食べる", false, "たべる", [["to eat"]]);
        var expected2 = new VocabularyItem("ある", true, "ある", [["to be"]]);
        var expected3 = new VocabularyItem("いる", true, "いる", [["to be"]]);
        Assert.That(list[0], Is.EqualTo(expected1));
        Assert.That(list[1], Is.EqualTo(expected2));
        Assert.That(list[2], Is.EqualTo(expected3));
        var containsNomu = list.ContainsWord("飲む");
        Assert.That(containsNomu, Is.False);
    }
        
    [Test]
    public void UndoAddRangeTest()
    {
        // Arrange
        var itemsToAdd = new List<VocabularyItem>
        {
            new("飲む", false, "のむ", [["to drink"]]),
            new("ある", true, "ある", [["to be"]]),
            new("いる", true, "いる", [["to be"]])
        };
        list.Add(new VocabularyItem("食べる", false, "たべる", [["to eat"]]));
        list.AddRange(itemsToAdd);
            
        // Act
        list.Undo();
            
        // Assert
        var containsWordValue1 = list.ContainsWord("食べる");
        var containsWordValue2 = list.ContainsWord("飲む");
        var containsWordValue3 = list.ContainsWord("ある");
        var containsWordValue4 = list.ContainsWord("いる");
        Assert.That(containsWordValue1, Is.True);
        Assert.That(containsWordValue2, Is.False);
        Assert.That(containsWordValue3, Is.False);
        Assert.That(containsWordValue4, Is.False);
    }

    [Test]
    public void UndoMoreOperationsThanOtherOperationsTest()
    {
        // Arrange
        list.Add(new VocabularyItem("ある", true, "ある", [["to be"]]));

        // Act
        list.Undo();
        list.Undo();
            
        // Assert
        Assert.That(list, Is.Empty);
    }
}