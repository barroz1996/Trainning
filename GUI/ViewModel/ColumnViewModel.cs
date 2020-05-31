using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using GUI.Model;
using System.Windows;
using GUI.View;

namespace GUI.ViewModel
{
    class ColumnViewModel : INotifyPropertyChanged
    {
        private Service service;
        public ColumnViewModel(Service service,string email, int columnOrdinal)
        {
            this.Email = email;
            this.ColumnOrdinal = columnOrdinal;
            this.service = service;
            List<Model.Task> temp = new List<Model.Task>();
            foreach(var task in service.GetColumn(email, columnOrdinal).Value.Tasks)
            {
                temp.Add(new Model.Task(task));
            }
            Tasks = temp;
        }
        private List<Model.Task> tasks;
        public List<Model.Task> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
        private int columnOrdinal;
        public int ColumnOrdinal
        {
            get { return columnOrdinal; }
            set
            {
                columnOrdinal = value;
                RaisePropertyChanged("ColumnOrdinal");
            }
        }
        public void DeleteTask(Model.Task deltask)
        {
            var res = service.DeleteTask(Email, ColumnOrdinal,deltask.TaskId);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                MessageBox.Show("Task "+deltask.TaskId+" removed successfully");
            }
        }
        private string emailAssignee;
        public string EmailAssignee
        {
            get { return emailAssignee; }
            set
            {
                emailAssignee = value;
                RaisePropertyChanged("EmailAssignee");
            }
        }
        public void AssignTask(Model.Task task1,string newEmail)
        {
            var res = service.AssignTask(Email, ColumnOrdinal, task1.TaskId,newEmail);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                EmailAssignee = "";
                MessageBox.Show("Task " + task1.TaskId + " was assigned to "+newEmail);
            }
        }
        public void AdvanceTask(Model.Task deltask)
        {
            var res = service.AdvanceTask(Email, ColumnOrdinal, deltask.TaskId);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                MessageBox.Show("Task " + deltask.TaskId + " was advanced to the next column.");
            }
        }
        /*public View.ColumnWindow NextColumn()
        {
            if (columnOrdinal + 1 == service.GetBoard(Email).Value.ColumnsNames.Count)
            {
                MessageBox.Show("This is the last column");
                return new View.ColumnWindow(service, Email, ColumnOrdinal);
            }
            else
            {
                return new View.ColumnWindow(service, Email, ColumnOrdinal + 1);
            }
        }*/
        public void GetTask(string email, int taskOrdinal)
        {
            if (taskOrdinal >= 0 && taskOrdinal < Tasks.Count)
            {
                List<Model.Task> temp = new List<Model.Task>();
                foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
                {
                    temp.Add(new Model.Task(task));
                }
                Tasks = temp;
                var Task = new TaskWindow(service, email,ColumnOrdinal, tasks[taskOrdinal]);
                Task.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select one of the tasks in the list before clicking this botton.");
            }
        }
        public void Reload()
        {
            List<Model.Task> temp = new List<Model.Task>();
            foreach (var task in service.GetColumn(Email, columnOrdinal).Value.Tasks)
            {
                temp.Add(new Model.Task(task));
            }
            Tasks = temp;
        }
    }
}
