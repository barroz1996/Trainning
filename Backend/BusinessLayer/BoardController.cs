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
            public void Register(string email)  //Creates a new board in the dictionary
            {
                var newBoard = new Board(email);
                this.Boards.Add(email, newBoard);
            }
            public Board GetBoard(string email) //Returns the board of the current user
            {
            if (this.Boards.ContainsKey(email))
                return this.Boards[email];
            else
                throw new Exception("This email is not registered.");
            }
            public Column GetColumn(string email, string columnName) //Returns a specific column based on it's name.
            {
                return GetBoard(email).GetColumn(columnName);
            }
            public Column GetColumn(string email, int columnOrdinal)//Returns a specific column based on it's ordinal.
        {
                return GetBoard(email).GetColumn(columnOrdinal);
            }
            public void AddTask(string email, string title, string description, DateTime dueDate) //adds a new task to the first column.
            {
                if (title.Length == 0)
                    throw new Exception("Title can't be empty.");
                if(title.Length > 50)
                    throw new Exception("Title can't be longer than 300 characters.");
                if (description.Length > 300)
                    throw new Exception("Description is required to be longer than 300 characters.");
                if(!(DateTime.Compare(dueDate,DateTime.Now)>0))
                    throw new Exception("Due date has to be a futuristic date.");
                var newTask = new Task(this.totalTasks, title, description, dueDate); //After checking input legitimacy, creates a new task.
                this.totalTasks++;  //Total tasks serves as an input for new tasks' ids and grows by one every time a new task is created by any user.
                GetColumn(email, 0).AddTask(newTask);
            }
            public void LimitColumnTasks(string email, int columnOrdinal, int limit) //Updates a limit on a specific column.
            {
                GetColumn(email, columnOrdinal).LimitColumnTasks(limit);
            }
            public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate) //Update a specific task's due date.
            {
                if (!(DateTime.Compare(dueDate, DateTime.Now) > 0))
                    throw new Exception("Due date is required to be a futuristic date.");
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDueDate(dueDate);
            }
            public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)//Update a specific task's description.
        {
                if (description.Length > 300)
                    throw new Exception("Description can't be longer than 300 characters.");
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDescription(description);
            }
            public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)//Update a specific task's title.
        {
                if (title.Length == 0)
                    throw new Exception("Title can't be empty.");
                if (title.Length > 50)
                    throw new Exception("Title can't be longer than 300 characters.");
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskTitle(title);
            }
            public void AdvanceTask(string email, int columnOrdinal, int taskId) //advances a task to the next column.
            {
                if (columnOrdinal == 2)
                    throw new Exception("Cannot advance a task in the done column");
                if (GetColumn(email, (columnOrdinal + 1)).GetLimit() > (GetColumn(email, (columnOrdinal + 1)).GetTasks().Count)|| ((GetColumn(email, (columnOrdinal + 1)).GetLimit()==-1)))
                    GetColumn(email, (columnOrdinal + 1)).AddTask(GetColumn(email, columnOrdinal).RemoveTask(taskId));  //Removes a task from the current column and adds it to the next one.
                else //the condition makes sure that the next column has not reached it's limit.
                    throw new Exception("The next column is already full.");
            }
            public int GetTotalTasks() //Returns the total number of tasks from all the users.
            {
                return this.totalTasks;
            }
    }
}
