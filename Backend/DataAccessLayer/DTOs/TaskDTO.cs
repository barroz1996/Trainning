using System;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class TaskDTO
    {
        public const string TasksIdColumnId = "ID";
        public const string TasksTitleColumnTitle = "Title";
        public const string TasksDescriptionColumnDescription = "Description";
        public const string TasksDueDateColumnDueDate = "DueDate";
        public const string TasksCreationDateColumnCreationDate = "CreationDate";
        public const string TasksEmailColumnEmail = "Email";
        public const string TasksColumnIdColumnColumnId = "ColumnOrdinal";
        private readonly Controllers.TaskControl _controller;

        public TaskDTO(int taskId, string title, string description, DateTime dueDate, DateTime creationTime, string email, int columnOridnal)
        {
            TaskId = taskId;
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreationTime = creationTime;
            Email = email;
            ColumnOridnal = columnOridnal;
            _controller = new Controllers.TaskControl();

        }

        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreationTime { get; set; }
        public string Email { get; set; }
        public int ColumnOridnal { get; set; }

    }
}