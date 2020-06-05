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
        [Test]
        public void AddColumn()
        {
            for (int i = 0; i < mocks.Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[i].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i + 1, "yakov@gmail.com")).Returns(true);
            }
            b.AddColumn(1, "test");
            Assert.AreEqual("test", b.GetColumn(1).GetColumnName(), "test failed");
        }
        [Test]
        public void MoveColumn()
        {
            for (int i = 0; i < mocks.Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[i].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i - 1, "yakov@gmail.com")).Returns(true);
                mocks.Add(new Mock<IDalController>());
                mocks[i].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i + 1, "yakov@gmail.com")).Returns(true);
            }
            string name = b.GetColumn(1).GetColumnName();
            b.MoveColumn(1, 1);
            Assert.AreEqual(name, b.GetColumn(2).GetColumnName(), "test failed");
        }
        [Test]
        public void RemoveColumn()
        {
            for (int i = 0; i < mocks.Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[i].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i - 1, "yakov@gmail.com")).Returns(true);
            }
            Column removed = b.GetColumn(1);
            b.RemoveColumn(1);
            Assert.AreEqual(false, b.GetColumns().Contains(removed), "test failed");
        }
        [TearDown]
        public void TearDown() {
            mocks.Clear();
        }

    }
}
