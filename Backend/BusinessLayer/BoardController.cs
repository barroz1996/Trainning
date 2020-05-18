using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class BoardController
    {
        private DataAccessLayer.Controllers.BoardControl BoardCon = new DataAccessLayer.Controllers.BoardControl();
        private DataAccessLayer.Controllers.ColumnControl ColumnCon = new DataAccessLayer.Controllers.ColumnControl();
        private DataAccessLayer.Controllers.TaskControl TaskCon = new DataAccessLayer.Controllers.TaskControl();
        private Dictionary<string, Board> Boards;
        private int totalTasks;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BoardController()
        {
            this.Boards = new Dictionary<string, Board>();
            this.totalTasks = 0;
        }

        public void Register(string email)  //Creates a new board in the dictionary.
        {
            var newBoard = new Board(email);
            this.Boards.Add(email, newBoard);
            BoardCon.Insert(new DataAccessLayer.DTOs.BoardDTO(email));
            foreach(Column col in GetBoard(email).GetColumns())
            {
                ColumnCon.Insert(new DataAccessLayer.DTOs.ColumnDTO(col.GetColumnOrdinal(), col.GetColumnName(), col.GetLimit(), email));
            }
        }
        public Board GetBoard(string email) //Returns the board of the current user.
        {
            if (this.Boards.ContainsKey(email))
                return this.Boards[email];
            else
            {
                log.Debug("Tried getting board for unregistered user " + email);
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
            GetBoard(email).AddTask(this.totalTasks, title, description, dueDate); //After checking input legitimacy, creates a new task.);
            this.totalTasks++;  //Total tasks serves as an input for new tasks' ids and grows by one every time a new task is created by any user.
        }
        public void LimitColumnTasks(string email, int columnOrdinal, int limit) //Updates a limit on a specific column.
        {
           // if (columnOrdinal == 1) //added for testing purposes in the submission system only.
            {
                GetColumn(email, columnOrdinal).LimitColumnTasks(limit);
                if (limit == -1)
                    log.Debug("User " + email + " disabled the limit for column " + GetColumn(email, columnOrdinal).GetColumnName());
                else
                    log.Debug("User " + email + " set the limit for column " + GetColumn(email, columnOrdinal).GetColumnName() + " to " + limit + ".");
                ColumnCon.Update(columnOrdinal, DataAccessLayer.DTOs.ColumnDTO.ColumnLimitColumnLimit, limit, email);
            }
          /*  else
            {
                log.Debug("tried limiting a column other than the in progress column.");
                throw new Exception("can only limit of the in progress column.");
            }*/
        }
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate) //Update a specific task's due date.
        {
            if (columnOrdinal == GetBoard(email).GetColumns().Count - 1)
            {
                log.Debug("Tried update task a task from the last column.");
                throw new Exception("Cannot update a task in the last column.");
            }
            if (!(DateTime.Compare(dueDate, DateTime.Now) > 0))
            {
                log.Debug("Tried setting the due date of task " + taskId + " to a non futuristic due date.");
                throw new Exception("Due date is required to be a futuristic date.");
            }
            else
            {
                GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDueDate(dueDate);
                TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksDueDateColumnDueDate, dueDate);
                log.Debug("Updated the due date of task " + taskId + ".");
            }
        }
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)//Update a specific task's description.
        {
            if (columnOrdinal == GetBoard(email).GetColumns().Count-1)
            {
                log.Debug("Tried update task a task from the last column.");
                throw new Exception("Cannot update a task in the last column.");
            }
            if (description != null)
            {
                if (description.Length > 300)
                {
                    log.Debug("Tried setting the description of task " + taskId + " to a description longer than 300 characters.");
                    throw new Exception("Description can't be longer than 300 characters.");
                }
            }
            GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskDescription(description);
            TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksDescriptionColumnDescription, description);
            log.Debug("Updated the description of task " + taskId + ".");
        }
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)//Update a specific task's title.
        {
            if (columnOrdinal == GetBoard(email).GetColumns().Count - 1)
            {
                log.Debug("Tried update task a task from the last column.");
                throw new Exception("Cannot update a task in the last column.");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                log.Debug("Tried setting the title of task " + taskId + " to an empty title");
                throw new Exception("Title can't be empty.");
            }
            else
            {
                if (title.Length > 50)
                {
                    log.Debug("Tried setting the title of task " + taskId + " to a title longer than 50 characters.");
                    throw new Exception("Title can't be longer than 50 characters.");
                }
                else
                {
                    GetColumn(email, columnOrdinal).GetTask(taskId).EditTaskTitle(title);
                    TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksTitleColumnTitle, title);
                    log.Debug("Updated the description of task " + taskId + ".");
                }
            }
        }
        public void AdvanceTask(string email, int columnOrdinal, int taskId) //advances a task to the next column.
        {
            if (columnOrdinal == GetBoard(email).GetColumns().Count - 1)
            {
                log.Debug("Tried advancing task " + taskId + " from the last column.");
                throw new Exception("Cannot advance a task in the last column.");
            }
            if (GetColumn(email, (columnOrdinal + 1)).GetLimit() > (GetColumn(email, (columnOrdinal + 1)).GetTasks().Count) || ((GetColumn(email, (columnOrdinal + 1)).GetLimit() == -1)))
            {

                GetColumn(email, (columnOrdinal + 1)).AddTask(GetColumn(email, columnOrdinal).RemoveTask(taskId));  //Removes a task from the current column and adds it to the next one.
                log.Debug("Task " + taskId + " was advanced from the " + GetColumn(email, columnOrdinal).GetColumnName() + " column to the " + GetColumn(email, columnOrdinal + 1).GetColumnName() + " column.");
                TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksColumnIdColumnColumnId, columnOrdinal + 1);
            }
            else //the condition makes sure that the next column has not reached it's limit.
            {
                log.Debug("Tried advancing task " + taskId + " to a full column.");
                throw new Exception("The next column is already full.");
            }
        }
        public int GetTotalTasks() //Returns the total number of tasks from all the users.
        {
            return this.totalTasks;
        }
        public void LoadData()
        {    
            var dalboard = BoardCon.Select();
            foreach (var dal in dalboard)
            {
                var colList = new List<Column>();  
                var dalCol = ColumnCon.SelectColumn(dal.Email);
                foreach (var cdal in dalCol)
                {
                    var newCol = new Column(cdal.ColumnOrdinal, cdal.ColumnName, cdal.Limit);
                    var dalTask = TaskCon.SelectTasks(dal.Email, cdal.ColumnOrdinal);
                    foreach (var tdal in dalTask)
                    {
                        newCol.AddTask(new Task(tdal.TaskId, tdal.Title, tdal.Description, tdal.DueDate, tdal.CreationTime));
                    }
                    if (colList.Count <= newCol.GetColumnOrdinal())
                        colList.Add(newCol);
                    else
                        colList.Insert(newCol.GetColumnOrdinal(), newCol);
                }
                Boards.Add(dal.Email, new Board(dal.Email, colList));
            }
            foreach (var entry in Boards)
            {
                this.totalTasks = this.totalTasks + entry.Value.TotalTask();
            }
        }
        public Column AddColumn(string email, int columnOrdinal, string Name)
        {
            return GetBoard(email).AddColumn(columnOrdinal, Name);       
        }
        public Column MoveColumn(string email, int columnOrdinal,int direction)
        {
            return GetBoard(email).MoveColumn(columnOrdinal, direction);
        }
        public void RemoveColumn(string email, int columnOrdinal)
        {
            GetBoard(email).RemoveColumn(columnOrdinal);
        }
        public void Delete()
        {
            BoardCon.DeleteTable();
            ColumnCon.DeleteTable();
            TaskCon.DeleteTable();
            Boards.Clear();
        }
    }
}
