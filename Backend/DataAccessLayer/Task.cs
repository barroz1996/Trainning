using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Task:DalObject
    {
        private int taskId;
        private string title;
        private string description;
        private DateTime creationDate;
        private DateTime dueDate;
        public Task(int taskId, string title, string description, DateTime dueDate)
        {
            this.taskId = taskId;
            this.title = title;
            this.description = description;
            this.creationDate = DateTime.Now;
            this.dueDate = dueDate;
        }
    }
}
