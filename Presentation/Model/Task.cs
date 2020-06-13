using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;
using Presentation.View;

namespace Presentation.Model
{
    public class Task:NotifiableModelObject
    {
        public string userEmail{ get; set; }
        public int column { get; set; }
        public Task(BackendController controller,int column,string userEmail, int id, string title, string description, DateTime creationTime,DateTime dueDate,string emailAssignee) : base(controller)
        {
            this.column = column;
            this.userEmail = userEmail;
            this.TaskId = id;
            this.Title = title;
            this.Description = description;
            this.CreationDate = creationTime;
            this.DueDate = dueDate;
            this.EmailAssignee = emailAssignee;
        }
        public Task(BackendController controller,int column, string userEmail, IntroSE.Kanban.Backend.ServiceLayer.Task task): base(controller)
        {
            this.column = column;
            this.userEmail = userEmail;
            this.TaskId = task.Id;
            this.Title = task.Title;
            this.Description = task.Description;
            this.CreationDate = task.CreationTime;
            this.DueDate = task.DueDate;
            this.EmailAssignee = task.emailAssignee;
          
        }
        public SolidColorBrush BackgroundColor
        {
            get
            { 
                if(Overdue())
                    return new SolidColorBrush( Colors.Red);
                if(NearDue())
                    return new SolidColorBrush(Colors.Orange);
                return new SolidColorBrush();
            }
        }
        public SolidColorBrush BorderColor
        {
            get
            {
                if (IsAssignee(userEmail))
                    return new SolidColorBrush(Colors.Blue);
                return new SolidColorBrush(Colors.Black);
            }
        }
        override
        public string ToString()
        {
            string toString= "ID: " + TaskId + " Title: " + Title + " EmailAssignee: " + EmailAssignee;
            return toString;
        }
        

        
        public bool Overdue()
        {
            return !(DateTime.Compare(DueDate, DateTime.Now) > 0);
        }
        
        public bool NearDue()
        {
            double total = (DueDate - CreationDate).TotalDays;
            double timePassed = (DateTime.Now - CreationDate).TotalDays;
            return (!Overdue()) && (timePassed / total >= 0.75);
        }
        public bool IsAssignee(string email)
        {
            return this.EmailAssignee.Equals(email);
        }
     

        private int _taskId;
        public int TaskId
        {
            get => _taskId;
            set
            {
                _taskId = value;
                RaisePropertyChanged("TaskId");
            }
        }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }
        private DateTime _creationDate;
        public DateTime  CreationDate

        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                RaisePropertyChanged("CreationDate");
            }
        }
        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }
        private string _emailAssignee;
        public string EmailAssignee
        {
            get => _emailAssignee;
            set
            {
                _emailAssignee = value;
                RaisePropertyChanged("EmailAssignee");
            }
        }     
    }
}
