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
        public bool AddColumn()
        {            
            try
            {
                Controller.AddColumn(Email, Name, ColumnOrdinal);
                MessageBox.Show("Column added successfully!");
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
       public bool AddTask()
        {
            try
            {
                Controller.AddTask(Email, Title, Description, DueDate);
                MessageBox.Show("Task added successfully!");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public bool ChangeName()
        {
            try
            {
                Controller.ChangeColumnName(Email,Ordinal,Name);
                MessageBox.Show("Column name changed to "+Name);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
