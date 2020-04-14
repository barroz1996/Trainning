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
            if (this.Boards.ContainsKey(email))
                return this.Boards[email];
            else
                throw new Exception("This user is not registered.");
            }
            public Column GetColumn(string email, string columnName)
            {
                return this.Boards[email].GetColumn(columnName);
            }
            public Column GetColumn(string email, int columnOrdinal)
            {
                return this.Boards[email].GetColumn(columnOrdinal);
            }
            public void AddTask(string email, string title, string description, DateTime dueDate)
            {
                if (title.Length == 0)
                    throw new Exception("Title can't be empty.");
                if(title.Length > 50)
                    throw new Exception("Title can't be longer than 300 characters.");
                if (description.Length > 300)
                    throw new Exception("Description is required to be longer than 300 characters.");
                if(!(DateTime.Compare(dueDate,DateTime.Now)>0))
                    throw new Exception("Due date has to be a futuristic date.");
                var newTask = new Task(this.totalTasks, title, description, dueDate);
                this.totalTasks++;
                GetColumn(email, 0).AddTask(newTask);
            }
            public void LimitColumnTasks(string email, int columnOrdinal, int limit)
            {
                GetColumn(email, columnOrdinal).LimitColumnTasks(limit);
            }
            public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
            {
                if (!(DateTime.Compare(dueDate, DateTime.Now) > 0))
                    throw new Exception("Due date is required to be a futuristic date.");
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDueDate(dueDate);
            }
            public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
            {
                if (description.Length > 300)
                    throw new Exception("Description can't be longer than 300 characters.");
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDescription(description);
            }
            public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
            {
                if (title.Length == 0)
                    throw new Exception("Title can't be empty.");
                if (title.Length > 50)
                    throw new Exception("Title can't be longer than 300 characters.");
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskTitle(title);
            }
            public void AdvanceTask(string email, int columnOrdinal, int taskId)
            {
                if (GetColumn(email, (columnOrdinal + 1)).GetLimit() > (GetColumn(email, (columnOrdinal + 1)).GetTasks().Count)|| ((GetColumn(email, (columnOrdinal + 1)).GetLimit()==-1)))
                    GetColumn(email, (columnOrdinal + 1)).AddTask(GetColumn(email, columnOrdinal).RemoveTask(taskId));
                else
                    throw new Exception("The next column is already full.");
            }
        public int GetTotalTasks()
        {
            return this.totalTasks;
        }
        }
}
