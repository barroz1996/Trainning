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
        DateTime date;
        List<Mock<IDalController>> mocks;
        [SetUp]
        public void SetUp()
        {
            mocks = new List<Mock<IDalController>>();
            b = new Board("yakov@gmail.com");
            b.GetColumn(2).GetTasks().Add(new Task());
            date = new DateTime(2030, 5, 1, 8, 30, 52);
        }
        [TestCase(1,"test")]
        [TestCase(3, "amos")]
        [TestCase(0, "")]
        [TestCase(4, "tg")]
        [TestCase(1, "test")]
        [TestCase(1,"backlog")]
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
        [TestCase(1,1)]
        [TestCase(0,-1)]
        [TestCase(2,1)]
        public void MoveColumn(int ordinal,int direction)
        {
            for (int i = 0; i < b.GetColumns().Count; i++)
            {
                mocks.Add(new Mock<IDalController>());
                mocks[mocks.Count - 1].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i - 1, "yakov@gmail.com")).Returns(true);
                mocks.Add(new Mock<IDalController>());
                mocks[mocks.Count-1].Setup(m => m.Update(i, IntroSE.Kanban.Backend.DataAccessLayer.DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal, i + 1, "yakov@gmail.com")).Returns(true);
            }
            string name = b.GetColumn(ordinal).GetColumnName();
            b.MoveColumn(ordinal, direction);
            Assert.AreEqual(name, b.GetColumn(ordinal + direction).GetColumnName(), "test failed");
        }

        [TestCase("asd","hello", 2000, 5, 1, 8, 30, 52)]
        [TestCase("asd","hello", 2030, 5, 1, 8, 30, 52)]
        [TestCase("","hello", 2030, 5, 1, 8, 30, 52)]
        [TestCase("asddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd","hello", 2030, 5, 1, 8, 30, 52)]
        [TestCase("asd", "asdddddddddddddddddddddassssssssssssssssssssssss" +
            "ssssssssddddddddddddddddddddddqwqwdqqqqqqqqqqqqqqqqqqqqq" +
            "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq" +
            "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqddddssssssssssssssssssss" +
            "sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" +
            "sssssssssddddddddddddddddddddddddddd", 2030, 5, 1, 8, 30, 52)]
        public void AddTasks(string title,string description,int a,int w,int c,int d,int e,int f)
        {
            var DueDate = new DateTime(a, w, c, d, e, f);
            mocks.Add(new Mock<IDalController>());
            mocks[mocks.Count - 1].Setup(m => m.Insert(new IntroSE.Kanban.Backend.DataAccessLayer.DTOs.TaskDTO(0,title,description,DueDate,DateTime.Now, "yakov@gmail.com",0, "yakov@gmail.com"))).Returns(true);
            b.AddTask(0, title, description, DueDate, "yakov@gmail.com");
            Assert.AreEqual(title, b.GetColumn(0).GetTask(0).GetTitle(),"task failed");
        }
        [TearDown]
        public void TearDown() {
            mocks.Clear();
        }

    }
}
