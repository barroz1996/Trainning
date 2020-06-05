using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;
using Presentation.Model;
using System.Windows;

namespace Presentation.ViewModel
{
    class TaskViewModel : INotifyPropertyChanged
    {
        private Service service;
        public TaskViewModel(Service service,string email, int columnOrdinal,Model.Task task)
        {
            this.service = service;
            Email = email;
            Id = task.TaskId;
            Title = task.Title;
            Description = task.Description;
            CreationDate = task.CreationDate;
            DueDate = task.DueDate;
            EmailAssignee = task.EmailAssignee;
            ColumnOrdinal = columnOrdinal;
            TaskName = "Task " + Id;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set
            {
                taskName = value;
                RaisePropertyChanged("TaskName");
            }
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
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged("Title");
            }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }
        private DateTime creationDate;
        public DateTime CreationDate
        {
            get { return creationDate; }
            set
            {
                creationDate = value;
                RaisePropertyChanged("CreationDate");
            }
        }
        private DateTime dueDate;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
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
        public void UpdateTitle()
        {
            var res = service.UpdateTaskTitle(email, ColumnOrdinal, Id, Title);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                MessageBox.Show("Title updated successfully");
            }
        }
        public void UpdateDescription()
        {
            var res = service.UpdateTaskDescription(email, ColumnOrdinal, Id, Description);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                MessageBox.Show("Description updated successfully");
            }
        }
        public void UpdateDueDate()
        {
            var res = service.UpdateTaskDueDate(email, ColumnOrdinal, Id, DueDate);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                MessageBox.Show("Due date updated successfully");
            }
        }
    }
}
