using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircularLinkedList.Test
{
    [TestClass]
    public class CircularLinkedListTests
    {
        [TestMethod]
        public void TestItemIsAddedToLast()
        {
            //Arrange
            CircularLinkedList.LinkedList<int> items = new CircularLinkedList.LinkedList<int>();
            items.AddLast(0);
            items.AddLast(2);
            //Act
            var itemsCount = items.Count;
            //Assert
            Assert.AreEqual(2, itemsCount);
        }

        [TestMethod]
        public void TestAddLastReturnsItemAddedToLast()
        {
            //Arrange
            CircularLinkedList.LinkedList<int> items = new CircularLinkedList.LinkedList<int>();
            items.AddLast(0);
            items.AddLast(40);
            items.AddLast(22);
            //Act
            var last = items.Tail.Value;
            //Assert
            Assert.AreEqual(22, last);
        }

        [TestMethod]
        public void TestRemoveSuccessfullyRemovesItemInMiddle()
        {
            //Arrange
            CircularLinkedList.LinkedList<int> items = new CircularLinkedList.LinkedList<int>();
            items.AddLast(0);
            items.AddLast(20);
            items.AddLast(40);
            items.AddLast(22);
            //Act
            bool result = items.Remove(40);
            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestRemoveSuccessfullyRemovesItemAtHead()
        {
            //Arrange
            CircularLinkedList.LinkedList<int> items = new CircularLinkedList.LinkedList<int>();
            items.AddLast(0);
            items.AddLast(20);
            items.AddLast(40);
            items.AddLast(22);
            //Act
            items.Remove(0);
            var result = items.Head.Value;
            //Assert
            Assert.AreEqual(20, result);
        }
    }
}
