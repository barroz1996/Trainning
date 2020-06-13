using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;
using Presentation.View;
using System.Windows.Media;

namespace Presentation
{
    class BoardWindowView : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        public BoardWindowView(Model.User user)
        {           
            this.Email = user.Email;
            this.Filter = "";
            this.board = new Model.Board(user.Controller, user,Filter);
            this.Controller = user.Controller;
        }
     
        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                RaisePropertyChanged("Filter");
            }
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
        
        private Model.Board board;
        public Model.Board Board
        {
            get { return board; }
            set
            {
                board = value;
                RaisePropertyChanged("Board");
            }
        }
       
        public void Logout()
        {
            Controller.LogOut(Email);
        }
        public void AddColumn()
        {
            var newCol = new AddColumnWindow(Controller, Email);
            newCol.ShowDialog();
            ReLoad();
        }
        public void ReLoad()
        {
            this.Board = new Model.Board(Controller, Email,Filter);
        }
      
        public void RemoveColumn(int ordinal)
        {
            try
            {
                Controller.RemoveColumn(Email, ordinal);
                MessageBox.Show("Column removed successfully!");
                ReLoad();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Move(int columnOrdinal,int direction)
        {           
            try
            {
                Controller.Move(Email, columnOrdinal, direction);
                if(direction==1)
                   MessageBox.Show("Column moved right successfully!");
                else
                   MessageBox.Show("Column moved left successfully!");
                ReLoad();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void AddTask()
        {
            var newTask = new AddTaskWindow(Controller, Email);
            newTask.ShowDialog();
            ReLoad();
        }
        public void AdvanceTask() {
            try
            {
                if (Task == null)
                    throw new Exception("No task selected!");
                Controller.AdvanceTask(Task);
                MessageBox.Show("Task Advanced successfully!");
                ReLoad();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void ChangeName(int ordinal, Model.Column column)
        {
            if (column != null)
            {
                var col = new ReNameWindow(Controller, Email, ordinal, column.Name);
                col.ShowDialog();
                ReLoad();
            }
            else
                MessageBox.Show("Please select column!");
        }
        public void SetLimit(int ordinal, Model.Column column)
        {
            if (column != null) { 
            var col = new SetLimitWindow(Controller, Email, ordinal, column.Limit.ToString());
            col.ShowDialog();
            ReLoad();
            }
          else
             MessageBox.Show("Please select column!");
        }
        public void OpenTask()
        {
            if (Task != null) { 
               var taskwindow = new TaskWindow(Controller, Email, Task);
               taskwindow.ShowDialog();
               ReLoad();
           }
        }
        public void DeleteTask()
        {
            try
            {
                if (Task == null)
                    throw new Exception("No task selected!");
                Controller.DeleteTask(Task);
                MessageBox.Show("Task removed successfully!");
                ReLoad();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }

}
