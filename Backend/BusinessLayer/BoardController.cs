using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class BoardController
    {
        private Dictionary<string, Board> Boards;
        private int totalTasks;
        public BoardController()
        {
            this.Boards = new Dictionary<string, Board>();
            this.totalTasks = 0;
        }
        public void Register(string email)
        {
            var newBoard = new Board(email);
            this.Boards.Add(email, newBoard);
        }
        public Board GetBoard(string email)
        {
            return this.Boards[email];
        }
        public Column GetColumn(string email,string columnName)
        {
            return this.Boards[email].GetColumn(columnName);
        }
        public Column GetColumn(string email, int columnOrdinal)
        {
            return this.Boards[email].GetColumn(columnOrdinal);
        }
        public void AddTask(string email, string title, string description, DateTime dueDate)
        {
            var newTask = new Task(this.totalTasks, title, description, dueDate);
            this.totalTasks++;
            GetColumn(email, 0).addTask(newTask);
        }
        public void LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            GetColumn(email, columnOrdinal).LimitColumnTasks(limit);
        }
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDueDate(dueDate);
        }
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDescription(description);
        }
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskTitle(title);
        }
        public void AdvanceTask(string email,int columnOrdinal, int taskId)
        {
            GetColumn(email, (columnOrdinal+1)).AddTask(GetColumn(email, columnOrdinal).RemoveTask(taskId));
        }
    }
}
