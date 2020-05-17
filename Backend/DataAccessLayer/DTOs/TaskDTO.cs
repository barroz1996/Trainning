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
        public const string TasksColumnIdColumnColumnId = "ColumnOridnal";
        private Controllers.TaskControl _controller;
        private int _taskId;
        private string _title;
        private string _description;
        private string _dueDate;
        private string _creationTime;
        private string email;
        private int columnOridnal;
        public TaskDTO(int taskId, string title, string description, string dueDate, string creationTime, string email, int columnOridnal)
        {
            this._taskId = taskId;
            this._title = title;
            this._description = description;
            this._dueDate = dueDate;
            this._creationTime = creationTime;
            this.Email = email;
            this.ColumnOridnal = columnOridnal;
            _controller = new Controllers.TaskControl();
            _controller.Insert(this);
        }

        public int TaskId { get => _taskId; set => _taskId = value; }
        public string Title { get => _title; set { _title = value; _controller.Update(_taskId, TasksTitleColumnTitle, value); } }
        public string Description { get => _description; set { _description = value; _controller.Update(_taskId, TasksDescriptionColumnDescription, value); } }
        public string DueDate { get => _dueDate; set { _dueDate = value; _controller.Update(_taskId, TasksDueDateColumnDueDate, value); } }
        public string CreationTime { get => _creationTime; set => _creationTime = value; }
        public string Email { get => email; set => email = value; }
        public int ColumnOridnal { get => columnOridnal; set => columnOridnal = value; }

        public bool Delete(int taskId)
        {
            _controller.Delete(taskId);
        }

    }
}