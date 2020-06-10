using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using Presentation.Model;


namespace Presentation
{
   public class BackendController
    {
        public IService Service { get; private set; }
        public BackendController(IService service)
        {
            this.Service = service;
        }

        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }
        internal void LogOut(string email)
        {
            var res = Service.Logout(email);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal Model.User Login(string email, string password)
        {
            Response<IntroSE.Kanban.Backend.ServiceLayer.User> user = Service.Login(email, password);
            if (user.ErrorOccured)
            {
                throw new Exception(user.ErrorMessage);
            }
            return new Model.User(this, email,user.Value.Nickname);
        }
        internal void Register(string email, string password, string nickName, string host)
        {
            Response reg;
            if (string.IsNullOrWhiteSpace(host))
            {
                reg = Service.Register(email, password, nickName);
            }
            else
            {
                reg = Service.Register(email, password, nickName, host);
            }
            if (reg.ErrorOccured)
            {
                throw new Exception(reg.ErrorMessage);
            }
        }
        internal void Move(string email, int columnOrdinal,int direction)
        {
            Response res;
            if (direction == 1)
            {
                 res = Service.MoveColumnRight(email, columnOrdinal);
            }
            else
            {
                res = Service.MoveColumnLeft(email, columnOrdinal);
            }
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else // we need to check !!!
            {
              //  ColumnsNames = Service.GetBoard(email).Value.GetColumnsNames();
            }

        }
        internal void AddColumn(string email, string colName, string colOrdinal)
        {
            int k = 0;
            if (!int.TryParse(colOrdinal, out k))
            { 
                throw new Exception("Column Ordinal must be an integer.");
            }
            var res = Service.AddColumn(email, k, colName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        internal void RemoveColumn(string email, int columnOrdinal)
        {
            var res = Service.RemoveColumn(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
               // ColumnsNames = Service.GetBoard(email).Value.GetColumnsNames();
               // MessageBox.Show("Column removed successfully");
            }
        }
        internal void SetLimit(string email, int columnOrdinal, string limit)
        {
            int k = 0;
            if (!int.TryParse(limit, out k))
            {
               // ColOrdinal = "";
                throw new Exception("Limit must be an integer.");
            }
            var res = Service.LimitColumnTasks(email, columnOrdinal, k);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {/*
                NewLimit = "";
                if (k == -1)
                    MessageBox.Show("The limit of your " + ColumnsNames.ElementAt<string>(columnOrdinal) + " column was disabled");
                else
                    MessageBox.Show("The limit of your " + ColumnsNames.ElementAt<string>(columnOrdinal) + " column was set to " + k);
           */
                }

        }
        internal void AddTask(string email, string title, string description, DateTime dueDate)
        {
            var res = Service.AddTask(email, title, description, dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
               /* NewTaskTitle = "";
                NewDescription = "";
                DueData = DateTime.Now;
                MessageBox.Show("Task " + res.Value.GetId() + " was added successfully to your " + ColumnsNames.First<string>());
            */
              }
        }
        internal void ChangeColumnName(string email,int columnOrdinal,string newName)
        {
            if (!(columnOrdinal >= 0 && columnOrdinal < Service.GetBoard(email).Value.ColumnsNames.Count))
            {
                throw new Exception("Please select one of the columns in the list before clicking this botton.");
            }
            else
            {
                var res = Service.ChangeColumnName(email, columnOrdinal, newName);
                if (res.ErrorOccured)
                {
                    throw new Exception(res.ErrorMessage);
                }
            }
            
        }
        internal void DeleteTask(Model.Task deltask)
        {
            var res = Service.DeleteTask(deltask.userEmail, deltask.column, deltask.TaskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
                /*
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                MessageBox.Show("Task " + deltask.TaskId + " removed successfully");
                */
            }
        }
        internal void AssignTask(Model.Task task1, string newEmail)
        {
            var res = Service.AssignTask(task1.userEmail, task1.column, task1.TaskId, newEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
                /*
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                EmailAssignee = "";
                MessageBox.Show("Task " + task1.TaskId + " was assigned to " + newEmail);
                */
            }
        }
        public void AdvanceTask(Model.Task deltask)
        {
            var res = Service.AdvanceTask(deltask.userEmail, deltask.column, deltask.TaskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
                deltask.column++;
                /*
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                MessageBox.Show("Task " + deltask.TaskId + " was advanced to the next column.");
                */
            }
        }
        public void UpdateTitle(Model.Task task, string title)
        {
            var res = Service.UpdateTaskTitle(task.userEmail, task.column, task.TaskId, title);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
               // MessageBox.Show("Title updated successfully");
            }
        }
        public void UpdateDescription(Model.Task task,string newdes)
        {
            var res = Service.UpdateTaskDescription(task.userEmail, task.column, task.TaskId, newdes);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
               // MessageBox.Show("Description updated successfully");
            }
        }
        internal void UpdateDueDate(Model.Task task, DateTime newDue)
        {
            var res = Service.UpdateTaskDueDate(task.userEmail, task.column, task.TaskId, newDue);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            else
            {
             //   MessageBox.Show("Due date updated successfully");
            }
        }
        internal IntroSE.Kanban.Backend.ServiceLayer.Task GetTask(string email, int column, int index)
        {
            var res = Service.GetColumn(email, column);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res.Value.Tasks.ElementAt(index);
        }
        internal List<string> GetAllColumnNames(string email)
        {
            IReadOnlyCollection<string> col = Service.GetBoard(email).Value.ColumnsNames;
            return new List<string>(col);
        }
        internal List<IntroSE.Kanban.Backend.ServiceLayer.Task> GetTasks(string email, int column)
        {       
            IReadOnlyCollection<IntroSE.Kanban.Backend.ServiceLayer.Task> tasks = Service.GetColumn(email,column).Value.Tasks;
            return new List<IntroSE.Kanban.Backend.ServiceLayer.Task>(tasks);
        }
        internal IntroSE.Kanban.Backend.ServiceLayer.Column GetColumn(string email,int columnOrdinal)
        {
            return Service.GetColumn(email, columnOrdinal).Value;
        }
    }
}
