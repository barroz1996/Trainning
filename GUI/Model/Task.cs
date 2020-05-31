using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace GUI.Model
{
    public struct Task
    {
        public readonly int TaskId;
        public string Title;
        public string Description;
        public readonly DateTime CreationDate;
        public DateTime DueDate;
        public string EmailAssignee;
        public Task(IntroSE.Kanban.Backend.ServiceLayer.Task task)
        {
            this.TaskId = task.Id;
            this.Title = task.Title;
            this.Description = task.Description;
            this.CreationDate = task.CreationTime;
            this.DueDate = task.DueDate;
            this.EmailAssignee = task.emailAssignee;
        }
        override
        public string ToString()
        {
            string toString= "ID: " + TaskId + " Title: " + Title + " EmailAssignee: " + EmailAssignee;
            return toString;
        }
    }
}
