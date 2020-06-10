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
    class TaskViewModel : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        public TaskViewModel(BackendController controller, string email, Model.Task task)
        {
            this.Controller = controller;
            this.Task = task;
            /*Email = email;
            Id = task.TaskId;
            Title = task.Title;
            Description = task.Description;
            CreationDate = task.CreationDate;
            DueDate = task.DueDate;
            EmailAssignee = task.EmailAssignee;
            ColumnOrdinal = columnOrdinal;*/
            TaskName = "Task " + Task.TaskId;
        }
        private Model.Task task;
        public Model.Task Task
        {
            get { return task; }
            set
            {
                task = value;
                RaisePropertyChanged("Task");
            }
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
        /*private string email;
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
        }*/
        public void UpdateTitle()
        {
            try
            {
                Controller.UpdateTitle(Task);
                MessageBox.Show("Title updated successfully");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void UpdateDescription()
        {
            try
            {
                Controller.UpdateDescription(Task);
                MessageBox.Show("Description updated successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void UpdateDueDate()
        {
            try
            {
                Controller.UpdateDueDate(Task);
                MessageBox.Show("Due date updated successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void UpdateEmailAssignee()
        {
            try
            {
                Controller.AssignTask(task);
                MessageBox.Show("Task Assigned to "+Task.userEmail);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
