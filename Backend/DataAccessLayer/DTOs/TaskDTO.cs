using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class TaskDTO
    {
        public const string TasksIdColumnId = "ID";
        public const string TasksTitleColumnTitle = "Title";
        public const string TasksDescriptionColumnDescription = "Description";
        public const string TasksDueDateColumnDueDate = "DueDate";
        public const string TasksCreationDateColumnCreationDate = "CreationDate";
        public const string TasksEmailColumnEmail = "Email";
        public const string TasksColumnIdColumnColumnId = "ColumnOrdinal";
        private Controllers.TaskControl _controller;
        private int _taskId;
        private string _title;
        private string _description;
        private DateTime _dueDate;
        private DateTime _creationTime;
        private string _email;
        private int columnOridnal;
        public TaskDTO(int taskId, string title, string description, DateTime dueDate, DateTime creationTime, string email, int columnOridnal)
        {
            this._taskId = taskId;
            this._title = title;
            this._description = description;
            this._dueDate = dueDate;
            this._creationTime = creationTime;
            this._email = email;
            this.ColumnOridnal = columnOridnal;
            _controller = new Controllers.TaskControl();
            _controller.Insert(this);
        }

        public int TaskId { get => _taskId; set => _taskId = value; }
        public string Title { get => _title; set { _title = value;  } }
        public string Description { get => _description; set { _description = value; } }
        public DateTime DueDate { get => _dueDate; set { _dueDate = value; } }
        public DateTime CreationTime { get => _creationTime; set => _creationTime = value; }
        public string Email { get => _email; set => _email = value; }
        public int ColumnOridnal { get => columnOridnal; set => columnOridnal = value; }

       /* public bool Delete()
        {
           return _controller.Delete(this);
        }
        */
    }
}