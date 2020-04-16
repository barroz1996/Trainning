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
            private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                {
                    log.Info("Tried getting board for unregistered user " + email);
                    throw new Exception("This email is not registered.");
                }
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
                {
                    log.Info("Tried adding new task with an empty title");
                    throw new Exception("Title can't be empty.");
                }
                if (title.Length > 50)
                {
                    log.Info("Tried adding new task with an title longer than 50 characters.");
                    throw new Exception("Title can't be longer than 50 characters.");
                }
                if (description.Length > 300)
                {
                    log.Info("Tried adding new task with an description longer than 300 characters.");
                    throw new Exception("Description can't be longer than 300 characters.");
                }
                if (!(DateTime.Compare(dueDate, DateTime.Now) > 0))
                {
                    log.Info("Tried adding new task with a non futuristic due date.");
                    throw new Exception("Due date is required to be a futuristic date.");
                }
                var newTask = new Task(this.totalTasks, title, description, dueDate); //After checking input legitimacy, creates a new task.
                this.totalTasks++;  //Total tasks serves as an input for new tasks' ids and grows by one every time a new task is created by any user.
                GetColumn(email, 0).AddTask(newTask);
                log.Info("Task "+(this.totalTasks-1)+" was created by user "+email+".");
            }
            public void LimitColumnTasks(string email, int columnOrdinal, int limit) //Updates a limit on a specific column.
            {
                GetColumn(email, columnOrdinal).LimitColumnTasks(limit);
                if (limit == -1)
                    log.Info("User " + email + " disabled the limit for column " + GetColumn(email, columnOrdinal).GetColumnName());
                else
                    log.Info("User " + email + " set the limit for column " + GetColumn(email, columnOrdinal).GetColumnName()+" to "+limit+".");
            }
            public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate) //Update a specific task's due date.
            {
                if (!(DateTime.Compare(dueDate, DateTime.Now) > 0))
                {
                    log.Info("Tried setting the due date of task "+taskId+" to a non futuristic due date.");
                    throw new Exception("Due date is required to be a futuristic date.");
                }
                else
                {
                    GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDueDate(dueDate);
                    log.Info("Updated the due date of task "+taskId+".");
                }
            }
            public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)//Update a specific task's description.
            {
                if (description.Length > 300)
                {
                    log.Info("Tried setting the description of task " + taskId + " to a description longer than 300 characters.");
                    throw new Exception("Description can't be longer than 300 characters.");
                }
                else
                {
                    GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDescription(description);
                    log.Info("Updated the description of task " + taskId + ".");
                }
            }
            public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)//Update a specific task's title.
            {
            if (title.Length == 0)
            {
                log.Info("Tried setting the title of task " + taskId + " to an empty title");
                throw new Exception("Title can't be empty.");
            }
                else
                {
                    if (title.Length > 50)
                    {
                        log.Info("Tried setting the title of task " + taskId + " to a title longer than 50 characters.");
                        throw new Exception("Title can't be longer than 50 characters.");
                    }
                    else
                    {
                        GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskTitle(title);
                        log.Info("Updated the description of task " + taskId + ".");
                    }
                }
            }
            public void AdvanceTask(string email, int columnOrdinal, int taskId) //advances a task to the next column.
            {
            if (columnOrdinal == 2)
            {
                log.Info("Tried advancing task " + taskId + " from the done column.");
                throw new Exception("Cannot advance a task in the done column.");
            }
            if (GetColumn(email, (columnOrdinal + 1)).GetLimit() > (GetColumn(email, (columnOrdinal + 1)).GetTasks().Count) || ((GetColumn(email, (columnOrdinal + 1)).GetLimit() == -1)))
            {
                GetColumn(email, (columnOrdinal + 1)).AddTask(GetColumn(email, columnOrdinal).RemoveTask(taskId));  //Removes a task from the current column and adds it to the next one.
                log.Info("Task " + taskId + " was advanced from the " + GetColumn(email, columnOrdinal).GetColumnName() + " column to the " + GetColumn(email, columnOrdinal + 1).GetColumnName() + " column.");
            }
            else //the condition makes sure that the next column has not reached it's limit.
                {
                log.Info("Tried advancing task " + taskId + " to a full column.");
                throw new Exception("The next column is already full.");
                }
            }
            public int GetTotalTasks() //Returns the total number of tasks from all the users.
            {
                return this.totalTasks;
            }
    }
}
