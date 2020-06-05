using System;
using System.Collections.Generic;


namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    internal class BoardController
    {
        private DataAccessLayer.Controllers.BoardControl BoardCon = new DataAccessLayer.Controllers.BoardControl();
        private DataAccessLayer.Controllers.ColumnControl ColumnCon = new DataAccessLayer.Controllers.ColumnControl();
        private DataAccessLayer.Controllers.TaskControl TaskCon = new DataAccessLayer.Controllers.TaskControl();
        private DataAccessLayer.Controllers.BoardEmailsControl BoardEmail = new DataAccessLayer.Controllers.BoardEmailsControl();
        private Dictionary<string, Board> Boards;
        private int totalTasks;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BoardController()
        {
            Boards = new Dictionary<string, Board>();
            totalTasks = 0;
        }

        public void Register(string email)  //Creates a new board.
        {
            var newBoard = new Board(email);
            Boards.Add(email, newBoard);
            BoardCon.Insert(new DataAccessLayer.DTOs.BoardDTO(email,0)); //inserts the new board to the database.
            BoardEmail.Insert(new DataAccessLayer.DTOs.BoardEmailsDTO(email, email));
            foreach (Column col in GetBoard(email).GetColumns())
            {
                ColumnCon.Insert(new DataAccessLayer.DTOs.ColumnDTO(col.GetColumnOrdinal(), col.GetColumnName(), col.GetLimit(), email)); //inserts all of the new columns of the new board to the database.
            }
        }
        public Board GetBoard(string email) //Returns the board of the current user.
        {
            if (Boards.ContainsKey(email))
            {
                return Boards[email];
            }
            else
            {
                log.Debug("This email " + email+" is not the host!" );
                throw new Exception("This email is not the host!.");
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
        public void AddTask(string email, string title, string description, DateTime dueDate,string emailHost) //adds a new task to the first column.
        {
            GetBoard(emailHost).AddTask(totalTasks, title, description, dueDate, email); //After checking input legitimacy, creates a new task.);
            GetColumn(emailHost, 0).GetTasks().Sort((x, y) => DateTime.Compare(x.GetDueDate(), y.GetDueDate()));
            totalTasks++;  //Total tasks serves as an input for new tasks' ids and grows by one every time a new task is created by any user.
        }
        public void LimitColumnTasks(string email, int columnOrdinal, int limit) //Updates a limit on a specific column.
        {
            {
                GetColumn(email, columnOrdinal).LimitColumnTasks(limit);
                if (limit == -1) //limit disabled.
                {
                    log.Debug("User " + email + " disabled the limit for column " + GetColumn(email, columnOrdinal).GetColumnName());
                }
                else //new limit set.
                {
                    log.Debug("User " + email + " set the limit for column " + GetColumn(email, columnOrdinal).GetColumnName() + " to " + limit + ".");
                }

                ColumnCon.Update(columnOrdinal, DataAccessLayer.DTOs.ColumnDTO.ColumnLimitColumnLimit, limit, email);
            }
        }
        public void UpdateTaskDueDate(string email,string emailHost, int columnOrdinal, int taskId, DateTime dueDate) //Update a specific task's due date.
        {
            if (columnOrdinal == GetBoard(emailHost).GetColumns().Count - 1)
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
                if(!email.Equals(GetColumn(emailHost, columnOrdinal).GetTask(taskId).GetEmailAssignee())){
                    log.Debug("This email is not the assignee of the task");
                    throw new Exception("This email is not the assignee of the task");
                }

                GetColumn(emailHost, columnOrdinal).GetTask(taskId).EditTaskDueDate(dueDate);
                GetColumn(emailHost, columnOrdinal).GetTasks().Sort((x, y) => DateTime.Compare(x.GetDueDate(), y.GetDueDate()));
                TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksDueDateColumnDueDate, dueDate);
                log.Debug("Updated the due date of task " + taskId + ".");
            }
        }
        public void UpdateTaskDescription(string email,string emailHost, int columnOrdinal, int taskId, string description)//Update a specific task's description.
        {
            if (columnOrdinal == GetBoard(emailHost).GetColumns().Count - 1)
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
            if (!email.Equals(GetColumn(emailHost, columnOrdinal).GetTask(taskId).GetEmailAssignee())){
                log.Debug("This email is not the assignee of the task");
                throw new Exception("This email is not the assignee of the task");
            }
            GetColumn(emailHost, columnOrdinal).GetTask(taskId).EditTaskDescription(description);
            TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksDescriptionColumnDescription, description);
            log.Debug("Updated the description of task " + taskId + ".");
        }
        public void UpdateTaskTitle(string email,string emailHost, int columnOrdinal, int taskId, string title)//Update a specific task's title.
        {
            if (columnOrdinal == GetBoard(emailHost).GetColumns().Count - 1)
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
                    if (!email.Equals(GetColumn(emailHost, columnOrdinal).GetTask(taskId).GetEmailAssignee())){
                        log.Debug("This email is not the assignee of the task");
                        throw new Exception("This email is not the assignee of the task");
                    }
                    GetColumn(emailHost, columnOrdinal).GetTask(taskId).EditTaskTitle(title);
                    TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksTitleColumnTitle, title);
                    log.Debug("Updated the description of task " + taskId + ".");
                }
            }
        }
        public void AdvanceTask(string email,string emailHost, int columnOrdinal, int taskId) //advances a task to the next column.
        {
            if (columnOrdinal == GetBoard(emailHost).GetColumns().Count - 1)
            {
                log.Debug("Tried advancing task " + taskId + " from the last column.");
                throw new Exception("Cannot advance a task in the last column.");
            }
            if (GetColumn(emailHost, (columnOrdinal + 1)).GetLimit() > (GetColumn(emailHost, (columnOrdinal + 1)).GetTasks().Count) || ((GetColumn(emailHost, (columnOrdinal + 1)).GetLimit() == -1)))
            {
                if (!email.Equals(GetColumn(emailHost, columnOrdinal).GetTask(taskId).GetEmailAssignee())){
                    log.Debug("This email is not the assignee of the task");
                    throw new Exception("This email is not the assignee of the task");
                }
                GetColumn(emailHost, (columnOrdinal + 1)).AddTask(GetColumn(email, columnOrdinal).RemoveTask(taskId));  //Removes a task from the current column and adds it to the next one.
                GetColumn(emailHost, columnOrdinal+1).GetTasks().Sort((x, y) => DateTime.Compare(x.GetDueDate(), y.GetDueDate()));
                log.Debug("Task " + taskId + " was advanced from the " + GetColumn(emailHost, columnOrdinal).GetColumnName() + " column to the " + GetColumn(emailHost, columnOrdinal + 1).GetColumnName() + " column.");
                TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksColumnIdColumnColumnId, columnOrdinal + 1);
            }
            //the condition makes sure that the next column has free space for the current task.
            else
            {
                log.Debug("Tried advancing task " + taskId + " to a full column.");
                throw new Exception("The next column is already full.");
            }
        }
        public int GetTotalTasks() //Returns the total number of tasks from all the users.
        {
            return totalTasks;
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
                        newCol.AddTask(new Task(tdal.TaskId, tdal.Title, tdal.Description, tdal.DueDate, tdal.CreationTime,tdal.EmailAssignee));
                    }
                    newCol.GetTasks().Sort((x, y) => DateTime.Compare(x.GetDueDate(), y.GetDueDate()));
                    if (colList.Count <= newCol.GetColumnOrdinal()) //if this is the last column so far, add it to the end of the list.
                    {
                        colList.Add(newCol);
                    }
                    else //if it is not, add it in a specific location based on it's ordinal.
                    {
                        colList.Insert(newCol.GetColumnOrdinal(), newCol);
                    }

                }
                var boardEmailList =new List<string>();
                var dalBoardEmail = BoardEmail.SelectBoard(dal.Email);
                foreach (var email in dalBoardEmail)
                {
                    boardEmailList.Add(email.Email);
                }
                Boards.Add(dal.Email, new Board(dal.Email, colList,boardEmailList,dal.DeletedTasks));              
                   
            }
            foreach (var entry in Boards) 
            {// we need to change the name of totaltasks to > nextId
                totalTasks = totalTasks + entry.Value.TotalTask()+entry.Value.GetDeletedTasks();
            }
        }
        public Column AddColumn(string email, int columnOrdinal, string Name)
        {
            return GetBoard(email).AddColumn(columnOrdinal, Name);
        }
        public Column MoveColumn(string email, int columnOrdinal, int direction) //recieves direction (1 or -1 from service).
        {
            return GetBoard(email).MoveColumn(columnOrdinal, direction);
        }
        public void RemoveColumn(string email, int columnOrdinal)
        {
            GetBoard(email).RemoveColumn(columnOrdinal);
            if (columnOrdinal == 0)
            {
                GetColumn(email, 0).GetTasks().Sort((x, y) => DateTime.Compare(x.GetDueDate(), y.GetDueDate()));
            }
            else
            {
                GetColumn(email, columnOrdinal-1).GetTasks().Sort((x, y) => DateTime.Compare(x.GetDueDate(), y.GetDueDate()));
            }
        }
        public void Delete() //deletes all data from businesslayer based tables, clears the dictionary and sets the total tasks to 0.
        {
            BoardCon.DeleteTable();
            ColumnCon.DeleteTable();
            TaskCon.DeleteTable();
            BoardEmail.DeleteTable();
            Boards.Clear();
            totalTasks = 0;
        }
        public void DeleteTask(string email, int columnOrdinal, int taskId,string emailHost)
        {

            if (!email.Equals(GetBoard(emailHost).GetColumn(columnOrdinal).GetTask(taskId).GetEmailAssignee()))
            {
                log.Debug("This email is not the assignee of the task");
                throw new Exception("This email is not the assignee of the task");

            }
            if (columnOrdinal == GetBoard(emailHost).GetColumns().Count - 1)
            {
                log.Debug("Tried deleting task " + taskId + " from the last column.");
                throw new Exception("Cannot delete a task from the last column.");
            }
            GetBoard(emailHost).GetColumn(columnOrdinal).GetTasks().Remove(GetBoard(emailHost).GetColumn(columnOrdinal).GetTask(taskId));
            GetBoard(emailHost).SetDeletedTasks();
            BoardCon.Update(emailHost, DataAccessLayer.DTOs.BoardDTO.BoardDeletedTaskColumn, GetBoard(emailHost).GetDeletedTasks());
            TaskCon.Delete(taskId);
        }
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee,string emailHost)
        {
            if (columnOrdinal == GetBoard(emailHost).GetColumns().Count - 1)
            {
                log.Debug("Tried assigning task " + taskId + " from the last column.");
                throw new Exception("Cannot assign a task from the last column.");
            }
            if (!GetBoard(emailHost).GetBoardEmail().Contains(emailAssignee))
            {
                log.Debug("This email is not the assigned to this board");
                throw new Exception("This email is not the assigned to this board");
            }
            if (!email.Equals(GetBoard(emailHost).GetColumn(columnOrdinal).GetTask(taskId).GetEmailAssignee()))
            {
                log.Debug("This email is not the assignee of the task");
                throw new Exception("This email is not the assignee of the task");

            }
            if (emailAssignee.Equals(email))
            {
                log.Debug("The emailAssignee is similar to the current assignee");
                throw new Exception("The emailAssignee is similar to the current assignee");

            }
            GetBoard(emailHost).GetColumn(columnOrdinal).GetTask(taskId).SetEmailAssignee(emailAssignee);
            TaskCon.Update(taskId, DataAccessLayer.DTOs.TaskDTO.TasksEmailAssigneeColumn, emailAssignee);

        }
        public void AddToBoard(string email,string host)
        {
            GetBoard(host).GetBoardEmail().Add(email);
            BoardEmail.Insert(new DataAccessLayer.DTOs.BoardEmailsDTO(email, host));
        }
        public void ChangeColumnName(string email,int columnOrdinal,string newName)
        {
            GetBoard(email).ChangeColumnName(columnOrdinal, newName);
        }

    }
}
