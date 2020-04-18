using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Task :DalObject<Task>
    {
        private int taskId;
        private string title;
        private string description;
        private DateTime creationDate;
        private DateTime dueDate;

        public Task() { }

        public Task(int taskId, string title, string description, DateTime dueDate, DateTime creationDate)
        {
            this.TaskId = taskId;
            this.Title = title;
            this.Description = description;
            this.CreationDate = creationDate;
            this.DueDate = dueDate;
        }
        public int TaskId { get => taskId; set => taskId = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public DateTime DueDate { get => dueDate; set => dueDate = value; }
    }
}
