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
    class BoardWindowView : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        public BoardWindowView(Model.User user)
        {           
            this.Email = user.Email;
            this.board = new Model.Board(user.Controller, user);
            this.Controller = user.Controller;
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
            this.Board = new Model.Board(Controller, Email);
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
                Controller.AdvanceTask(Task);
                MessageBox.Show("Task Advanced successfully!");
                ReLoad();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void ChangeName(int ordinal, string name)
        {
            var col = new ReNameWindow(Controller, Email,ordinal,name);
            col.ShowDialog();
            ReLoad();
        }
        public void SetLimit(int ordinal, int limit)
        {
            var col = new SetLimitWindow(Controller, Email, ordinal, limit.ToString());
            col.ShowDialog();
            ReLoad();
        }
        public void OpenTask()
        {
            var taskwindow = new TaskWindow(Controller, Email, Task);
            taskwindow.ShowDialog();
            ReLoad();
        }
      
    }

}
