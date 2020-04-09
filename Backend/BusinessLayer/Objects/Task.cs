using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    class Task
    {
        private int taskID;
        private String title;
        private String description;
        private DateTime creationDate;
        private DateTime dueDate;
        public Task(int taskID,String title, String description, DateTime dueDate)
        {
            this.taskID = taskID;
            this.title = title;
            this.description = description;
            this.creationDate = DateTime.Now;
            this.dueDate = dueDate;
        }
        public void EditTitle(String title) { this.title = title; }
        public void EditDescription(String description) { this.description = description; }
        public void EditDueDate(DateTime dueDate) { this.dueDate = dueDate; }
        public int GetTaskID() { return this.taskID; }
        public DateTime GetCreationDate() { return this.creationDate; }
        public String GetTitle() { return this.title; }
        public String GetDescription() { return this.description; }
        public DateTime GetDueDate() { return this.dueDate; }

    }
}
