using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;
using Presentation.View;

namespace Presentation.ViewModel
{
    class SetBoardWindowView : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        public SetBoardWindowView(BackendController controller,string email)
        {
            this.Controller = controller;
            this.Email = email;
            this.Name = "";
            this.ColumnOrdinal = "";
            this.Title = "";
            this.Description = "";
            this.dueDate = DateTime.Now;
            this.Limit = "";
        }
        public SetBoardWindowView(BackendController controller, string email,int columnOrdinal,string name)
        {
            this.Controller = controller;
            this.Ordinal = columnOrdinal;
            this.ColumnOrdinal = "";
            this.Email = email;
            this.Name = name;
            this.Title = "";
            this.Description = "";
            this.dueDate = DateTime.Now;
            this.Limit = "";
        }
        public SetBoardWindowView(BackendController controller, string email,string limit, int columnOrdinal)
        {
            this.Controller = controller;
            this.Ordinal = columnOrdinal;
            this.ColumnOrdinal = "";
            this.Email = email;
            this.Name = "";
            this.Title = "";
            this.Description = "";
            this.dueDate = DateTime.Now;
            this.Limit = limit;
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string columnOrdinal;
        public string ColumnOrdinal
        {
            get { return columnOrdinal; }
            set
            {
                columnOrdinal = value;
                RaisePropertyChanged("ColumnOrdinal");
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
        private string limit;
        public string Limit
        {
            get { return limit; }
            set
            {
                limit = value;
                RaisePropertyChanged("Limit");
            }
        }
        private int ordinal;
        public int Ordinal
        {
            get { return ordinal; }
            set
            {
                ordinal = value;
                RaisePropertyChanged("Ordinal");
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
        public void AddColumn(AddColumnWindow newCol)
        {            
            try
            {
                Controller.AddColumn(Email, Name, ColumnOrdinal);
                MessageBox.Show("Column added successfully!");
                newCol.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
       public void AddTask(AddTaskWindow newTask)
        {
            try
            {
                Controller.AddTask(Email, Title, Description, DueDate);
                MessageBox.Show("Task added successfully!");
                newTask.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void ChangeName(ReNameWindow rename)
        {
            try
            {
                Controller.ChangeColumnName(Email,Ordinal,Name);
                MessageBox.Show("Column name changed to "+Name);
                rename.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void setLimit(SetLimitWindow setLimit)
        {
            try
            {
                Controller.SetLimit(Email, Ordinal, Limit);
                if (Limit.Equals("-1"))
                    MessageBox.Show("The limit of this column was disabled");
                else
                    MessageBox.Show("The limit of this column was set to "+Limit);
                setLimit.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
