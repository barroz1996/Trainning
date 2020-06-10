using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;
using IntroSE.Kanban.Backend.DataAccessLayer.Controllers;
using Assert = NUnit.Framework.Assert;
using Moq;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TestTask
    {
        Board b;
        List<Mock<IDalController>> mocks;
        [SetUp]
        public void SetUp()
        {
            mocks = new List<Mock<IDalController>>();
            b = new Board("yakov@gmail.com");
        }
        [TestCase(1,"test")]
        [TestCase(3, "amos")]
        [TestCase(0, "")]
        [TestCase(4, "tg")]
        [TestCase(1, "test")]
        public void AddColumn(int newcol,string name)
        {
            for (int i = newcol; i < b.GetColumns().Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[mocks.Count - 1].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i + 1, "yakov@gmail.com")).Returns(true);
            }
            b.AddColumn(newcol, name);
            Assert.AreEqual(name, b.GetColumn(newcol).GetColumnName(), "test failed");
        }
        [Test]
        public void MoveColumn()
        {
            for (int i = 0; i < b.GetColumns().Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[mocks.Count - 1].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i - 1, "yakov@gmail.com")).Returns(true);
                mocks.Add(new Mock<IDalController>());
                mocks[mocks.Count-1].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i + 1, "yakov@gmail.com")).Returns(true);
            }
            string name = b.GetColumn(1).GetColumnName();
            b.MoveColumn(1, 1);
            Assert.AreEqual(name, b.GetColumn(2).GetColumnName(), "test failed");
        }
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(3)]
        public void RemoveColumn(int col)
        {
            for (int i = 0; i < b.GetColumns().Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[mocks.Count - 1].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i - 1, "yakov@gmail.com")).Returns(true);
            }
            Column removed = b.GetColumn(col);
            b.RemoveColumn(col);
            Assert.AreEqual(false, b.GetColumns().Contains(removed), "Failed to remove column number "+col);      
        }
        [TearDown]
        public void TearDown() {
            mocks.Clear();
        }

    }
}
