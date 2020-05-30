using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;

namespace GUI
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
            this.emailCreator = email;
            this.columnsNames = service.GetBoard(email).Value.GetColumnsNames();
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
        public void LogOut(Service service, string email) {
            service.Logout(email);
            ColOrdinal = "";
            NewColName = "";
        }
        public void MoveRight(Service service, string email, int columnOrdinal)
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
        public void MoveLeft(Service service, string email, int columnOrdinal)
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
        public void AddColumn(Service service,string email,string colName,string colOrdinal)
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
        public void RemoveColumn(Service service,string email,int columnOrdinal)
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
        private DateTime dueData = DateTime.MaxValue;
        public DateTime DueData
        {
            get { return dueData; }
            set
            {
                dueData = value;
                RaisePropertyChanged("DueData");
            }
        }
        public void AddTask(Service service,string email,string title, string description,DateTime dueDate)
        {
            var res = service.AddTask(email,title,description,dueDate);
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


    }

}
