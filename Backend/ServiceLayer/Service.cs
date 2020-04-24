
using System;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// The service for using the Kanban board.
    /// It allows executing all of the required behaviors by the Kanban board.
    /// You are not allowed (and can't due to the interfance) to change the signatures
    /// Do not add public methods\members! Your client expects to use specifically these functions.
    /// You may add private, non static fields (if needed).
    /// You are expected to implement all of the methods.
    /// Good luck.
    /// </summary>
    public class Service : IService
    {
        private BusinessLayer.UserPackage.UserController UserController;
        private BusinessLayer.BoardPackage.BoardController BoardController;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Simple public constructor.
        /// </summary>
        public Service()
        {
            this.UserController = new BusinessLayer.UserPackage.UserController();
            this.BoardController = new BusinessLayer.BoardPackage.BoardController();
        }

        /// <summary>        
        /// Loads the data. Intended be invoked only when the program starts
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error.</returns>
        public Response LoadData()
        {
            UserController.LoadData();
            BoardController.LoadData();

            return new Response();
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="email">The email address of the user to register</param>
        /// <param name="password">The password of the user to register</param>
        /// <param name="nickname">The nickname of the user to register</param>
        /// <returns>A response object. The response should contain a error message in case of an error<returns>
        public Response Register(string email, string password, string nickname)
        {
            try
            {
                UserController.PasswordVerify(password);
                UserController.Register(email, password, nickname);
                BoardController.Register(email);
                return new Response();
            }
            catch (Exception ex)
            {
                log.Debug(ex.Message);
                return new Response<Object>(ex.Message);
            }

        }


        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                UserController.Login(email, password);
                var user = new User(email, UserController.GetUser(email).GetNickname());
                return new Response<User>(user);
            }
            catch (Exception ex)
            {
                return new Response<User>(ex.Message);
            }
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {
                UserController.Logout(email);
                return new Response();
            }
            catch (Exception ex)
            {
                return new Response<Object>(ex.Message);
            }
        }

        /// <summary>
        /// Returns the board of a user. The user must be logged in
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<Board> GetBoard(string email)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    var ColumnsNames = new List<string>();
                    foreach (var column in BoardController.GetBoard(email).GetColumns())
                    {
                        ColumnsNames.Add(column.GetColumnName());
                    }
                    var board = new Board((IReadOnlyCollection<string>)ColumnsNames);
                    return new Response<Board>(board);
                }
                catch (Exception ex)
                {
                    return new Response<Board>(ex.Message);
                }
            }
            else
                return new Response<Board>("This user is not logged in");
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    BoardController.LimitColumnTasks(email, columnOrdinal, limit);
                    return new Response();
                }
                catch (Exception ex)
                {
                    return new Response<Object>(ex.Message);
                }
            }
            else
                return new Response<Exception>("This user is not logged in");
        }


        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    BoardController.AddTask(email, title, description, dueDate);
                    return new Response<Task>(new Task(BoardController.GetTotalTasks() - 1, DateTime.Now, dueDate, title, description));
                }
                catch (Exception ex)
                {
                    return new Response<Task>(ex.Message);
                }
            }
            else
                return new Response<Task>("This User is not logged in");
        }


        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    BoardController.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
                    return new Response();
                }
                catch (Exception ex)
                {
                    return new Response<Object>(ex.Message);
                }
            }
            else
                return new Response<Exception>("This user is not logged in");

        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {

            if (UserController.IsLogged(email))
            {
                try
                {
                    BoardController.UpdateTaskTitle(email, columnOrdinal, taskId, title);
                    return new Response();
                }
                catch (Exception ex)
                {
                    return new Response<Object>(ex.Message);
                }
            }
            else
                return new Response<Exception>("This user is not logged in");

        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    BoardController.UpdateTaskDescription(email, columnOrdinal, taskId, description);
                    return new Response();
                }
                catch (Exception ex)
                {
                    return new Response<Object>(ex.Message);
                }
            }
            else
                return new Response<Exception>("This user is not logged in");
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    BoardController.AdvanceTask(email, columnOrdinal, taskId);
                    return new Response();
                }
                catch (Exception ex)
                {
                    return new Response<Object>(ex.Message);
                }
            }
            else
                return new Response<Exception>("This user is not logged in");
        }


        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnName">Column name</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<Column> GetColumn(string email, string columnName)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    var Tasks = new List<Task>();
                    foreach (var task in BoardController.GetColumn(email, columnName).GetTasks())
                    {
                        Tasks.Add(new Task(task.GetTaskID(), task.GetCreationDate(), task.GetDueDate(), task.GetTitle(), task.GetDescription()));
                    }
                    var column = new Column((IReadOnlyCollection<Task>)Tasks, columnName, BoardController.GetColumn(email, columnName).GetLimit());
                    return new Response<Column>(column);
                }
                catch (Exception ex)
                {
                    return new Response<Column>(ex.Message);
                }
            }
            else
                return new Response<Column>("This user is not logged in");
        }

        /// <summary>
        /// Returns a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            if (UserController.IsLogged(email))
            {
                try
                {
                    var Tasks = new List<Task>();
                    foreach (var task in BoardController.GetColumn(email, columnOrdinal).GetTasks())
                    {
                        Tasks.Add(new Task(task.GetTaskID(), task.GetCreationDate(), task.GetDueDate(), task.GetTitle(), task.GetDescription()));
                    }
                    var column = new Column((IReadOnlyCollection<Task>)Tasks, BoardController.GetColumn(email, columnOrdinal).GetColumnName(), BoardController.GetColumn(email, columnOrdinal).GetLimit());

                    return new Response<Column>(column);
                }
                catch (Exception ex)
                {
                    return new Response<Column>(ex.Message);
                }
            }
            else

                return new Response<Column>("This user is not logged in");

        }

    }
}
