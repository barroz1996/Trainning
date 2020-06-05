using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;
using Presentation.View;

namespace Presentation
{
    class BoardWindowView : INotifyPropertyChanged
    {
        private readonly string emailCreator;
        public string EmailCreator
        {
            get { return emailCreator; }

        }
        private Service service;
        public BoardWindowView(Service service, string email)
        {
            this.service = service;
            this.Email = email;
            this.emailCreator = service.GetBoard(email).Value.emailCreator;
            this.columnsNames = service.GetBoard(email).Value.GetColumnsNames();
        }
        public string Welcome
        {
            get { return "Welcome "+Email; }
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
        private IReadOnlyCollection<string> columnsNames;
        public IReadOnlyCollection<string> ColumnsNames
        {
            get { return columnsNames; }
            set
            {
                columnsNames = value;
                RaisePropertyChanged("ColumnsNames");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public void LogOut(string email) {
            service.Logout(email);
            ColOrdinal = "";
            NewColName = "";
        }
        public void MoveRight(string email, int columnOrdinal)
        {
            var res = service.MoveColumnRight(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                ColumnsNames = service.GetBoard(email).Value.GetColumnsNames();
            }

        }
        public void MoveLeft(string email, int columnOrdinal)
        {
            var res = service.MoveColumnLeft(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                ColumnsNames = service.GetBoard(email).Value.GetColumnsNames();
            }
        }
        private string newName = "";
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                RaisePropertyChanged("NewName");
            }
        }
        private string newColName = "";
        public string NewColName
        {
            get { return newColName; }
            set
            {
                newColName = value;
                RaisePropertyChanged("NewColName");
            }
        }


        private string colOrdinal = "";
        public string ColOrdinal
        {
            get { return colOrdinal; }
            set
            {              
                colOrdinal = value;               
                RaisePropertyChanged("ColOrdinal");
            }
        }
        public void AddColumn(string email,string colName,string colOrdinal)
        {
            int k = 0;
            if (!int.TryParse(colOrdinal, out k))
            {
                ColOrdinal = "";
                MessageBox.Show("Column Ordinal must be an integer.");
                return;
            }
            var res = service.AddColumn(email, k, colName);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                ColumnsNames = service.GetBoard(email).Value.GetColumnsNames();
                NewColName= "";
                ColOrdinal = "";
                MessageBox.Show("Column added successfully");
            }
        }
        public void RemoveColumn(string email,int columnOrdinal)
        {
            var res = service.RemoveColumn(email,columnOrdinal);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                ColumnsNames = service.GetBoard(email).Value.GetColumnsNames();
                MessageBox.Show("Column removed successfully");
            }
        }
        private string newTaskTitle= "";
        public string NewTaskTitle
        {
            get { return newTaskTitle; }
            set
            {
                newTaskTitle = value;
                RaisePropertyChanged("NewTaskTitle");
            }
        }
        private string newDescription = "";
        public string NewDescription
        {
            get { return newDescription; }
            set
            {
                newDescription = value;
                RaisePropertyChanged("NewDescription");
            }
        }
        private DateTime dueData = DateTime.Now;
        public DateTime DueData
        {
            get { return dueData; }
            set
            {
                dueData = value;
                RaisePropertyChanged("DueData");
            }
        }
        private string newLimit = "";
        public string NewLimit
        {
            get { return newLimit; }
            set
            {
                newLimit = value;
                RaisePropertyChanged("NewLimit");
            }
        }
        public void SetLimit(string email, int columnOrdinal, string limit)
        {
            int k = 0;
            if (!int.TryParse(limit, out k))
            {
                ColOrdinal = "";
                MessageBox.Show("Limit must be an integer.");
                return;
            }
            var res = service.LimitColumnTasks(email, columnOrdinal, k);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                NewLimit = "";
                if(k==-1)
                    MessageBox.Show("The limit of your "+ColumnsNames.ElementAt<string>(columnOrdinal)+" column was disabled");
                else
                    MessageBox.Show("The limit of your " + ColumnsNames.ElementAt<string>(columnOrdinal) + " column was set to "+k);
            }
        }


        public void AddTask(string email,string title, string description,DateTime dueDate)
        {
            var res = service.AddTask(email,title,description,dueDate);
            if (res.ErrorOccured)
            {
                MessageBox.Show(res.ErrorMessage);
            }
            else
            {
                NewTaskTitle = "";
                NewDescription = "";
                DueData = DateTime.Now;
                MessageBox.Show("Task " + res.Value.GetId() + " was added successfully to your " + ColumnsNames.First<string>());
            }
        }
        public void GetColumn(string email,int columnOrdinal)
        {
            if (columnOrdinal >= 0 && columnOrdinal < ColumnsNames.Count)
            {
                var Column = new ColumnWindow(service, email, columnOrdinal);
                Column.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select one of the columns in the list before clicking this botton.");
            }
        }
        public void ChangeColumnName(int columnOrdinal)
        {
            if (!(columnOrdinal >= 0 && columnOrdinal < ColumnsNames.Count))
            {
                MessageBox.Show("Please select one of the columns in the list before clicking this botton.");
            }
            else
            {
                var res = service.ChangeColumnName(Email, columnOrdinal, NewName);
                if (!res.ErrorOccured)
                {
                    NewName = "";
                    ColumnsNames = service.GetBoard(email).Value.GetColumnsNames();
                }
                else
                {
                    MessageBox.Show(res.ErrorMessage);
                }
            }
        }


    }

}
