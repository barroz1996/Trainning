using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Task
    {
        private int taskId;
        private string title;
        private string description;
        private DateTime creationDate;
        private DateTime dueDate;
        public Task(int taskID,string title, string description, DateTime dueDate)
        {
            this.taskID = taskId;
            this.title = title;
            this.description = description;
            this.creationDate = DateTime.Now;
            this.dueDate = dueDate;
        }
        public void EditTaskTitle(string title) { this.title = title; }
        public void EditTaskDescription(string description) { this.description = description; }
        public void EditTaskDueDate(DateTime dueDate) { this.dueDate = dueDate; }
        public int GetTaskID() { return this.taskId; }
        public DateTime GetCreationDate() { return this.creationDate; }
        public string GetTitle() { return this.title; }
        public string GetDescription() { return this.description; }
        public DateTime GetDueDate() { return this.dueDate; }

    }
}
